using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public interface IRandom
	{
		int Next();
		int Next(Int32 maxValue);
		double NextDouble();
	}
}
