using System;

namespace MS.SSquare.API.Models
{
    public class Logs
    {
        public int LogID { get; set; }
        public int OrderID { get; set; }
        public string APIMethodName { get; set; }
        public string APIRequest { get; set; }
        public string APIResponse { get; set; }
        public DateTime? APIRequestDateTime { get; set; }
        public DateTime? APIResponseDateTime { get; set; }
    }
}
