using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace.UI
{
	interface IBookieInput
	{
		IBet TakeBet(IRaceLineup raceLineup);
	}
}
