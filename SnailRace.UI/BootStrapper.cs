using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace.UI
{
	class BootStrapper
	{
		public IGameUI NewGameUI()
		{
			IRandom random = new InjectableRandom();
			IPlayer player = new Player();
			SnailNameGenerator snailNameGenerator = new SnailNameGenerator(random);
			IBookie bookie = new Bookie();
			IGame game = new Game
			(
				player,
				CreateRaceLineup.DelegateCreateRaceLineup
				(
					random,
					40,
					CreateRace.DelegateCreateRace()
				),
				AggregateSnails.DelegateAggregateSnails
				(
					CreateSnail.DelegateCreateSnail(),
					snailNameGenerator
				),
				bookie
			);

			ISplashScreen splashScreen = new SplashScreen();
			IRaceViewer raceViewer = new RaceViewer(100);
			IBookieInput bookieInput = new BookieInput(CreateBet.DelegateCreateBet());
			IPayoutDisplay payoutDisplay = new PayoutDisplay();

			return new GameUI(
				game,
				splashScreen,
				raceViewer,
				bookieInput,
				payoutDisplay
			);
		}
	}
}
