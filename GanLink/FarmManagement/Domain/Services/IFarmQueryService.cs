using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Domain.Models.Queries;

namespace GanLink.FarmManagement.Domain.Services;

public interface IFarmQueryService
{
    Task<IEnumerable<Farm>> Handle(GetAllFarmsQuery query);
    Task<Farm?> Handle(GetFarmByIdQuery query);
    Task<Farm?> Handle(GetFarmByUserId query);
}