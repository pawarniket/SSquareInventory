using System;

namespace MS.SSquare.API.Models
{
    public class UsersData
    {
        public int UserID { get; set; }

        public string Cinema_strID { get; set; }

        public int UserRoleID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string ProfilePicturePath { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? IsActive { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }
    }
}
