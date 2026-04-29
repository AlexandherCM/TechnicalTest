using System.Data.Entity;

namespace TechnicalTest.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
