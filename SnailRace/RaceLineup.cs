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

using System.Collections.Generic;
using System.Linq;

namespace SnailRace
{
	class RaceLineup : IRaceLineup
	{
		private readonly IEnumerable<ISnail> snails;
		private readonly IRandom random;
		private readonly int raceLength;
		private IEnumerable<ISnail> finishedSnails;
		private IPlayer player;
		private DelegateCreateRace createRace;

		public RaceLineup(IEnumerable<ISnail> snails, IRandom random, int raceLength, IPlayer player, DelegateCreateRace createRace)
		{
			this.snails     = snails;
			this.random     = random;
			this.raceLength = raceLength;
			this.player     = player;
			this.createRace = createRace;

			finishedSnails =
				from snail in snails
				where snail.Position >= this.raceLength
				select snail;
		}

		public IRace RunRace()
		{
			ISnail randomSnail;
			IDictionary<ISnail, List<int>> race = prepareRace();

			var currentPosition =
				from snails in race
				select snails.Value.Last();

			while (currentPosition.Max() < this.raceLength)
			{
				randomSnail = this.pickRandomSnail(race);
				foreach (var pair in race)
				{
					if (pair.Key == randomSnail)
					{
						pair.Value.Add(pair.Value.Last() + 1);
					}
					else
					{
						pair.Value.Add(pair.Value.Last());
					}
				}
			}

			IDictionary<ISnail, IEnumerable<int>> raceAsIEnumerable = this.convertToIEnumerable(race);

			return createRace(this.raceLength, raceAsIEnumerable);
		}

		private IDictionary<ISnail, List<int>> prepareRace()
		{
			Dictionary<ISnail, List<int>> race = new Dictionary<ISnail, List<int>>();
			foreach (var snail in snails)
			{
				List<int> position = new List<int>(){0};
				race.Add(snail, position);
			};

			return race;
		}
		private ISnail pickRandomSnail(IDictionary<ISnail, List<int>> race)
		{
			return race.Keys.ElementAt(random.Next(race.Count()));
		}

		private IDictionary<ISnail, IEnumerable<int>> convertToIEnumerable(IDictionary<ISnail, List<int>> race)
		{
			IDictionary<ISnail, IEnumerable<int>> raceAsIEnumerable = new Dictionary<ISnail, IEnumerable<int>>();
			foreach (var pair in race)
			{
				raceAsIEnumerable.Add(pair.Key, pair.Value);
			}

			return raceAsIEnumerable;
		}

		public IEnumerable<ISnail> Snails
		{
			get { return this.snails; }
		}
		public IPlayer Player
		{
			get { return this.player; }
		}
		public int RaceLength
		{
			get { return raceLength; }
		}
	}
}
