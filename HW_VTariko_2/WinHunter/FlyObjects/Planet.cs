using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHunter.FlyObjects
{
	class Planet: BaseObject
	{
		public Planet(Point pos, Point dir, Size size, Image image) : base(pos, dir, size, image)
		{
		}
	}
}
