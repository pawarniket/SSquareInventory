using System;

namespace MS.SSquare.API.Models
{
    public class Banners
    {
        public int BannerID { get; set; }
        public string Cinema_strID { get; set; }
        public string BannerImagePath { get; set; }
        public string BannerType { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
