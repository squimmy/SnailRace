using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public class Player : IPlayer
	{
		public int Money { get; set; }

		public Player()
		{
			Money = 100;
		}
	}
}