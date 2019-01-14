using ELearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Repository
{
    public class UserRepository : BaseRepository
    {
        public User GetUser(User user)
        {
            return QueryFirst<User>("spGetUser", new { @pUserName = user.UserName, @pPassword = user.Password });
        }

        public User GetUserById(int id)
        {
            return QueryFirst<User>("GetUserById", new { @pId = id });
        }

        public Role GetRole(int id)
        {
            return QueryFirst<Role>("GetRole", new { @pId = id });
        }

        public List<User> GetAllUsers()
        {
            return QueryMultiple<User>("spGetAllUsers");
        }

        public User InsertUser(User user)
        {
            return QueryFirst<User>("spInsertUser", new
            {

                @pFirstName = user.FullName
                ,
                @pUserName = user.UserName
                ,
                @pPassword = user.Password
                ,
                @pAddress = user.Address
                ,
                @pEmail = user.Email
                ,
                @pPhone = user.Phone
                ,
                @pDate = user.Date
                ,
                @pIsAdmin = user.IsAdmin
                ,
                @pRoleId = user.RoleId
            });
        }

        public User UpdateUser(User user)
        {
            return QueryFirst<User>("spUpdateUser", new
            {
                @pId = user.Id
                ,
                @pFirstName = user.FullName
                ,
                @pUserName = user.UserName
                ,
                @pPassword = user.Password
                ,
                @pAddress = user.Address
                ,
                @pEmail = user.Email
                ,
                @pPhone = user.Phone
                ,
                @pDate = user.Date
                ,
                @pIsAdmin = user.IsAdmin
                ,
                @pRoleId = user.RoleId
            });
        }

        public List<Role> GetAllRoles()
        {
            return QueryMultiple<Role>("GetAllRoles");
        }
    }
}
