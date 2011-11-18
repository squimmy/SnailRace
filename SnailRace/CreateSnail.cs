using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public delegate ISnail DelegateCreateSnail(string name);

	static public class CreateSnail
	{
		static public DelegateCreateSnail DelegateCreateSnail()
		{
			return name => { return new Snail(name); };
		}
	}
}