using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnailRace.UI
{
	class RaceViewer : IRaceViewer
	{
		private object drawTrackLock;
		private string[] startLine;
		private string[] finishLine;
		private int topMargin;
		private string leftMargin;
		private int speed;

		public RaceViewer(int speed)
		{
			this.speed = speed;

			this.startLine = new string[] { "S", "T", "A", "R", "T" };
			this.finishLine = new string[] { " ", "E", "N", "D" };
			this.topMargin = 4;
			this.leftMargin = "          ";

			this.drawTrackLock = new object();
		}

		public void ShowRace(IRace race)
		{
			int turn = 0;
			this.drawTrack(race, turn, "Press any key to start the race...");
			Console.ReadKey(true);

			using (Timer timer = new System.Threading.Timer(
				(o) =>
				{
					turn++;
					this.drawTrack(race, turn, "(Press any key to skip)");
				}
			))
			{
				timer.Change(this.speed, this.speed);

				while (turn < race.TurnCount - 1 && !Console.KeyAvailable) ; // check if race is finished or if there's anything in the input buffer
				while (Console.KeyAvailable)
				{
					Console.ReadKey(true);	// clear the input buffer
				}
				timer.Dispose();
			}
			this.drawTrack(race, race.TurnCount - 1, string.Format("The winner is {0}\n\n", race.Winner.Name) + this.leftMargin + "Press any key to continue...");
			Console.ReadKey(true);
		}

		private void drawTrack(IRace race, int turn, string message)
		{			
			lock (this.drawTrackLock)
			{
				Console.Clear();
				Console.SetCursorPosition(0, this.topMargin);
				int iteration = 0;

				foreach (var position in race.Positions)
				{
					if (iteration < this.startLine.Length)
					{
						Console.Write(this.leftMargin + this.startLine[iteration]);
					}
					else
					{
						Console.Write(this.leftMargin + " ");
					}
					Console.Write("|");
					Console.Write(drawSnail(position.Value, turn, race.Length));
					Console.Write("|");
					if (iteration >= 0 && iteration < this.finishLine.Length)
					{
						Console.Write(this.finishLine[iteration]);
					}
					else
					{
						Console.Write(" ");
					}
					Console.WriteLine("  " + position.Key.Name);

					iteration++;
				}
				
				Console.Write("\n\n" + this.leftMargin + message);
			}
		}

		private string drawSnail(IEnumerable<int> snail, int turn, int raceLength)
		{
			int position = snail.ElementAt(turn);
			string snailText = new string(' ', position);
			snailText += "_@/\"";
			snailText += new string(' ', raceLength - position);
			
			return snailText;
		}

	}
}
