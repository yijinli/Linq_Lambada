using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infratructure
{
    public class CustomEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            if (typeof(T) == typeof(Person))
            {
                return string.Equals((x as Person).JobId, (y as Person).JobId);
            }
            return false;
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}
