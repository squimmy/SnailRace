using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace.UI
{
	class PayoutDisplay : IPayoutDisplay
	{
		private string leftMargin;

		public PayoutDisplay()
		{
			this.leftMargin = "          ";
		}

		public void ShowWinnings(IBet bet, IRace race)
		{

			if (bet.PickToWin == race.Winner)
			{
				this.winner(bet.WagerAmount * race.Positions.Count);
			}
			else
			{
				this.loser();
			}
		}

		private void winner(int winnings)
		{
			Console.Clear();
			Console.WriteLine("\n");
			Console.WriteLine(this.leftMargin + "CONGRATULATIONS!");
			Console.WriteLine(this.leftMargin + "You won ${0}.", winnings);

			Console.ReadKey(true);
		}

		private void loser()
		{
			Console.Clear();
			Console.WriteLine("\n");
			Console.WriteLine(this.leftMargin + "Sorry, you didn't win anything.");

			Console.ReadKey(true);
		}
	}
}
