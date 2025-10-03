namespace GanLink.BovinueSystem.Domain.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;

public interface IBovinueQueryService
{
    Task<Bovinue?> Handle(GetBovinueByIdQuery query);
    Task<IEnumerable<Bovinue>> Handle(GetAllBovinuesQuery query);
    Task<IEnumerable<Bovinue>> Handle(GetBovinuesByFarmIdQuery query);
    // Si luego quieres paginación/búsqueda:
    // Task<IEnumerable<Bovinue>> Handle(GetBovinuesPagedQuery query);
    // Task<IEnumerable<Bovinue>> Handle(SearchBovinuesQuery query);
}

