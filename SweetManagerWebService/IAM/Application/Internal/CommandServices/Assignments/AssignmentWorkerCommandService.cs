﻿using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Commands.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Repositories.Assignments;
using SweetManagerWebService.IAM.Domain.Services.Assignments;

namespace SweetManagerWebService.IAM.Application.Internal.CommandServices.Assignments;

public class AssignmentWorkerCommandService(IAssignmentWorkerRepository assignmentWorkerRepository, 
    IUnitOfWork unitOfWork) : IAssignmentWorkerCommandService
{
    public async Task<bool> Handle(CreateAssignmentWorkerCommand command)
    {
        try
        {

            var verificationWorkerId = await assignmentWorkerRepository.FindByWorkerIdAsync(command.WorkersId);
            
            if (!verificationWorkerId.Any())
                throw new Exception($"There's no Assignment with the given worker id: {command.WorkersId}");

            verificationWorkerId = await assignmentWorkerRepository.FindByAdminIdAsync(command.AdminsId);
            
            if (!verificationWorkerId.Any())
                throw new Exception($"There's no Assignment with the given admin id: {command.AdminsId}");

            verificationWorkerId = await assignmentWorkerRepository.FindByWorkerAreaIdAsync(command.WorkersAreasId);

            if (verificationWorkerId.Any())
                throw new Exception($"There's no Assignment with the given worker area id: {command.WorkersAreasId}");
            
            if (command.WorkersId is 0)
            {
                await assignmentWorkerRepository.AddAsync(new AssignmentWorker(command.WorkersAreasId,
                    null, command.AdminsId, command.StartDate, command.FinalDate, command.State));
            }
            else
            {
                await assignmentWorkerRepository.AddAsync(new AssignmentWorker(command.WorkersAreasId,
                    command.WorkersId, null, command.StartDate, command.FinalDate, command.State));
            }
            
            await unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}