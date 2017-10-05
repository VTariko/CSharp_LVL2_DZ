using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6_Ex3
{
    class OrderCartridges
    {
        public T[] ProduceCartridges<T>(PermissibleCountCartridge number)
            where T : ICaliber, new()
        {

            List<T> order = new List<T>();
            for (int i = 0; i < (int)number; i++)
            {
                order.Add(new T());
            }
            return order.ToArray();
        }
    }
}
