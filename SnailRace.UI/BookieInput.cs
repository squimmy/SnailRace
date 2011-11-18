using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace.UI
{
	class BookieInput : IBookieInput
	{
		private DelegateCreateBet createBet;
		private string leftMargin;

		public BookieInput(DelegateCreateBet createBet)
		{
			this.createBet = createBet;
			this.leftMargin = "          ";
		}

		public IBet TakeBet(IRaceLineup race)
		{
			ISnail pickToWin = chooseSnail(race);
			int betAmount = getBetAmount(pickToWin, race.Player.Money);

			return createBet(pickToWin, betAmount);
		}

		private ISnail chooseSnail(IRaceLineup race)
		{
			int selection = 0;

			this.printSnailChoices(race, null);
			
			int.TryParse(Console.ReadLine(), out selection);

			while (selection <= 0 || selection > race.Snails.Count())
			{
				this.printSnailChoices(race, string.Format("You must enter a number from 1 to {0}.\n", race.Snails.Count()));
				int.TryParse(Console.ReadLine(), out selection);
			}

			return race.Snails.ElementAt(selection - 1);
		}

		private void printSnailChoices(IRaceLineup race, string additionalText)
		{
			Console.Clear();
			Console.WriteLine("\n");
			Console.WriteLine(this.leftMargin + "Please choose one of the following snails:\n");
			Console.WriteLine(this.leftMargin + "Name:               Number:\n");
			for (int i = 0; i < race.Snails.Count(); i++)
			{
				Console.WriteLine(this.leftMargin + string.Format("{0, -20}{1}", race.Snails.ElementAt(i).Name, i + 1));
			}
			Console.Write("\n");
			if (additionalText != null)
			{
				Console.WriteLine(this.leftMargin + additionalText);	
			}
			Console.Write(this.leftMargin + "Which snail would you like to bet on? ");
		}

		private int getBetAmount(ISnail pickToWin, int money)
		{
			int wager = 0;
			bool inputValid = false;

			this.printBetPrompt(pickToWin, money, null);
			inputValid = int.TryParse(Console.ReadLine(), out wager);

			while (wager <=0 || wager > money)
			{
				if (!inputValid)
				{
					this.printBetPrompt(pickToWin, money, "You must enter a number!\n");
				}
				else if (wager <= 0)
				{
					this.printBetPrompt(pickToWin, money, "You must bet at least $1.\n");
				}
				else
				{
					this.printBetPrompt(pickToWin, money, "You don't have that much money!\n");
				}
				inputValid = int.TryParse(Console.ReadLine(), out wager);
			}

			return wager;
		}

		private void printBetPrompt(ISnail pickToWin, int money, string additionalText)
		{
			Console.Clear();
			Console.WriteLine("\n");
			if (additionalText != null)
			{
				Console.WriteLine(this.leftMargin + additionalText);
			}
			Console.WriteLine(this.leftMargin + "You have ${0}.\n", money);
			Console.Write(this.leftMargin + "How many dollars would you like to bet on {0}? ", pickToWin.Name);
		}
	}
}