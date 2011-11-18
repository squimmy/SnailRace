using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public delegate IRace DelegateCreateRace(int length, IDictionary<ISnail, IEnumerable<int>> positions);

	static public class CreateRace
	{
		static public DelegateCreateRace DelegateCreateRace()
		{
			return (length, positions) => { return new Race(length, positions); };
		}
	}
}
