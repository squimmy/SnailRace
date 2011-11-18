using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace.UI
{
	class Program
	{
		static void Main(string[] args)
		{
			BootStrapper bootStrapper = new BootStrapper();

			IGameUI gameUI = bootStrapper.NewGameUI();

			gameUI.PlayGame();
		}
	}
}
