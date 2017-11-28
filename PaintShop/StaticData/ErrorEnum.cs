using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintShop.StaticData
{
    public class ErrorEnum
    {
        public enum Errors
        {
            [Description("No Solution Exists")]
            Nosolutionexists,
            [Description("Please check the text file, there is no customer color present")]
            NoCustomerColorPresent,
            [Description("Please check the text file, the customer color not in expected format")]
            InputCustomerColorNotExpectedFormat

        }
    }
}
