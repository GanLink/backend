using System;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.BovinueSystem.Domain.Services;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Application.Internal.CommandServices
{
    public class BovinueCommandService : IBovinueCommandService
    {
        private readonly IBovinueRepository _bovinueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BovinueCommandService(
            IBovinueRepository bovinueRepository,
            IUnitOfWork unitOfWork)
        {
            _bovinueRepository = bovinueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Bovinue> Handle(CreateBovinueCommand command)
        {
            var bovinue = new Bovinue(command);
            
            await _bovinueRepository.AddAsync(bovinue);
            await _unitOfWork.CompleteAsync();
            
            return bovinue;
        }

        public async Task<Bovinue> Handle(UpdateBovinueCommand command)
        {
            var bovinue = await _bovinueRepository.GetByIdAsync(command.id);
            
            if (bovinue == null)
                throw new InvalidOperationException($"Bovinue with id {command.id} not found");
            
            if (bovinue.deleted)
                throw new InvalidOperationException($"Cannot update deleted Bovinue {command.id}");
            
            bovinue.UpdateFromCommand(command);
            
            // EF Core tracks changes automatically, no need for UpdateAsync
            await _unitOfWork.CompleteAsync();
            
            return bovinue;
        }

        public async Task Handle(DeleteBovinueCommand command)
        {
            var bovinue = await _bovinueRepository.GetByIdAsync(command.id);
            
            if (bovinue == null)
                throw new InvalidOperationException($"Bovinue with id {command.id} not found");
            
            // Business rule: Cannot delete if has open health records
            var hasOpenRecords = await _bovinueRepository.HasOpenHealthRecordsAsync(command.id);
            if (hasOpenRecords)
                throw new InvalidOperationException($"Cannot delete Bovinue {command.id} with open health records");
            
            bovinue.DeleteFromCommand(command);
            
            // EF Core tracks changes automatically, no need for UpdateAsync
            await _unitOfWork.CompleteAsync();
        }
    }
}