using System;

namespace MS.SSquare.API.Models
{
    public class CustomQuestions
    {
        public int? CustomQuestionID { get; set; }
        public string CustomQuestion { get; set; }
        public string CustomQuestionDescription { get; set; }
        public int? ClientID { get; set; }
        public int? AuditTypeID { get; set; }
        public int? QuestionType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
