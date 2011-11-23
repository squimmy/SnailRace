// Copyright © 2011 Timothy du Heaume. All rights reserved.
//
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Linq;

namespace SnailRace.UI
{
	class BookieInput : IBookieInput
	{
		private DelegateCreateBet createBet;
		private string leftMargin;
		DelegateCreateOutputMethod createView;

		public BookieInput(DelegateCreateBet createBet, DelegateCreateOutputMethod createView)
		{
			this.createBet  = createBet;
			this.createView = createView;
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
			Console.CursorVisible = true;
			int.TryParse(Console.ReadLine(), out selection);

			while (selection <= 0 || selection > race.Snails.Count())
			{
				this.printSnailChoices(race, string.Format("You must enter a number from 1 to {0}.\n", race.Snails.Count()));
				int.TryParse(Console.ReadLine(), out selection);
			}
			Console.CursorVisible = false;

			return race.Snails.ElementAt(selection - 1);
		}

		private void printSnailChoices(IRaceLineup race, string additionalText)
		{
			using (IOutputMethod view = createView())
			{
				view.Write("\n\n");
				view.Write(this.leftMargin + "Please choose one of the following snails:\n\n");
				view.Write(this.leftMargin + "Name:               Number:\n\n");
				int snailNumber = 1;
				foreach (var snail in race.Snails)
				{
					view.Write(this.leftMargin + "{0, -20}{1}\n", snail.Name, snailNumber);
					snailNumber++;
				}
				view.Write("\n");
				if (additionalText != null)
				{
					view.Write(this.leftMargin + additionalText + "\n");
				}
				view.Write(this.leftMargin + "Which snail would you like to bet on? ");
			}
		}

		private int getBetAmount(ISnail pickToWin, int money)
		{
			int wager = 0;
			bool inputValid = false;

			this.printBetPrompt(pickToWin, money, null);

			Console.CursorVisible = true;
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
			Console.CursorVisible = false;

			return wager;
		}

		private void printBetPrompt(ISnail pickToWin, int money, string additionalText)
		{
			using (IOutputMethod view = createView())
			{
				view.Write("\n\n");
				if (additionalText != null)
				{
					view.Write(this.leftMargin + additionalText + "\n");
				}
				view.Write(this.leftMargin + "You have ${0}.\n\n", money);
				view.Write(this.leftMargin + "How many dollars would you like to bet on {0}? ", pickToWin.Name);
			}
			
		}
	}
}