using GFData.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class FileOrdersDTO
    {
        public int FileId { get; set; }
        public int OrderID { get; set; }
        public string FileLink { get; set; }
    }
}
