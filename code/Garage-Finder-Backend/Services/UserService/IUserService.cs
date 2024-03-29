﻿using DataAccess.DTO.User;
using DataAccess.DTO.User.RequestDTO;
using DataAccess.DTO.User.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserService
{
    public interface IUserService
    {
        UserInfor Login(LoginModel loginModel);
        UserInfor LoginGG(string accessToken);
        void Logout(int userId);
        void Register(UserRegister registerUser);
        void UpdateUser(UserUpdateDTO usersDTO, int userID);
        bool SendPhoneCode(string phoneNumber);
        UserInfor Get(int userId);
        void ChangePassword(int userId, string oldPassword, string newPassword);
        dynamic RefreshToken(string refreshToken);
        void ForgotPassword(ForgotPassDTO forgotPassModel);
        public UsersDTO GetUserByID(int id);
    }
}
