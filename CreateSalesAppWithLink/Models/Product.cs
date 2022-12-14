using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLink.Models {
    public class Product {

        [Key]
        public int Id { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }

    }
}
