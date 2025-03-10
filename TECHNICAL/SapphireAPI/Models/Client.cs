using System;

namespace MS.SSquare.API.Controllers
{
    public class Client
    {
        public int? ClientID { get; set; }
        public int? ClientTypeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public String Country { get; set; }
        public int ParentClientID { get; set; }
        public int IndustryID { get; set; }
        public string Certified { get; set; }
        public string Regulated { get; set; }
        public string Pincode { get; set; }

        public string GSTNo { get; set; }
        public string ContactPersonname { get; set; }
        public string ContactPersonlastname { get; set; }

        public string ContactPersondesignation { get; set; }
        public string @ContactPersonmobile { get; set; }

        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string OfficeAddress { get; set; }
    }
    public class AllClient {
        public int ClientTypeId { get; set; }
        public string Name { get; set; }
        public int ParentClientID { get; set; }
        public Boolean IsActive { get; set; }

    }
}
