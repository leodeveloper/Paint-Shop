using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaintShop.StaticData.ColorEnums;

namespace PaintShop.Model
{
    public class CustomerModel
    {
        public int Paint { get; set; }
        public ColorFinish ColorFinish { get; set; }

        public bool IsOutPutParse { get; set; } = false;
    }
}
