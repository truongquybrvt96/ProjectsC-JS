using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonanzaImporterTool
{
    public class ProductInfoSku
    {
        public List<PropertyItem> proItemList = new List<PropertyItem>();
        public void Add(PropertyItem _PI)
        {
            proItemList.Add(_PI);
        }
        public List<string> GetAllNames()
        {
            List<string> toReturn = new List<string>();
            foreach(PropertyItem PI in proItemList)
            {
                toReturn.Add(PI.name);
            }
            return toReturn;
        }
    }

    public class PropertyItem
    {
        public string name;
        public NameVal_String skuList_hasIMGTag;
        public int hasIMGTag;

        public PropertyItem(string _name, NameVal_String _skuList_hasIMGTag)
        {
            this.name = _name;
            this.skuList_hasIMGTag = _skuList_hasIMGTag;
        }
    }

    public class NameVal_String
    {
        public NameValueCollection skuList;
        public int hasIMGTag;
        public NameVal_String(NameValueCollection _skuList, int _hasIMGTag)
        {
            this.skuList = _skuList;
            this.hasIMGTag = _hasIMGTag;
        }
    }

    public class String_String
    {
        public string name1 = "";
        public string name2 = "";
        public String_String(string _name1, string _name2)
        {
            this.name1 = _name1;
            this.name2 = _name2;
        }
    }
}
