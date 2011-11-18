using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public delegate IBet DelegateCreateBet(ISnail pick, int wager);

	static public class CreateBet
	{
		static public DelegateCreateBet DelegateCreateBet()
		{
			return (pick, wager) => { return new Bet(pick, wager); };
		}
	}
}