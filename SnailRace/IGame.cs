using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public interface IGame
	{
		IRaceLineup NewRaceLineup();
		IPlayer Player { get; }
		IBookie Bookie { get; }
	}
}
