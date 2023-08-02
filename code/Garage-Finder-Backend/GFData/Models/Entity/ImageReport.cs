using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class ImageReport
    {
        [Key]
        public int ImageID { get; set; }
        [ForeignKey("Report")]
        public int ReportID { get; set; }
        public string ImageLink { get; set; }

        public Report Report { get; set; }
    }
}
