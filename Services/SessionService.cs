using Microsoft.EntityFrameworkCore;
using BoulderingGymAPI.Data;
using BoulderingGymAPI.Models;

namespace BoulderingGymAPI.Services
{
    public class SessionService
    {
        private readonly GymDbContext _context;

        public SessionService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetAllSessions()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<Session> CreateSession(Session session)
        {
            _context.Sessions.Add(session);

            await _context.SaveChangesAsync();

            return session;
        }

        public async Task<bool> DeleteSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
                return false;

            _context.Sessions.Remove(session);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}