using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories.Interfaces;
using WebAPI.Repositories.Repository;
using WebAPI.Repositories.Repository.Entities;

namespace WebAPI.Repositories
{
    public class UserTypesRepository : IUserTypesRepository
    {
        private readonly MTLRSDbContext _context;

        public UserTypesRepository(MTLRSDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserType>> GetAllUserTypesAsync()
        {
            var usersTypes = await _context.UserType.ToListAsync();

            return usersTypes;
        }

        public async Task<UserType> GetUserTypeAsync(int id)
        {
            var userType = await _context.UserType.FindAsync(id);

            if (userType == null)
            {
                throw new ArgumentException("User Type not found");
            }

            return userType;
        }
    }
}