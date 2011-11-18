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
	public class SnailNameGenerator : ISnailNameGenerator
	{
		private IEnumerable<string> names;
		private IRandom random;

		public SnailNameGenerator(IRandom random)
		{
			this.random = random;
			names = new List<string>()
			{
				"Mike",
				"Alan",
				"Hugh",
				"Clement",
				"Levi",
				"Oak",
				"Potato",
				"Ethan",
				"Hannah",
				"Kimbo",
				"Cheese-Man",
				"Choco",
				"Strawberry",
				"Pancake",
				"Monster",
				"Smurf",
				"Fish",
				"Bobrika",
				"Bacon",
				"Speedy",
				"Lightning",
				"Trailblazer",
				"Maimai",
				"Denden",
				"Rambo"
			};
		}

		public string GetName()
		{
			return names.ElementAt(random.Next(names.Count()));
		}
		public IEnumerable<string> GetNames(int numberOfNames)
		{
			return
			(
				from name in names
				orderby this.random.NextDouble()
				select name
			)
			.Take(numberOfNames);
		}
	}
}
