using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.BLL.Exceptions;
using Chess_Tournament_Tracker.BLL.Mappers;
using Chess_Tournament_Tracker.DAL.Repositories;
using Chess_Tournament_Tracker.IL.EmailInfrastructures;
using Chess_Tournament_Tracker.IL.PasswordInfrastructures;
using Chess_Tournament_Tracker.IL.TokenInfrastructures;
using Chess_Tournament_Tracker.Models.Entities;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly EmailSender _sender;
        private readonly TokenManager _tokenManager;


        public UserService(IUserRepository repository, EmailSender sender, TokenManager tokenManager)
        {
            _repository = repository;
            _sender = sender;
            _tokenManager = tokenManager;
        }

        public bool Delete(Guid id)
        {
            User? user = _repository.FindOne(id);
            if (user is null)
                throw new KeyNotFoundException("Doesn't exist");
            if (!_repository.HasTournamentInProgress(user.Id))
                throw new UserRules("Cannot delete user with tournament in progress");
            user.IsDeleted = true;
            return _repository.Update(user);
        }

        public User GetById(Guid id)
        {
            return _repository.FindOne(id) ?? throw new ArgumentNullException("Not Found");
        }

        public string Login(LoginDTO loginUser)
        {
            User? user = _repository.FindOne(u => u.Pseudo == loginUser.Login || u.Mail == loginUser.Login);
            if (user is null)
            {
                throw new KeyNotFoundException("User Doesn't exist");
            }
            if (Argon2.Verify(user.Password, loginUser.Password + user.Salt))
                return _tokenManager.GenerateToken(user);
            throw new UnauthorizedAccessException("Wrong Password");
        }

        public User Register(RegisterDTO userRegister)
        {
            User? oldUser = _repository.FindOne(u => u.Mail == userRegister.Mail && u.IsDeleted == true);
            if (oldUser is not null)
            {
                oldUser.IsDeleted = false;
                _repository.Update(oldUser);
                return oldUser;
            }
            if (_repository.Any(u => u.Pseudo == userRegister.Pseudo || u.Mail == userRegister.Mail))
                throw new ValidationException("Already exist");

            using TransactionScope t = new();

            User user = userRegister.ToDAL();
            user.Password = PasswordGenerator.Create(10);
            user.Id = Guid.NewGuid();
            user.Salt = Guid.NewGuid();
            user.ELO = userRegister.ELO ?? 1200;
            _sender.SendPassword(user.Pseudo, user.Password, user.Mail);
            user.Password = Argon2.Hash(user.Password + user.Salt);

            t.Complete();

            return _repository.Insert(user);
        }

        public bool Update(User user)
        {
            return _repository.Update(user);
        }
    }
}
