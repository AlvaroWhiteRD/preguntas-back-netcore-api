﻿using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.IServices
{
    public interface IUsuarioService
    {
        Task SaveUser(Users usuario);
        Task<bool> ValidateExistence(Users usuario);
        Task<Users> ValidatePassword(int idUsuario, string passwordAnterior);
        Task UpdatePassword(Users usuario);
    }
}
