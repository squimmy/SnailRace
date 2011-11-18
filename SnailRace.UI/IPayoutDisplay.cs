using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace.UI
{
	interface IPayoutDisplay
	{
		void ShowWinnings(IBet bet, IRace race);
	}
}
