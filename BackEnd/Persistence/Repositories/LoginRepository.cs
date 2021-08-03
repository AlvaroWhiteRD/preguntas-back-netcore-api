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
    public class LoginRepository: ILoginRepository
    {
        private readonly AplicationDbContext _context;
        public LoginRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Users> ValidateUser(Users usuario)
        {
            var user = await _context.Users.Where(x => x.Username == usuario.Username
                                                && x.Password == usuario.Password).FirstOrDefaultAsync();
            return user;
        }
    }
}
