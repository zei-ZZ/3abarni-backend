﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using _3abarni_backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace _3abarni_backend.Repositories
{
    public class UserRepository
    {
        const int NUMBER_OF_ITEMS_PER_PAGE= 6;
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(string id)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public void Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Update(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string id)
        {
            var userToDelete = _dbContext.Users.Find(id);
            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                _dbContext.SaveChanges();
            }
        }
        public IEnumerable<User> SearchPaginated(string query,int page)
        {
            if(query.IsNullOrEmpty())
                  return Enumerable.Empty<User>();
            var users = _dbContext.Users.Where(user => user.NormalizedUserName.Contains(query.Trim().ToUpper()))
                .Skip((page-1)* NUMBER_OF_ITEMS_PER_PAGE)
                .Take(NUMBER_OF_ITEMS_PER_PAGE)
                .ToList();
            return users;
        }

    }
}
