namespace Template.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Enums;

    public class UserClaim
    {
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public ClaimType Claim { get; set; }

        public UserClaim()
        {
        }

        public UserClaim(Guid userId, ClaimType claimType)
        {
            this.UserId = userId;
            this.Claim = claimType;
        }
    }
}