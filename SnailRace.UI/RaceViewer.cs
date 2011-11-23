// Copyright © 2011 Timothy du Heaume. All rights reserved.
//
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

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
		private string topMargin;
		private string leftMargin;
		private int speed;
		private DelegateCreateOutputMethod newOutput;

		public RaceViewer(int speed, DelegateCreateOutputMethod newOutput)
		{
			this.speed = speed;
			this.newOutput = newOutput;

			this.startLine  = new string[] { "S", "T", "A", "R", "T" };
			this.finishLine = new string[] { " ", "E", "N", "D" };
			this.topMargin  = "\n\n\n\n";
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
				using (IOutputMethod output = newOutput())
				{
					output.OutputText(this.topMargin);
					int iteration = 0;
					foreach (var position in race.Positions)
					{
						if (iteration < this.startLine.Length)
						{
							output.OutputText(this.leftMargin + this.startLine[iteration]);
						}
						else
						{
							output.OutputText(this.leftMargin + " ");
						}
						output.OutputText("|");
						output.OutputText(drawSnail(position.Value, turn, race.Length));
						output.OutputText("|");
						if (iteration >= 0 && iteration < this.finishLine.Length)
						{
							output.OutputText(this.finishLine[iteration]);
						}
						else
						{
							output.OutputText(" ");
						}
						output.OutputText("  " + position.Key.Name + "\n");

						iteration++;
					}
					output.OutputText("\n\n" + this.leftMargin + message);
				}
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
