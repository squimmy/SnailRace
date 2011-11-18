using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnailRace;

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
			this.game = game;
			this.splashScreen = splashScreen;
			this.raceViewer = raceViewer;
			this.bookieInput = bookieInput;
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
