



namespace GST.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using GST.Models;
    public class DBCTX : DbContext
    {
        public DBCTX()
            : base("name=DBCTX")
        {
            Database.SetInitializer<DBCTX>(new DropCreateDatabaseIfModelChanges<DBCTX>());
        }
        public DbSet<USER1> USER1 { get; set; }
        public DbSet<STC> STC { get; set; }
        public DbSet<BILL> BILL { get; set; }
        public DbSet<BILL1> BILL1 { get; set; }
        public DbSet<PUCH> PUCH { get; set; }
    }
}