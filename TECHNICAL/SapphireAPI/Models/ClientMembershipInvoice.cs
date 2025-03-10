using System;

namespace MS.SSquare.API.Models
{
    public class ClientMembershipInvoice
    {
        public int ClientMembershipID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? MemberShipStartDate { get; set; }
        public DateTime? MemberShipExpiryDate { get; set; }
        public int? ClientID { get; set; }
        public int? MemberShipID { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? SGST { get; set; }
        public decimal? CGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal? InvoiceTotal { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}


