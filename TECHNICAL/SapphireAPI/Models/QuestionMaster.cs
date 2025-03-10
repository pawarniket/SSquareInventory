using System;

namespace MS.SSquare.API.Models
{
    public class QuestionMaster
    {
        public int? QuestionMasterID { get; set; }

        public int? AuditTypeID { get; set; }
        public string Question_details { get; set; }
        public string QuestionDescription { get; set; }
        public int? QuestionGroupID { get; set; }
        public int? Impact { get; set; }
        public int? Likelihood { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int OrderBy { get; set; }
        public string AttachmentRequired { get; set; }
    }
}
