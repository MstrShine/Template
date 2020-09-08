namespace Template.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Template.DAL.GenericEntity;

    public class User : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [EmailAddress]
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string MiddleName { get; set; }

        public string Lastname { get; set; }

        public string Password { get; set; }

        public User()
        {
        }

        public User(string email, string firstname, string? middleName, string lastname, string password)
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.Firstname = firstname;
            this.MiddleName = middleName;
            this.Lastname = lastname;
            this.Password = password;
        }
    }
}