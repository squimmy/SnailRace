using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public interface ISnail
	{
		int Position { get; set; }
		string Name { get; }

		void MoveForward();
	}
}
