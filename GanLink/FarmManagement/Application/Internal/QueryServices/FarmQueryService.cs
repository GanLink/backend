using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Domain.Models.Queries;
using GanLink.FarmManagement.Domain.Repositories;
using GanLink.FarmManagement.Domain.Services;

namespace GanLink.FarmManagement.Application.Internal.QueryServices
{
    // C# 12: primary constructor (puedes mantenerlo así)
    public class FarmQueryService(IFarmRepository farmRepository) : IFarmQueryService
    {
        public Task<Farm?> Handle(GetFarmByIdQuery query)
        {
            // Ajusta 'query.id' si tu record usa otra propiedad (p. ej., 'Id')
            return farmRepository.GetByIdAsync(query.id);
        }

        public Task<IReadOnlyList<Farm>> Handle(GetAllFarmsByUserIdQuery query)
        {
            // Ajusta 'query.userId' si tu record usa 'UserId'
            return farmRepository.ListByUserIdAsync(query.UserId);
        }
    }
}