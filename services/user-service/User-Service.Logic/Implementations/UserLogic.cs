using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserService.Data;
using UserService.Domain.Entities;
using UserService.Logic.Interfaces;

namespace UserService.Logic.Implementations
{
    public class UserLogic : IUserLogic
    {
        private readonly ApplicationDbContext _context;

        public UserLogic(ApplicationDbContext context)
        {
            _context = context;
        }


        public User createUser(User user)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<User> wow = _context.User.Add(user);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return null;
                throw;
            }

            ;
            return wow.Entity;
        }

        public bool deleteUser(int id)
        {
            User user = _context.User.Find(id);

            if(user == null)
            {
                return false;
            }
            _context.User.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public User FindUserByGUID(Guid guid)
        {
            throw new NotImplementedException();
        }

        // TODO: protect this endpoint, internal usage only
        public User FindUserById(int id)
        {
            User user = _context.User.Find(id);

            return user;
        }

        public User FindUserByName(string name)
        {
            User user = _context.User.Where(a => a.Name == name).FirstOrDefault();

            return user;
        }

        public bool updateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
