

namespace GST.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson;

    public partial class BILLM
    {
        public ObjectId _id { get; set; }
        public int BILLID { get; set; }
        public string BID { get; set; }
        public string BILL_NO { get; set; }
        public Nullable<System.DateTime> BDATE { get; set; }
        public string CUST { get; set; }
        public string HSN_NO { get; set; }
        public string PARTI { get; set; }
        public string PQTY { get; set; }
        public string SQTY { get; set; }
        public string MRP { get; set; }
        public string SPRICE { get; set; }
        public string TOTAL { get; set; }
        public string IGST { get; set; }
        public string IGSTR { get; set; }
        public string CGST { get; set; }
        public string CGSTR { get; set; }
        public string SGST { get; set; }
        public string SGSTR { get; set; }
        public string TVAL { get; set; }
        public string STOT { get; set; }
        public string UNIT { get; set; }
        public string USER1 { get; set; }
        public string MODE { get; set; }
        public Nullable<decimal> BAMT { get; set; }
    }
}
