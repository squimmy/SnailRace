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

namespace SnailRace.UI
{
	class SplashScreen : ISplashScreen
	{
		private string[] intro;
		private string[] gameOver;
		private DelegateCreateOutputMethod createView;

		public SplashScreen(DelegateCreateOutputMethod createView)
		{
			this.createView = createView;

			intro = new string[]
			{
				"\n\n                ____              _ _ ____\n",
				"               / ___| _ __   __ _(_) |  _ \\ __ _  ___ ___\n", 
				"               \\___ \\| '_ \\ / _` | | | |_) / _` |/ __/ _ \\\n",
				"                ___) | | | | (_| | | |  _ < (_| | (_|  __/\n",
				"               |____/|_| |_|\\__,_|_|_|_| \\_\\__,_|\\___\\___|\n",
				"\n\n",
				"                          Press any key to begin..."
			};

			gameOver = new string[]
			{
				"\n\n                ____                         ___\n",
				"               / ___| __ _ _ __ ___   ___   / _ \\__   _____ _ __\n",
				"              | |  _ / _` | '_ ` _ \\ / _ \\ | | | \\ \\ / / _ \\ '__|\n",
				"              | |_| | (_| | | | | | |  __/ | |_| |\\ V /  __/ |\n",
				"               \\____|\\__,_|_| |_| |_|\\___|  \\___/  \\_/ \\___|_|\n",
				"\n\n",
				"                          Sorry, you ran out of money!\n",
				"\n",
				"                             (Press any key to exit)"
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

		private void printMessage(IEnumerable<string> message)
		{
			using (IOutputMethod view = createView())
			{
				foreach (var text in message)
				{
					view.Write(text);
				}
			}
			Console.ReadKey(true);
		}
	}
}