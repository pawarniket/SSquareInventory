using System;

namespace MS.SSquare.API.Models
{
    public class Vendor
    {
        public int VendorID { get; set; }
        public int? ClientID { get; set; }
        public string VendorName { get; set; }
        public int? IndustryID { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string Pincode { get; set; }
        public string GSTNo { get; set; }
        public string Regulated { get; set; }
        public string Certified { get; set; }
        public string OfficeAddress { get; set; }
        public string ContactPerson_name { get; set; }

        public string ContactPerson_Lastname { get; set; }

        public string ContactPerson_designation { get; set; }
        public string ContactPerson_mobile { get; set; }
        public bool? IsCritical { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created_Date { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
