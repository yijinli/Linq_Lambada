using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infratructure
{
    public class CustomCompare<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            if (x is Person)
            {
                return (x as Person).Age - (y as Person).Age;
            }
            return -1;
        }
    }
}
