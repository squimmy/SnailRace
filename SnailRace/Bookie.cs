using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public class Bookie : IBookie
	{
		public IBet Bet { get; set; }
		
		public Bookie()
		{
		}
			
		public void PlaceBet(IRaceLineup race, IBet bet)
		{
			if (bet.WagerAmount <= 0 || bet.WagerAmount > race.Player.Money)
			{
				throw new ArgumentOutOfRangeException("Wager must be a positive value less than the player's current money.");
			}
			if (!race.Snails.Contains(bet.PickToWin))
			{
				throw new ArgumentException("Must bet on a snail that is participating in the race.");
			}

			race.Player.Money -= bet.WagerAmount;
			this.Bet = bet;
		}

		public void PayWinnings(IPlayer player, IRace raceResults)
		{
			if (this.Bet.PickToWin == raceResults.Winner)
			{
				player.Money += this.Bet.WagerAmount * raceResults.Positions.Count();
			}
		}
	}
}
