namespace Template.DAL.GenericEntity
{
    using System;

    public class Entity : IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}