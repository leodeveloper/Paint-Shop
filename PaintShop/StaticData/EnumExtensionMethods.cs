using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaintShop.StaticData
{
    public static class EnumExtensionMethods
    {
        #region Public ExtensionMethos

        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        public static string GetAbbreviation(this Enum en)
        {
            return en.ToString().Substring(0, 1);
        }

        public static string GetAbbreviation(this string str)
        {
            return str.Substring(0,1);
        }

#endregion
    }
}
