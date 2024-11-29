﻿using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain;
using API.Services.Interfaces.DataServices;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Concrete
{
    public class UserDataRepository : DataRepository<User>, IUserDataRepository
    {
        public UserDataRepository(ECommerceDbContext context) : base(context, context.Users) { }
    }
}
