using System;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.BovinueSystem.Domain.Services;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Application.Internal.CommandServices
{
    public class BovinueHealthRecordCommandService : IBovinueHealthRecordCommandService
    {
        private readonly IBovinueHealthRecordRepository _healthRecordRepository;
        private readonly IBovinueRepository _bovinueRepository;
        private readonly IBovinueCattleHealthRecordRepository _cattleHealthRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BovinueHealthRecordCommandService(
            IBovinueHealthRecordRepository healthRecordRepository,
            IBovinueRepository bovinueRepository,
            IBovinueCattleHealthRecordRepository cattleHealthRepository,
            IUnitOfWork unitOfWork)
        {
            _healthRecordRepository = healthRecordRepository;
            _bovinueRepository = bovinueRepository;
            _cattleHealthRepository = cattleHealthRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BovinueHealthRecord> Handle(CreateBovinueHealthRecordCommand command)
        {
            // Validate bovinue exists
            var bovinue = await _bovinueRepository.GetByIdAsync(command.bovinueId);
            if (bovinue == null || bovinue.deleted)
                throw new InvalidOperationException($"Bovinue {command.bovinueId} not found or deleted");
            
            // Validate cattle health record exists
            var cattleHealth = await _cattleHealthRepository.GetByIdAsync(command.bovinueCHRId);
            if (cattleHealth == null || cattleHealth.deleted)
                throw new InvalidOperationException($"Cattle health record {command.bovinueCHRId} not found");
            
            // Business rule: Check for duplicate active vaccinations
            var hasActiveVaccination = await _healthRecordRepository
                .HasActiveVaccinationAsync(command.bovinueId, command.bovinueCHRId);
            
            if (hasActiveVaccination && cattleHealth.Frequency == 0) // Once in lifetime
                throw new InvalidOperationException($"Vaccination '{cattleHealth.ActivityName}' can only be applied once");
            
            var healthRecord = new BovinueHealthRecord(command);
            
            await _healthRecordRepository.AddAsync(healthRecord);
            await _unitOfWork.CompleteAsync();
            
            return healthRecord;
        }

        public async Task<BovinueHealthRecord> Handle(UpdateBovinueHealthRecordCommand command)
        {
            var healthRecord = await _healthRecordRepository.GetByIdAsync(command.id);
            
            if (healthRecord == null)
                throw new InvalidOperationException($"Health record {command.id} not found");
            
            if (healthRecord.deleted)
                throw new InvalidOperationException($"Cannot update deleted health record {command.id}");
            
            // Validate new cattle health record if changed
            if (command.bovinueCHRId != healthRecord.BovinueCHRId)
            {
                var cattleHealth = await _cattleHealthRepository.GetByIdAsync(command.bovinueCHRId);
                if (cattleHealth == null || cattleHealth.deleted)
                    throw new InvalidOperationException($"Cattle health record {command.bovinueCHRId} not found");
            }
            
            healthRecord.UpdateFromCommand(command);
            
            // EF Core tracks changes automatically, no need for UpdateAsync
            await _unitOfWork.CompleteAsync();
            
            return healthRecord;
        }

        public async Task Handle(DeleteBovinueHealthRecordCommand command)
        {
            var healthRecord = await _healthRecordRepository.GetByIdAsync(command.id);
            
            if (healthRecord == null)
                throw new InvalidOperationException($"Health record {command.id} not found");
            
            healthRecord.DeleteFromCommand(command);
            
            // EF Core tracks changes automatically, no need for UpdateAsync
            await _unitOfWork.CompleteAsync();
        }
    }
}