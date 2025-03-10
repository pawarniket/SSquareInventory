namespace MS.SSquare.API.Models
{
    public class Comments
    {
        public int? CommentID { get; set; }
        public int AuditResponseID { get; set; }
        public int ClientAuditID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }
        public int CommentBy { get; set; }
        public int CreatedBy { get; set; }

    }
}
