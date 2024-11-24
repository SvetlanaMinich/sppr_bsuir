using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_253503_MINICH.Domain.Entities
{
    public class CartItem
    {
        public Cup Cup { get; set; }
        public int Count { get; set; } = 0;
    }
}
