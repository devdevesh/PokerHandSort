using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandSort.Models
{
    public class Card
    {
       public int Value { get; set; }
       public char Suit { get; set; }
		public Card(string str)
		{
			char v = str[0];
			switch (v)
			{
				case 'T':
					this.Value = 10;
					break;
				case 'J':
					this.Value = 11;
					break;
				case 'Q':
					this.Value = 12;
					break;
				case 'K':
					this.Value = 13;
					break;
				case 'A':
					this.Value = 14;
					break;
				default:
					this.Value = int.Parse("" + v);
					break;
			}
			this.Suit = str[1];
		}

		public override string ToString()
		{
			string str = "";
			str = this.Value.ToString() + this.Suit;
			return str;
		}

	}
}
