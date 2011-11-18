using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public delegate IEnumerable<ISnail> DelegateAggregateSnails(int numberOfSnails);

	static public class AggregateSnails
	{
		static public DelegateAggregateSnails DelegateAggregateSnails(DelegateCreateSnail createSnail, SnailNameGenerator snailNameGenerator)
		{
			return (numberOfSnails =>
				{

					var snails = from name in snailNameGenerator.GetNames(numberOfSnails)
								 select createSnail(name);

					return snails.ToList();
				});
		}
	}
}