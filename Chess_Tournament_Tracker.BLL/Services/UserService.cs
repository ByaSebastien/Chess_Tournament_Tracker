using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.BLL.Mappers;
using Chess_Tournament_Tracker.DAL.Repositories;
using Chess_Tournament_Tracker.Models.Entities;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool Delete(User user)
        {
            return _repository.Delete(user);
        }

        public User GetById(Guid id)
        {
            return _repository.FindOne(id) ?? throw new ArgumentNullException("Not Found");
        }

        public User Login(LoginDTO loginUser)
        {
            User user = _repository.FindOne(u => u.Pseudo == loginUser.ConnectionField || u.Mail == loginUser.ConnectionField);
            if (Argon2.Hash(loginUser.Password) == user.Password)
                return user;
            throw new UnauthorizedAccessException("Wrong Password");
        }

        public User Register(RegisterDTO userRegister)
        {
            if (_repository.Any(u => u.Pseudo == userRegister.Pseudo || u.Mail == userRegister.Mail))
                throw new ValidationException("Already exist");
            User user = userRegister.ToDAL();
            user.Id = Guid.NewGuid();
            user.Salt = Guid.NewGuid();
            user.Password = Argon2.Hash(userRegister.Password + user.Salt);
            user.ELO = userRegister.ELO ?? 1200;
            return _repository.Insert(user);
        }

        public bool Update(User user)
        {
            return _repository.Update(user);
        }
    }
}
