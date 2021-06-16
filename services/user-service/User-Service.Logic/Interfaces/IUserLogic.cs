using System;
using System.Collections.Generic;
using System.Text;
using UserService.Domain.Entities;

namespace UserService.Logic.Interfaces
{
    public interface IUserLogic
    {
        User createUser(User user);

        bool updateUser(User user);

        bool deleteUser(int id);

        User FindUserById(int id);

        User FindUserByName(string name);

        User FindUserByGUID(Guid guid);




    }
}
