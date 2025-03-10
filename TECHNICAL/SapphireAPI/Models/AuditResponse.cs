using System;

namespace MS.SSquare.API.Models
{
    public class AuditResponse
    {
        public int? AuditResponseID { get; set; }
        public int? ReferenceNo { get; set; }
        public int? ClientID { get; set; }
        public int? VendorID { get; set; }
        public int? AuditTypeID { get; set; }
        public int? QuestionGroupID { get; set; }
        public int IsCustom { get; set; }


        public string Question { get; set; }
        public string @QuetionGroupName { get; set; }
        public string QuestionGroupName { get; set; }

        public string QuestionDescription { get; set; }
        public DateTime? ResponseDateTime { get; set; }
        public string ResponseAction { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Applicability { get; set; }
        public string Criticality { get; set; }
        public int Likelihood { get; set; }

        public int Impact { get; set; }

        public string PartnerComment { get; set; }
        public string ReviewerComment { get; set; }
        public int? RiskScore { get; set; }
        public int? ApprovedBy { get; set; }
        public int? CreatedBy { get; set; }
        public int? AuditStatusID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ReviewedBy { get; set; }
        public string? Attachments { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? ReviewStatus { get; set; }
        public string? AttachmentRequired { get; set; }
    }
}

