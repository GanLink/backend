namespace GanLink.BovinueSystem.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

public interface IBovinueRepository : IBaseRepository<Bovinue>
{
    Task<Bovinue?> GetByIdAsync(long id);
    Task<ICollection<Bovinue>> GetByFarmIdAsync(long farmId);

    // Apoya regla de negocio: evitar transferencias/borrados con salud abierta
    Task<bool> HasOpenHealthRecordsAsync(long bovinueId);
}

