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

namespace SnailRace.UI
{
	class PayoutDisplay : IPayoutDisplay
	{
		private string leftMargin;
		private DelegateCreateOutputMethod createView;

		public PayoutDisplay(DelegateCreateOutputMethod createView)
		{
			this.createView = createView;
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
			using (IOutputMethod view = createView())
			{
				view.Write("\n\n");
				view.Write(this.leftMargin + "CONGRATULATIONS!\n");
				view.Write(this.leftMargin + "You won ${0}.\n\n", winnings);
				view.Write(this.leftMargin + "(press any key to continue...)");
			}
			Console.ReadKey(true);
		}

		private void loser()
		{
			using (IOutputMethod view = createView())
			{
				view.Write("\n\n");
				view.Write(this.leftMargin + "Sorry, you didn't win anything.\n\n");
				view.Write(this.leftMargin + "(press any key to continue...)");
			}
			Console.ReadKey(true);
		}
	}
}
