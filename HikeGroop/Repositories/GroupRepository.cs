﻿using HikeGroop.Interfaces;
using HikeGroop.Models;
using Microsoft.EntityFrameworkCore;

namespace HikeGroop.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
           _context = context;
        }

        public async Task<bool> Add(Group group)
        {
            _context.Add(group);
            return await Save();
        }

        public async Task<bool> Delete(Group group)
        {
            _context.Remove(group);
            return await Save();
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
          return await  _context.Groups
                .Include(g => g.Address)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupsByCity(string city)
        {
            return await _context.Groups.Where(g=> g.Address.City
            .Contains(city)).ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Group group)
        {
            _context.Update(group);
            return await Save();
        }
    }
}
