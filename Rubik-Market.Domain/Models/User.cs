using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.Models
{
    public class User:BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool isAdmin { get; set; } = false;
        public string? ConfirmCode { get; set; }
        public bool isActive { get; set; } = false;

        #region Relations

         public List<UserProfileInfo>? UserProfileInfo { get; set; }
         public List<Address>? Addresses { get; set; }

        #endregion

    }
}
