using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public delegate IRaceLineup DelegateCreateRaceLineup(IEnumerable<ISnail> snails, IPlayer player);

	static public class CreateRaceLineup
	{
		static public DelegateCreateRaceLineup DelegateCreateRaceLineup(IRandom random, int raceLength, DelegateCreateRace createRace)
		{
			return (snails, player) => { return new RaceLineup(snails, random, raceLength, player, createRace); };
		}
	}
}