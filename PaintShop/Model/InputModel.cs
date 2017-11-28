using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintShop.Model
{
    public class InputModel
    {
        public InputModel()
        {
            this.CustomerInputLines = new List<CustomerInputLine>();
        }
        public int OutPutColorCount { get; set; }

        public bool IsFirstInputLineParse
        {
            get
            {
                if (this.OutPutColorCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public IList<CustomerInputLine> CustomerInputLines { get; set; }
    }

    public class CustomerInputLine
    {
        public CustomerInputLine()
        {
            this.CustomerColorLine = new List<CustomerModel>();
        }
        public IList<CustomerModel> CustomerColorLine { get; set; }
    }  

}
