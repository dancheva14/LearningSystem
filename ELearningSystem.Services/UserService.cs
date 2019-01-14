using ELearningSystem.Models;
using ELearningSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Services
{
    public class UserService
    {
        private UserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public User GetUserByNameAndPass(User user)
        {
            return userRepository.GetUser(user);
        }

        public User GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }

        public Role GetRole(int id)
        {
            return userRepository.GetRole(id);
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }
        public User UpdateUser(User user)
        {
            return userRepository.UpdateUser(user);
        }


        public User InsertUser(User user)
        {
            return userRepository.InsertUser
                    (user);
        }

        public List<Role> GetAllRoles()
        {
            return userRepository.GetAllRoles();
        }
    }
}
