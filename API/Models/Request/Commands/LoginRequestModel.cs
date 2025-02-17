﻿using API.Models.Control.ResultModels;
using FluentResults;
using MediatR;

namespace API.Models.Request.Commands
{
    public class LoginRequestModel : IRequest<InnerResult<string>>
    {
        public LoginRequestModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
