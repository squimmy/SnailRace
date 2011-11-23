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

namespace SnailRace.UI
{
	class BootStrapper
	{
		public IGameUI NewGameUI()
		{
			IRandom random                        = new InjectableRandom();
			IPlayer player                        = new Player();
			SnailNameGenerator snailNameGenerator = new SnailNameGenerator(random);
			IBookie bookie                        = new Bookie();
			IGame game                            = new Game
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
			DelegateCreateOutputMethod standardOutput = CreateConsoleOutput.DelegateCreateConsoleOutput();
			DelegateCreateOutputMethod bufferedOutput = CreateBufferedConsoleOutput.DelegateCreateBufferedConsoleOutput();

			ISplashScreen splashScreen   = new SplashScreen(standardOutput);
			IRaceViewer raceViewer       = new RaceViewer(100, bufferedOutput);
			IBookieInput bookieInput     = new BookieInput(CreateBet.DelegateCreateBet(), standardOutput);
			IPayoutDisplay payoutDisplay = new PayoutDisplay(standardOutput);

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
