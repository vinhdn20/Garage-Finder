using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class RefreshToken
    {
        [Key]
        public int TokenID { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }
        public DateTime CreateDate { get; set; }

        //[ForeignKey("UserID")]
        public int UserID { get; set; }
        public Users Users { get; set; }
    }
}
