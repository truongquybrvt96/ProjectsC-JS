using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonanzaImporterTool
{
    class ShippingMethod
    {
        public string name = "";
        public double price = -1f;
        public decimal timeMean = -1.0m;
        public ShippingMethod(string _name, double _price, decimal _timeMean)
        {
            this.name = _name;
            this.price = _price;
            this.timeMean = _timeMean;
        }
    }
}
