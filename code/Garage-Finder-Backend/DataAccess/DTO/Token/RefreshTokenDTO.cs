using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Token
{
    public class RefreshTokenDTO
    {
        public int TokenID { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
