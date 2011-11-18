using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
