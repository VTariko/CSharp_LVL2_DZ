using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6_Ex3
{
    class Program
    {
        static void Main(string[] args)
        {
            MagazineFirearms<ICaliber7_62> newMagazine = new MagazineFirearms<ICaliber7_62>(PermissibleCountCartridge.Count35);
            OrderCartridges newBatch = new OrderCartridges();
            newMagazine.Recharge(newBatch.ProduceCartridges<Cartridge7_62>(PermissibleCountCartridge.Count35));

            AssaultRifle<ICaliber7_62> ak = new AssaultRifle<ICaliber7_62>();
            ak.Magazine = newMagazine;

            while (true)
            {
                if (Console.ReadKey().KeyChar == ' ')
                {
                    ak.Shutter();
                    ak.PullTrigger();
                    ak.ReleasingTrigger();
                }
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Стрельба закончена");
                    Console.ReadLine();
                }

            }


        }
    }
}
