using System;

namespace MS.SSquare.API.Models
{
    public class AuditType
    {
        public int? AuditTypeID { get; set; }

        public string AuditName { get; set; }
        public string AuditDescription { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? Created_Date { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }



    }
}
