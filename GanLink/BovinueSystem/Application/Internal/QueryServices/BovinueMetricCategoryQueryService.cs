using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.BovinueSystem.Domain.Services;

namespace GanLink.BovinueSystem.Application.Internal.QueryServices
{
    public class BovinueMetricCategoryQueryService : IBovinueMetricCategoryQueryService
    {
        private readonly IBovinueMetricCategoryRepository _categoryRepository;

        public BovinueMetricCategoryQueryService(IBovinueMetricCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BovinueMetricCategory?> Handle(GetBovinueMetricCategoryByIdQuery query)
        {
            return await _categoryRepository.GetByIdAsync(query.id);
        }

        public async Task<IEnumerable<BovinueMetricCategory>> Handle(GetAllBovinueMetricCategoriesQuery query)
        {
            return await _categoryRepository.GetAllActiveAsync();
        }
    }
}