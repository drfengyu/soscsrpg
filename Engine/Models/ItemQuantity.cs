using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// 单任务清单
    /// </summary>
    public class ItemQuantity
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public ItemQuantity(int itemID, int quantity) {
            ItemID = itemID;
            Quantity = quantity;
        }
    }
}
