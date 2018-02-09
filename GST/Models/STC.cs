namespace GST.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class STC
    {
        [Key]
        public int RID1 { get; set; }
        [DisplayName("RECORD NO")]
        public string RID { get; set; }
        [DisplayName("HSN NO")]
        public string HSN_NO { get; set; }
        [Required(ErrorMessage = "Please Fill The Record!", AllowEmptyStrings = false)]
        [DisplayName("PART NAME")]
        public string PARTI { get; set; }
        [Required(ErrorMessage = "Please Fill The Record!", AllowEmptyStrings = false)]
        public string MRP { get; set; }
        [Required(ErrorMessage = "Please Fill The Record!", AllowEmptyStrings = false)]
        public string QTY { get; set; }
        [DisplayName("ITEM TOTAL")]
        public string TOTAL { get; set; }
        [DisplayName("TAX RATE")]
        public string TAX { get; set; }
        [DisplayName("TAX VALUE")]
        public string TVAL { get; set; }
        [DisplayName("PURCAHSE PRICE")]
        public string PPRICE { get; set; }
        public string UNIT { get; set; }
        [DisplayName("SELL PRICE")]
        public string SPRICE { get; set; }
        [DisplayName("USER NAME")]
        public string USER1 { get; set; }
    }
}