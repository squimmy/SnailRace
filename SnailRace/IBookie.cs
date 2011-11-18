using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public interface IBookie
	{
		IBet Bet { get; set; }

		void PlaceBet(IRaceLineup race, IBet bet);
		void PayWinnings(IPlayer player, IRace raceResults);
	}
}