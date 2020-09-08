namespace Template.DAL.GenericEntity
{
    using System;

    public interface IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}