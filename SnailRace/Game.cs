using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public class Game : IGame
	{
		private readonly IPlayer player;
		private readonly DelegateCreateRaceLineup createRaceLineup;
		private readonly DelegateAggregateSnails aggregateSnails;
		private readonly IBookie bookie;
		
		public Game(IPlayer player, DelegateCreateRaceLineup createRaceLineup, DelegateAggregateSnails aggregateSnails, IBookie bookie)
		{
			this.player = player;
			this.createRaceLineup = createRaceLineup;
			this.aggregateSnails = aggregateSnails;
			this.bookie = bookie;
		}

		public IRaceLineup NewRaceLineup()
		{
			return createRaceLineup(aggregateSnails(5), player);
		}

		public IPlayer Player
		{
			get { return this.player; }
		}
		public IBookie Bookie
		{
			get { return this.bookie; }
		}
	}
}
