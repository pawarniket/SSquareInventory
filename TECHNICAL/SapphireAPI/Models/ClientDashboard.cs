using System;

namespace MS.SSquare.API.Models
{
    public class Clientdashboards
    {
        public int? ClientID { get; set; }  // Optional parameter
        public int? VendorID{ get; set; }  // Optional parameter
        public bool? IsActive { get; set; }  // Optional parameter
    }
}
