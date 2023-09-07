using gestionworkflow.Models;
using gestionworkflow.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace gestionworkflow.Repositories
{
    public interface IUserRepository
    {
       
            Task<IEnumerable<User>> GetAllUsersAsync();
            Task<User> GetAvanceQuery(int id);
 
        Task CreateUserAsync(User user);
            Task UpdateUserAsync(User user);
   
        Task DeleteUserAsync(int id);
            Task<User> GetUserByEmailAsync(string email);
        Task  UpdateUserStatusAsync(int Id, string status);
        Task<IEnumerable<User>> GetPendingUsersAsync();

        //demande conges
        Task<DemandeConge> GetDemandeCongeByIdAsync(int id);
        Task UpdateDemandeAsync(DemandeConge demande);
        Task<IEnumerable<DemandeConge>> GetPendingDemandesAsync();
        Task<IEnumerable<DemandeConge>> GetPendingDemandeAsync();
        Task AddDemandeCongeAsync(DemandeConge demandeConge);
        //demande avance 
        Task AddDemandeAvanceAsync(DemandeAvance demandeAvance);
        Task UpdateAvanceAsync(DemandeAvance demande);
        Task<IEnumerable<DemandeAvance>> GetPendingAvancesAsync();
        Task<DemandeAvance> GetDemandeAvanceByIdAsync(int id);
    }
}

