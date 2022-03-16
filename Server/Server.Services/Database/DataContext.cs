using System.Data.Entity;
using Falcon.Web.Core.Auth;
using Falcon.Web.Core.Settings;
using Phoenix.Server.Data.Entity;

namespace Phoenix.Server.Services.Database
{
    public class DataContext : DbContext
    {
        #region Init
        public DataContext() : base("DataContext") { }
        #endregion

        #region Datasets
        //framework
        public virtual DbSet<Medicine_Image> Medicine_Images { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        //modules

        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Input> Inputs { get; set; }
        public virtual DbSet<InputInfo> InputInfos { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Output> Outputs { get; set; }
        public virtual DbSet<OutputInfo> OutputInfos { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
       
       
     



        #endregion

    }
}