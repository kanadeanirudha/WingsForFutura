using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Coditech.ViewModel
{
    public class ClientMasterViewModel : BaseViewModel
    {
        public ClientMasterViewModel()
        {
            ClientMasterList = new List<SelectListItem>();
        }
        public List<SelectListItem> ClientMasterList { get; set; }
        public int UserMasterId { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile number must be 10 digits long")]
        public string MobileNumber { get; set; }

        [Required]
        public string EmailId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DOB { get; set; } = DateTime.Now;
        public string Nationality { get; set; }
        public string UniqueNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public byte AdminRoleMasterId { get; set; }
        public string RoleName { get; set; }
        public List<string> FormAccessList { get; set; }
    }
}
