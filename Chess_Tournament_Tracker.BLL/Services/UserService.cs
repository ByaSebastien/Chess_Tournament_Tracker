﻿using Chess_Tournament_Tracker.BLL.DTO.Users;
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
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public bool Delete(User user)
        {
            return _repository.Delete(user);
        }

        public User GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new ArgumentNullException("Not Found");
        }

        public User Login(UserLoginDTO loginUser)
        {
            User user = _repository.FindOne(u => u.Pseudo == loginUser.ConnectionField || u.Mail == loginUser.ConnectionField);
            if (Argon2.Hash(loginUser.Password) == user.Password)
                return user;
            throw new UnauthorizedAccessException("Wrong Password");
        }

        public User Register(User user)
        {
            if (_repository.FindAny(u => u.Pseudo == user.Pseudo || u.Mail == user.Mail))
                throw new ValidationException("Already exist");
            user.Password = Argon2.Hash(user.Password);
            return _repository.Insert(user);
        }

        public bool Update(User user)
        {
            return _repository.Update(user);
        }
    }
}