﻿using Auth.Server.Models;

namespace Auth.Server.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int id);
    }
}
