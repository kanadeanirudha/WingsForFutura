using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Coditech.ViewModel
{
    public class UserMasterViewModel : BaseViewModel
    {
        public UserMasterViewModel()
        {
            AdminRoleMasterList = new List<SelectListItem>();
        }
        public List<SelectListItem> AdminRoleMasterList { get; set; }
        public int UserMasterId { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; } = DateTime.Now;
        public string Nationality { get; set; }
        public string UniqueNumber { get; set; }
        public bool IsActive { get; set; }
        public short AdminRoleMasterId { get; set; }
        public bool IsDocumentApprovalAuthority { get; set; }
        public string RoleName { get; set; }
        //[Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        //[MaxLength(100)]
        //[MinLength(8)]
        //[Required(ErrorMessage = "Please Enter The New Password")]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&#])[A-Za-z\\d@$!%*?&#]{8,}$",
        //ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character (@$!%*?&#).")]
        //[DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        //[MaxLength(100)]
        //[MinLength(8)]
        //[Required(ErrorMessage = "Confirm Password Is Required")]
        //[DataType(DataType.Password)]
        //[System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirm password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


    }
}
