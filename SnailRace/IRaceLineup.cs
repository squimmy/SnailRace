using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public interface IRaceLineup
	{
		IEnumerable<ISnail> Snails { get; }
		int RaceLength { get; }
		IPlayer Player { get; }

		IRace RunRace();
	}
}
