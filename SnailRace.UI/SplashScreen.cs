using System;

namespace SnailRace.UI
{
	class SplashScreen : ISplashScreen
	{
		private string[] intro;
		private string[] gameOver;

		public SplashScreen()
		{
			intro = new string[]
			{
				" ____              _ _ ____",
				"/ ___| _ __   __ _(_) |  _ \\ __ _  ___ ___", 
				"\\___ \\| '_ \\ / _` | | | |_) / _` |/ __/ _ \\",
				" ___) | | | | (_| | | |  _ < (_| | (_|  __/",
				"|____/|_| |_|\\__,_|_|_|_| \\_\\__,_|\\___\\___|",
				"",
				"",
				"           Press any key to begin..."
			};

			gameOver = new string[]
			{
				"  ____                         ___",
				" / ___| __ _ _ __ ___   ___   / _ \\__   _____ _ __",
				"| |  _ / _` | '_ ` _ \\ / _ \\ | | | \\ \\ / / _ \\ '__|",
				"| |_| | (_| | | | | | |  __/ | |_| |\\ V /  __/ |",
				" \\____|\\__,_|_| |_| |_|\\___|  \\___/  \\_/ \\___|_|",
				"",
				"",
				"            Sorry, you ran out of money!",
				"",
				"               (Press any key to exit)"
			};
		}

		public void Intro()
		{
			printMessage(this.intro);
		}

		public void GameOver()
		{
			printMessage(this.gameOver);
		}

		private void printMessage(string[] message)
		{
			Console.Clear();
			for (int i = 0; i < message.Length; i++)
			{
				Console.SetCursorPosition(15, i + 2);
				Console.Write(message[i]);
			}
			Console.ReadKey(true);
		}
	}
}
