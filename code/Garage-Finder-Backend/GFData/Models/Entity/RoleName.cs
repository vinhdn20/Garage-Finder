using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class RoleName
    {
        [Key]
        public int RoleID { get; set; }
        public string NameRole { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
