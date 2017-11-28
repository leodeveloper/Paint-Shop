using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintShop.StaticData
{
    public class ColorEnums
    {
        public enum ColorFinish
        {
            GLOSS,
            MATTE
        }

        public static ColorFinish parseColorFinish(string abbreviation)
        {
            switch (abbreviation)
            {
                case "M":
                    return ColorFinish.MATTE;
                case "G":
                    return ColorFinish.GLOSS;
                default:
                    throw new Exception(("Invalid abbreviation for Finish: " + abbreviation));
            }
        }
    }
}
