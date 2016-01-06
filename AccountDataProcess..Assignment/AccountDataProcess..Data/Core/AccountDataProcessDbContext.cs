using AccountDataProcess.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDataProcess.Data.Core
{
    public class AccountDataProcessDbContext : DbContext
    {
        private string _connectionString = string.Empty;
        public string ConnectionString
        {
            get
            {
                return this.Database.Connection.ConnectionString;
            }
            set
            {
                _connectionString = value;
                this.Database.Connection.ConnectionString = _connectionString;
            }
        }

        static AccountDataProcessDbContext()
        {
            Database.SetInitializer<AccountDataProcessDbContext>(new CreateDatabaseIfNotExists<AccountDataProcessDbContext>());
        }


        public AccountDataProcessDbContext()
            : base(nameOrConnectionString: "AccountDataProcessDbConnection")
        {
            // Do NOT enable proxied entities, else serialization fails
            Configuration.ProxyCreationEnabled = true;
            // Load navigation properties explicitly (avoid serialization trouble)
            Configuration.LazyLoadingEnabled = true;
            // Because Web API will perform validation, we don't need/want EF to do so
            Configuration.ValidateOnSaveEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<AccountData> AccountData { get; set; }

    }
}
