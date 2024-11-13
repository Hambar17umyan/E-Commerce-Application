namespace E_Commerce.API.Models.DomainModels
{
    internal class Role
    {
        public Role(string name, string description, int priorityPoints)
        {
            Name = name;
            Description = description;
            PriorityPoints = priorityPoints;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Role() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PriorityPoints { get; set; }

    }
}
