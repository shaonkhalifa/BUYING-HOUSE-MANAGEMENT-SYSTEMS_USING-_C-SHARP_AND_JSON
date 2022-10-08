using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buying_House_Management_StoreApp_Project.Model
{
    public class orderInformation
    {
        public int orderId { get; set; }
        public string productName { get; set; }

        public bool productAvailable { get; set; }
        public DateTime orderDate { get; set; }
        public int quantity { get; set; }
        public int Unitprice { get; set; }
        public int TotalPrice { get; set; }

    }
}
