using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintShop.Services.Repository
{
    public interface IColorMixing
    {
        string GetCustomerColors(string filePath);
    }
}
