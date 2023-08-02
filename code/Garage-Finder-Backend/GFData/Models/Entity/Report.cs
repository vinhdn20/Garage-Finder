using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Garage")]
        public int GarageID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public Users User { get; set; }
        public Garage Garage { get; set; }
        public ICollection<ImageReport> ImageReport { get; set; }
    }
}
