using System;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.BovinueSystem.Domain.Services;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Application.Internal.CommandServices
{
    public class BovinueMetricCommandService : IBovinueMetricCommandService
    {
        private readonly IBovinueMetricRepository _metricRepository;
        private readonly IBovinueRepository _bovinueRepository;
        private readonly IBovinueMetricParameterRepository _parameterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BovinueMetricCommandService(
            IBovinueMetricRepository metricRepository,
            IBovinueRepository bovinueRepository,
            IBovinueMetricParameterRepository parameterRepository,
            IUnitOfWork unitOfWork)
        {
            _metricRepository = metricRepository;
            _bovinueRepository = bovinueRepository;
            _parameterRepository = parameterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BovinueMetric> Handle(CreateBovinueMetricCommand command)
        {
            // Validate bovinue exists
            var bovinue = await _bovinueRepository.GetByIdAsync(command.bovinueId);
            if (bovinue == null || bovinue.deleted)
                throw new InvalidOperationException($"Bovinue {command.bovinueId} not found or deleted");
            
            // Validate parameter exists (datasets don't need deleted check)
            var parameter = await _parameterRepository.GetByIdAsync(command.bovinueMPId);
            if (parameter == null)
                throw new InvalidOperationException($"Metric parameter {command.bovinueMPId} not found");
            
            // Business rule: Check for duplicate metric on same date
            var exists = await _metricRepository.ExistsForDateAsync(
                command.bovinueId, 
                command.bovinueMPId, 
                command.date);
            
            if (exists)
                throw new InvalidOperationException(
                    $"Metric for parameter {parameter.Parameter} already exists for date {command.date:yyyy-MM-dd}");
            
            // Validate quantity value
            if (command.quantity < 0)
                throw new ArgumentException("Metric quantity cannot be negative");
            
            var metric = new BovinueMetric(command);
            
            await _metricRepository.AddAsync(metric);
            await _unitOfWork.CompleteAsync();
            
            return metric;
        }

        public async Task<BovinueMetric> Handle(UpdateBovinueMetricCommand command)
        {
            var metric = await _metricRepository.GetByIdAsync(command.id);
            
            if (metric == null)
                throw new InvalidOperationException($"Metric {command.id} not found");
            
            if (metric.deleted)
                throw new InvalidOperationException($"Cannot update deleted metric {command.id}");
            
            // Validate new parameter if changed
            if (command.bovinueMPId != metric.BovinueMPId)
            {
                var parameter = await _parameterRepository.GetByIdAsync(command.bovinueMPId);
                if (parameter == null || parameter.deleted)
                    throw new InvalidOperationException($"Metric parameter {command.bovinueMPId} not found");
                
                // Check for duplicate if date or parameter changed  
                var exists = await _metricRepository.ExistsForDateAsync(
                    metric.BovinueId, 
                    command.bovinueMPId, 
                    command.date);
                
                if (exists)
                    throw new InvalidOperationException(
                        $"Metric for this parameter already exists for date {command.date:yyyy-MM-dd}");
            }
            
            // Validate quantity value
            if (command.quantity < 0)
                throw new ArgumentException("Metric quantity cannot be negative");
            
            metric.UpdateFromCommand(command);
            
            // EF Core tracks changes automatically, no need for UpdateAsync
            await _unitOfWork.CompleteAsync();
            
            return metric;
        }

        public async Task Handle(DeleteBovinueMetricCommand command)
        {
            var metric = await _metricRepository.GetByIdAsync(command.id);
            
            if (metric == null)
                throw new InvalidOperationException($"Metric {command.id} not found");
            
            metric.DeleteFromCommand(command);
            
            // EF Core tracks changes automatically, no need for UpdateAsync
            await _unitOfWork.CompleteAsync();
        }
    }
}