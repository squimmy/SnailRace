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
	class Race : IRace
	{
		private readonly int length;
		private readonly IDictionary<ISnail, IEnumerable<int>> positions;
		private readonly ISnail winner;

		public Race(int length, IDictionary<ISnail, IEnumerable<int>> positions)
		{
			this.length    = length;
			this.positions = positions;

			winner = (from pair in positions
					  where pair.Value.Last() == positions.Max(snail => snail.Value.Last())
					  select pair).First().Key;
		}

		public int Length
		{
			get { return length; }
		}
		public IDictionary<ISnail, IEnumerable<int>> Positions
		{
			get { return positions; }
		}
		public ISnail Winner
		{
			get { return winner; }
		}
		public int TurnCount
		{
			get { return this.Positions.Values.First().Count(); }
		}
	}
}
