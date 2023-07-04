using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class StaffRefreshToken
    {
        [Key]
        public int TokenID { get; set; }
        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }
        public DateTime CreateDate { get; set; }
        public Staff Staff { get; set; }
    }
}
