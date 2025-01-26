namespace Rubik_Market.Domain.ViewModels.User.Areas
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string Email { get; set; }
        public string? NationalCode { get; set; }
        public string? CellPhoneNumber { get; set; }
        public string? HousePhoneNumber { get; set; }
        public string? CardNumberForRejectMoney { get; set; }
        public string? BirthDate { get; set; }
        public DateTime UserCreateDate { get; set; }
        public string? AddOrEdit { get; set; }
    }

    public class UserComponentViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        public string ProfileIsForEdit { get; set; }
    }

    public class UserPanelComponentViewModel
    {
        public string? FullName { get; set; }
        public bool? ProfileIsForEdit { get; set; }
    }

    public enum DeleteProfileResult
    {
        Success,
        UserNotFound,
        Error
    }

}
