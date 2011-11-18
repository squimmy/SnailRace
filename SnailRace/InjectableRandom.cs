using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public class InjectableRandom : IRandom
	{
		private Random random;
		public InjectableRandom()
		{
			this.random = new Random();
		}

		public int Next()
		{
			return this.random.Next();
		}
		public int Next(Int32 maxValue)
		{
			return this.random.Next(maxValue);
		}
		public double NextDouble()
		{
			return this.random.NextDouble();
		}
	}
}
