using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintShop.Model
{
    public class OutPutModel
    {
        public string PossibleOutPut { get; set; }
        public int CountG { get { return PossibleOutPut.Count(f => f == 'G'); } }
        public int CountM { get { return PossibleOutPut.Count(f => f == 'M'); } }
    }
}
