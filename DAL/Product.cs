using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Product
    {
        //This is data annotation style
        //This makes your class prop looks meesy so you can avoid this annotation style
        //by using Fluent api. refer category class for the same
        //Check databse Context for Fluent API category class chages

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; } // be default primary key naming conv

        [Column(TypeName ="varchar(50)")]
        [Required]
        public string Name { get; set; } // Varchar Max bedefault

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Description { get; set; }
        public decimal UnitPrice { get; set; } //decimal (18,2) default precision

        /*
         * this is not a class name this is prop name
         * You can either define it here or over navigation property to make it foreign key
         */
        [ForeignKey("Category")] 
        public int CategoryId { get; set; } // Now this is foreign key be dault as we have below navigation prop

        /*
         * [ForeignKey("CategoryId")] is also accepted
         * either or is fine
         * */
        public virtual Category Category { get; set; } // adding virtual is optional


    }
}
