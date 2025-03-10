using System;

namespace MS.SSquare.API.Models
{
    public class UserRoles
    {
        public int UserRoleID { get; set; }
        public string UserRoleName { get; set; }
        public string UserDescription { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
