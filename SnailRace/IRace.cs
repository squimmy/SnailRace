using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public interface IRace
	{
		int Length { get; }
		IDictionary<ISnail, IEnumerable<int>> Positions { get; }
		ISnail Winner { get; }
		int TurnCount { get; }
	}
}
