using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	class Bet : IBet
	{
		private readonly ISnail pickToWin;
		private readonly int wagerAmount;

		public Bet(ISnail pick, int wager)
		{
			this.pickToWin = pick;
			this.wagerAmount = wager;
		}

		public ISnail PickToWin
		{
			get { return this.pickToWin; }
		}
		public int WagerAmount
		{
			get { return this.wagerAmount; }
		}
	}
}
