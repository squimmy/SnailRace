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
	class GameUI : IGameUI
	{
		IGame game;
		ISplashScreen splashScreen;
		IRaceViewer raceViewer;
		IBookieInput bookieInput;
		IPayoutDisplay payoutDisplay;

		public GameUI(IGame game, ISplashScreen splashScreen, IRaceViewer raceViewer, IBookieInput bookieInput, IPayoutDisplay payoutDisplay)
		{
			this.game          = game;
			this.splashScreen  = splashScreen;
			this.raceViewer    = raceViewer;
			this.bookieInput   = bookieInput;
			this.payoutDisplay = payoutDisplay;
		}

		public void PlayGame()
		{
			Console.CursorVisible = false;
			splashScreen.Intro();
			while (game.Player.Money > 0)
			{
				IRaceLineup lineup = game.NewRaceLineup();
				IBet bet = bookieInput.TakeBet(lineup);
				game.Bookie.PlaceBet(lineup, bet);
				IRace race = lineup.RunRace();
				raceViewer.ShowRace(race);
				game.Bookie.PayWinnings(game.Player, race);
				payoutDisplay.ShowWinnings(bet, race);
			}
			splashScreen.GameOver();
		}
	}
}
