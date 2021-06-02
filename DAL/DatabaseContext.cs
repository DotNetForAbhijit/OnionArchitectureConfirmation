using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        //we are passing options to base i.e. DbContext
        //because it is responsible for db connection mgnt
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        /*
         * This is fleunt api style for category class
         * this keeps class completely clean unlike product class
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this is how you define primary key
            modelBuilder.Entity<Category>().HasKey(c=>c.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.Name)
                                           .IsRequired()
                                           .HasMaxLength(50)
                                           .IsUnicode(false); //this means it is varchar
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=CODITAS-ABHIJIT\SQLEXPRESS;Initial Catalog=EFCorePractice; Integrated Security=true");
            }
        }

        //Write sp and stored proc here code like ado.net here
        //and call it from controller to access data from db
        public Product usp_getproduct(int productId)
        {
            Product product = new Product();
            using (var command= this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_getproduct";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter("ProductId", productId);

                command.Parameters.Add(parameter);

                Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        product.ProductId = reader.GetInt32("productId");
                        product.Name = reader.GetString("Name");
                        product.Description = reader.GetString("Description");
                        product.UnitPrice = reader.GetDecimal("UnitPrice");
                        product.CategoryId = reader.GetInt32("CategoryId");
                    }
                }

                Database.CloseConnection();
            }

            return product;
        }

        public Product fn_getproduct(int productId)
        {
            return Products.FromSqlRaw<Product>("Select * from udf_getproduct(productId)",
                                                 new SqlParameter ("productId", productId)).FirstOrDefault();
        }
    }
}
