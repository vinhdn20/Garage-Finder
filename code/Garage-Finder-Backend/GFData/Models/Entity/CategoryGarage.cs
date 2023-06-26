using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class CategoryGarage
    {
        [Key]
        public int CategoryGarageID { get; set; }
        public int GarageID { get; set;}
        [ForeignKey("Categorys")]
        public int CategoryID { get; set;}

        public Garage Garage { get; set;}
        public Categorys Categorys { get; set;}
        public ICollection<Service> Services { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
