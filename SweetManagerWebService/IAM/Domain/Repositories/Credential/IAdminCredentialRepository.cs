﻿using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;

namespace SweetManagerWebService.IAM.Domain.Repositories.Credential;

public interface IAdminCredentialRepository
{
    Task<AdminCredential> FindByIdAsync(int id);
}