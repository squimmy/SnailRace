using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public interface IBet
	{
		ISnail PickToWin { get; }
		int WagerAmount { get; }
	}
}
