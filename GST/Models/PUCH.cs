

namespace GST.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;


    public partial class PUCH
    {
        [Key]
        public int BID { get; set; }
        [Display(Name = "BILL DATE")]
        [Required]
        public System.DateTime BDATE { get; set; }
        [Display(Name = "BILL NO")]
        public string BNO { get; set; }
        [Display(Name = "CUSTOMER")]
        public string CUST { get; set; }
        [Display(Name = "GRAND TOTAL")]
        public Nullable<decimal> GTOT { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public Nullable<decimal> PAYMENT { get; set; }
        public string ADDRESS { get; set; }
        [Display(Name = "ROUND OFF")]
        public Nullable<decimal> ROUND { get; set; }
        [Display(Name = "NET TOTAL")]
        public Nullable<decimal> NTOT { get; set; }
        [Display(Name = "IGST")]
        public Nullable<decimal> IGST { get; set; }
        [Display(Name = "CGST")]
        public Nullable<decimal> CGST { get; set; }
        [Display(Name = "SGST")]
        public Nullable<decimal> SGST { get; set; }
        public string USER1 { get; set; }
        [Display(Name = "BILL MODE")]
        public string MODE { get; set; }
        [Display(Name = "GST NO")]
        public string VNO { get; set; }
        [Display(Name = "STATE CODE")]
        public string SNO { get; set; }
        [Display(Name = "STATE")]
        public string STATE { get; set; }
        public Nullable<decimal> CBILL { get; set; }
        public Nullable<decimal> BAPAY { get; set; }
        public string IGSTR { get; set; }
        public string CGSTR { get; set; }
        public string SGSTR { get; set; }
        public string BST { get; set; }
        public string BID1 { get; set; }
        public Nullable<decimal> BAMT { get; set; }
        public string STATECO { get; set; }
    }
}
