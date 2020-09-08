namespace Template.Models.User
{
    using System;

    public class UserManagementViewModel
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string? MiddleName { get; set; }

        public string Lastname { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public UserManagementViewModel() { }

        public UserManagementViewModel(Guid id, string firstname, string? middleName, string lastname, string email)
        {
            this.Id = id;
            this.Firstname = firstname;
            this.MiddleName = middleName;
            this.Lastname = lastname;
            this.Email = email;
            this.Fullname = this.MiddleName == null ?
                                $"{this.Firstname} {this.Lastname}" :
                                $"{this.Firstname} {this.MiddleName} {this.Lastname}";
        }
    }
}