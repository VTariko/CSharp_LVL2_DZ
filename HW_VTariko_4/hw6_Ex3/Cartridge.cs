using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6_Ex3
{
    public interface ICaliber
    {
        bool IsUsed { get; }
        void Shot();
    }

    public interface ICaliber5_45 : ICaliber { }
    public interface ICaliber7_62 : ICaliber { }

    public abstract class Cartridge: ICaliber
    {
        private bool _isUsed = false;
        public bool IsUsed { get { return _isUsed; } }
        public void Shot()
        {
            _isUsed = true;
            Console.WriteLine("╟──>");
        }
    }

    public class Cartridge7_62 : Cartridge, ICaliber7_62 {}
    public class Cartridge5_45 : Cartridge, ICaliber5_45 { }


}
