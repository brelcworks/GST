namespace GST.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson;

    public class USER
    {
        public ObjectId _id { get; set; }
        public int CID { get; set; }
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        [DisplayName("User Name")]
        public string uid { get; set; }
        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        [DisplayName("Password")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string pass { get; set; }
        [DisplayName("First Name")]
        public string fname { get; set; }
        [DisplayName("Last Name")]
        public string lname { get; set; }
    }
}
