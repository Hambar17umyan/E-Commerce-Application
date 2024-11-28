﻿using Microsoft.AspNetCore.Identity;

namespace API.Models.Domain
{
    public class Role : IdentityRole<int>
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}