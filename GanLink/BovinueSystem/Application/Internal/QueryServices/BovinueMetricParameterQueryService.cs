using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.BovinueSystem.Domain.Services;

namespace GanLink.BovinueSystem.Application.Internal.QueryServices
{
    public class BovinueMetricParameterQueryService : IBovinueMetricParameterQueryService
    {
        private readonly IBovinueMetricParameterRepository _parameterRepository;

        public BovinueMetricParameterQueryService(IBovinueMetricParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        public async Task<BovinueMetricParameter?> Handle(GetBovinueMetricParameterByIdQuery query)
        {
            return await _parameterRepository.GetByIdAsync(query.id);
        }

        public async Task<IEnumerable<BovinueMetricParameter>> Handle(GetAllBovinueMetricParametersQuery query)
        {
            return await _parameterRepository.GetAllActiveAsync();
        }

        public async Task<IEnumerable<BovinueMetricParameter>> Handle(GetBovinueMetricParametersByCategoryIdQuery query)
        {
            return await _parameterRepository.GetByCategoryIdAsync(query.categoryId);
        }
    }
}