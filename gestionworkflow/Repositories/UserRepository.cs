using gestionworkflow.Context;
using gestionworkflow.Models;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace gestionworkflow.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextName _dbContext;
     
        public UserRepository(DbContextName dbContext )
        {
            _dbContext = dbContext;
          
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email.ToLower());
        }


        public async Task<User> GetAvanceQuery(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task UpdateUserAsync(User user)
        //{
        //    _context.Entry(user).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}
        public async Task DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserStatusAsync(int Id, string status)
        {
            var user = await _dbContext.Users.FindAsync(Id);

            if (user != null)
            {
                user.Status = status;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<User>> GetPendingUsersAsync()
        {
            return await _dbContext.Users.Where(u => u.Status == "Pending").ToListAsync();
        }

        public async Task AddDemandeCongeAsync(DemandeConge demandeConge)
        {
            await _dbContext.DemandeConges.AddAsync(demandeConge);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddDemandeAvanceAsync(DemandeAvance demandeAvance)
        {
            await _dbContext.DemandeAvances.AddAsync(demandeAvance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DemandeConge>> GetPendingDemandeAsync()
        {
            return await _dbContext.DemandeConges.Where(u => u.Statut == "Pending").ToListAsync();
        }

        public async  Task<DemandeConge> GetDemandeCongeByIdAsync(int id)
        {
            return await _dbContext.DemandeConges.FindAsync(id);
        }

        public async  Task UpdateDemandeAsync(DemandeConge demande)
        {
            _dbContext.DemandeConges.Update(demande);
            await _dbContext.SaveChangesAsync();
        }

        public async  Task<IEnumerable<DemandeConge>> GetPendingDemandesAsync()
        {

            return await _dbContext.DemandeConges.Where(u => u.Statut == "Pending").ToListAsync();
        }

        public async Task UpdateAvanceAsync(DemandeAvance avance)
        {
            _dbContext.DemandeAvances.Update(avance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DemandeAvance>> GetPendingAvancesAsync()
        {
            return await _dbContext.DemandeAvances.Where(u => u.Statut == "Pending").ToListAsync();
        }

        public  async Task<DemandeAvance> GetDemandeAvanceByIdAsync(int id)
        {
            return await _dbContext.DemandeAvances.FindAsync(id);
        }
    }
}
