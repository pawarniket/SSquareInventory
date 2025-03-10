using System;

namespace MS.SSquare.API.Models
{
    public class UserRoleForms
    {
        public int UserRoleFormID { get; set; }
        public int? MenuHeaderID { get; set; }
        public char MenuSubHeaderID { get; set; }
        public char MenuFormID { get; set; }
        public int? UserRoleID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
