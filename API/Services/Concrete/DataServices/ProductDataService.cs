using API.Data.Repositories.Interfaces;
using API.Models.Domain.Concrete;
using API.Services.Interfaces.DataServices;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Services.Concrete.DataServices
{
    public sealed class ProductDataService : DataService<Product>, IProductDataService
    {
        public ProductDataService(IProductDataRepository repo) : base(repo) { }
    }
}
