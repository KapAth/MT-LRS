using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories.Interfaces;
using WebAPI.Repositories.Repository;
using WebAPI.Repositories.Repository.Entities;

namespace WebAPI.Repositories
{
    public class UserTitlesRepository : IUserTitlesRepository
    {
        private readonly MTLRSDbContext _context;

        public UserTitlesRepository(MTLRSDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<UserTitle>> GetAllUserTitlesAsync()
        {
            var usersTitles = await _context.UserTitle.ToListAsync();

            return usersTitles;
        }

        public async Task<UserTitle> GetUserTitleByIdAsync(int id)
        {
            var userTitle = await _context.UserTitle.FindAsync(id);

            if (userTitle == null)
            {
                throw new ArgumentException("User Title not found");
            }

            return userTitle;
        }
    }
}