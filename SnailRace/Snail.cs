using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnailRace
{
	class Snail : ISnail
	{
		public int Position { get; set; }
		private readonly string name;

		public Snail(string name)
		{
			this.Position = 0;
			this.name = name;
		}

		public void MoveForward()
		{
			this.Position++;
		}
		public string Name
		{
			get { return this.name; }
		}
	}
}