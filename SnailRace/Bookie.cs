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
