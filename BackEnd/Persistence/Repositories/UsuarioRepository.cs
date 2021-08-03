using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Repositories
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly AplicationDbContext _context;
        public UsuarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveUser(Users user) 
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateExistence(Users user)
        {
            var validateExistence = await _context.Users.AnyAsync(x => x.Username == user.Username);
            return validateExistence;
        }

        public async Task<Users> ValidatePassword(int userId, string backPassword)
        {
            var user = await _context.Users.Where(x => x.Id == userId && x.Password == backPassword).FirstOrDefaultAsync();
            return user;

        }

        public async Task UpdatePassword(Users user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
