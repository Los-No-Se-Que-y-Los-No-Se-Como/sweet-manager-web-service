﻿using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace SweetManagerWebService.IAM.Domain.Model.Aggregates;

public partial class Owner : IEntityWithCreatedUpdatedDate
{
    
    [Column("created_at")] public DateTimeOffset? CreatedDate { get; set; }
    
    [Column("updated_at")] public DateTimeOffset? UpdatedDate { get; set; }
}