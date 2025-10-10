using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;

namespace GanLink.BovinueSystem.Domain.Services
{
    public interface IBovinueQueryService
    {
        Task<Bovinue?> Handle(GetBovinueByIdQuery query);
        Task<IEnumerable<Bovinue>> Handle(GetAllBovinuesQuery query);
        Task<IEnumerable<Bovinue>> Handle(GetBovinuesByFarmIdQuery query);
        Task<IEnumerable<Bovinue>> Handle(GetBovinuesPagedQuery query);
        Task<IEnumerable<Bovinue>> Handle(SearchBovinuesQuery query);
    }
}