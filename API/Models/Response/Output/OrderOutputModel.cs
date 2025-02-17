﻿namespace API.Models.Response.Output
{
    public class OrderOutputModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public ICollection<LineItemOutputModel> LineItems { get; set; } = new List<LineItemOutputModel>();
    }
}
