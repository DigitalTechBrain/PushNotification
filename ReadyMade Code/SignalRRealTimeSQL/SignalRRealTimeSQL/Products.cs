using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalRRealTimeSQL
{
    public class Products
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal PricDecimal { get; set; }
        public decimal QuantDecimal { get; set; }
    }
}
