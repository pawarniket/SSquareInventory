using System;

namespace MS.SSquare.API.Models
{
    public class MemberShip
    {
        public int? MemberShipTypeID { get; set; }
        public string MemberShipName { get; set; }
        public decimal? MembershipFees { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
