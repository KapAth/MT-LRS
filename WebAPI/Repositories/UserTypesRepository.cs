using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Repository;
using Repositories.Repository.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class UserTypesRepository : IUserTypesRepository
    {
        private MTLRSDbContext _context;

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