using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.BovinueSystem.Domain.Services;

namespace GanLink.BovinueSystem.Application.Internal.QueryServices
{
    public class BovinueQueryService : IBovinueQueryService
    {
        private readonly IBovinueRepository _bovinueRepository;

        public BovinueQueryService(IBovinueRepository bovinueRepository)
        {
            _bovinueRepository = bovinueRepository;
        }

        public async Task<Bovinue?> Handle(GetBovinueByIdQuery query)
        {
            var bovinue = await _bovinueRepository.GetByIdAsync(query.id);
            return (bovinue != null && !bovinue.deleted) ? bovinue : null;
        }

        public async Task<IEnumerable<Bovinue>> Handle(GetAllBovinuesQuery query)
        {
            var bovinues = await _bovinueRepository.ListAsync();
            return bovinues.Where(b => !b.deleted);
        }

        public async Task<IEnumerable<Bovinue>> Handle(GetBovinuesByFarmIdQuery query)
        {
            var bovinues = await _bovinueRepository.GetByFarmIdAsync(query.farmId);
            return bovinues.Where(b => !b.deleted);
        }

        public async Task<IEnumerable<Bovinue>> Handle(GetBovinuesPagedQuery query)
        {
            var bovinues = await _bovinueRepository.ListAsync();
            return bovinues
                .Where(b => !b.deleted)
                .Skip((query.page - 1) * query.pageSize)
                .Take(query.pageSize);
        }

        public async Task<IEnumerable<Bovinue>> Handle(SearchBovinuesQuery query)
        {
            var bovinues = await _bovinueRepository.ListAsync();
            var result = bovinues.Where(b => !b.deleted);

            // Filter by keyword if provided (search in multiple fields)
            if (!string.IsNullOrWhiteSpace(query.keyword))
            {
                result = result.Where(b => 
                    b.Id.ToString().Contains(query.keyword) ||
                    b.FarmId.ToString().Contains(query.keyword)
                );
            }

            // Apply pagination
            result = result
                .Skip((query.page - 1) * query.pageSize)
                .Take(query.pageSize);

            return result;
        }
    }
}