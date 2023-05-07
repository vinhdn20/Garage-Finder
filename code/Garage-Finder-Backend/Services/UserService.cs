using DataAccess.DAO;
using GFData.Models.Entity;

namespace Garage_Finder_Backend.Services
{
    public class UserService
    {
        private UsersDAO usersDAO = new UsersDAO();
        public bool ValidateLogin(string email, string password)
        {
            return true;
        }

        public Users GetUser(string email, string password)
        {
            Users users = new Users();
            users = usersDAO.FindUserByEmailPassword(email, password);
            if (string.IsNullOrEmpty(users.EmailAddress))
            {
                return null;
            }
            return users;
        }
    }
}
