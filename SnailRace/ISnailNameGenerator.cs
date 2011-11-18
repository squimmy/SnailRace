using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	public interface ISnailNameGenerator
	{
		string GetName();
		IEnumerable<string> GetNames(int numberOfNames);
	}
}
