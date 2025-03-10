using System;

namespace MS.SSquare.API.Models
{
    public class AuditResponseLog
    {
        public int? AuditResponseLogID { get; set; }
        public string ReferenceNo { get; set; }
        public int? AuditResponseID { get; set; }
        public int? ClientID { get; set; }
        public int? AuditTypeID { get; set; }
        public string Question { get; set; }
        public string QuestionDescription { get; set; }
        public DateTime? ResponseDateTime { get; set; }
        public string ResponseAction { get; set; }
        public int? AuditStatusID { get; set; }
        public string Applicability { get; set; }
        public string Criticality { get; set; }
        public string Likelihood { get; set; }
        public string PartnerComment { get; set; }
        public string ReviewerComment { get; set; }
        public int? RiskScore { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
    }
}

