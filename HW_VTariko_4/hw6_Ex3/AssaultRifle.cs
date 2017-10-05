using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6_Ex3
{
    class AssaultRifle<T> where T : class, ICaliber
    {
        private Queue<T> _сhamber = new Queue<T>();
        public MagazineFirearms<T> Magazine { get; set; }
        
        public void PullTrigger()
        {
            Console.WriteLine("Курок нажат");
            Shoot();
            Shutter();

        }
        public void ReleasingTrigger()
        {
            Console.WriteLine("Курок отпущен");
        }
        public void Shutter()
        {
            ClearChamber();
            Recharge();
        }        
        public void Shoot()
        {
            //Console.WriteLine(Magazine.CapacityMagazine + "   " + _Сhamber.Peek().IsUsed);
            if(_сhamber.Count>0 && !_сhamber.Peek().IsUsed )
            {
                // _Сhamber.Peek().Shot(); - было
                _сhamber.Dequeue().Shot();  // - нужно.
            }
        }
        public void Recharge()
        {
            if (Magazine != null)
            {
               
                T newCartridges = Magazine.GetCartridge();
                //Console.WriteLine(новыйПатрон.IsUsed + ":::!!!");
                if (newCartridges != null)
                {
                    // здесь мы не проверяем наличие патрона в патроннике
                    // потому что метод private и мы его всегда используем 
                    // после очистки
                    
                    _сhamber.Enqueue(newCartridges);
                }
            }
        }
        public void ClearChamber()
        {
            
        }

        






    }
}
