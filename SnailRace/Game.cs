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

namespace SnailRace
{
	public class Game : IGame
	{
		private readonly IPlayer player;
		private readonly DelegateCreateRaceLineup createRaceLineup;
		private readonly DelegateAggregateSnails aggregateSnails;
		private readonly IBookie bookie;
		
		public Game(IPlayer player, DelegateCreateRaceLineup createRaceLineup, DelegateAggregateSnails aggregateSnails, IBookie bookie)
		{
			this.player           = player;
			this.createRaceLineup = createRaceLineup;
			this.aggregateSnails  = aggregateSnails;
			this.bookie           = bookie;
		}

		public IRaceLineup NewRaceLineup()
		{
			return createRaceLineup(aggregateSnails(5), player);
		}

		public IPlayer Player
		{
			get { return this.player; }
		}
		public IBookie Bookie
		{
			get { return this.bookie; }
		}
	}
}
