using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6_Ex3
{
    enum PermissibleCountCartridge
	{
        Count35 = 35,      // 0  35
		Count45 = 45,      // 1  45
        Count64 = 64       // 2  64
    }

    class MagazineFirearms<T> where T :  class, ICaliber
    {
        private int _capacityMagazine;

        public int CapacityMagazine => _capacityMagazine;

	    public MagazineFirearms(PermissibleCountCartridge number)
        {
            _capacityMagazine = (int)number;
        }

        Stack<T> _cartridges = new Stack<T>();
        public bool IsEmptyMagazine => _capacityMagazine == 0;

	    public int Recharge(IEnumerable<T> cartridges)
        {
            int temp = 0;

            foreach (var cartridge in cartridges)
            {
                if (_cartridges.Count < _capacityMagazine)
                {
                    _cartridges.Push(cartridge);
                    temp++;
                }
                else
                {
                    break;
                }
            }
            return temp;
        }
        public T GetCartridge()
        {
            if (_cartridges.Count > 0)
            {
                //Console.WriteLine(_Cartridges.Peek().IsUsed + "!!!");
                return _cartridges.Pop(); }
            else
            { return null; }
        }

    }
}
