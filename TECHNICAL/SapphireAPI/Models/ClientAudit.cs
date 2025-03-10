using System;

namespace MS.SSquare.API.Models
{
    public class ClientAudit
    {
        public int? ClientAuditID { get; set; }
        public int? ClientID { get; set; }

        public int? VendorID { get; set; }

        public int? AuditTypeID { get; set; }
        public int? ReviewBy { get; set; }
        public int? ApprovedBy { get; set; }
        public int? AuditStatusID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ResponseCompletedDate { get; set; }

     public DateTime? DueDate { get; set; }
        public Questions[] Questions { get; set; }

    }
    public class Questions
    {
        public int? ClientID { get; set; }
        public int? VendorID { get; set; }
        public int? AuditTypeID { get; set; }
        public string QuestionGroupName { get; set; }
        public string Question { get; set; }
        public string? QuestionDescription { get; set; }
        public int? QuestionGroupID { get; set; }
        public int IsCustom { get; set; }
        public string Criticality { get; set; }
        public int Likelihood { get; set; }
        public int Impact { get; set; }
        public int? ReferenceNo { get; set; }
        public string? AttachmentRequired { get; set; }
        public int? AuditStatusID { get; set; }




    }
}
