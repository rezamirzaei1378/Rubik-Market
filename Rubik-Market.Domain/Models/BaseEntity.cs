using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.Models
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool isDelete { get; set; } = false;
    }
}
