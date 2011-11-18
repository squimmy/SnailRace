using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	class Race : IRace
	{
		private readonly int length;
		private readonly IDictionary<ISnail, IEnumerable<int>> positions;
		private readonly ISnail winner;

		public Race(int length, IDictionary<ISnail, IEnumerable<int>> positions)
		{
			this.length = length;
			this.positions = positions;

			winner = (from pair in positions
					  where pair.Value.Last() == positions.Max(snail => snail.Value.Last())
					  select pair).First().Key;
		}

		public int Length
		{
			get { return length; }
		}
		public IDictionary<ISnail, IEnumerable<int>> Positions
		{
			get { return positions; }
		}
		public ISnail Winner
		{
			get { return winner; }
		}
		public int TurnCount
		{
			get { return this.Positions.Values.First().Count(); }
		}
	}
}
