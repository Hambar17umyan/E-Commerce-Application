﻿using FluentResults;
using MediatR;

namespace API.Models.Request
{
    public class ChangeInventoryQuantityManualRequestModel : IRequest<Result>
    {
        public int InventoryId { get; set; }
        public int? NewQuantity { get; set; }
        public int? AddQuantity { get; set; }
    }
}
