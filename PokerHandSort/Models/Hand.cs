using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandSort.Models
{
	public enum HandCategory
	{
		HIGH_CARD = 1,
		ONE_PAIR = 2,
		TWO_PAIRS = 3,
		THREE_OF_A_KIND = 4,
		STRAIGHT = 5,
		FLUSH = 6,
		FULL_HOUSE = 7,
		FOUR_OF_A_KIND = 8,
		STRAIGHT_FLUSH = 9,
		ROYAL_FLUSH = 10

	}
	public class Hand
	{
		public Card[] cards;
		public HandCategory category;
		public int? handValue;

		public HandCategory getHandCategory()
		{
			return this.category;
		}

		public int? getHandValue()
		{
			return this.handValue;
		}

		public Hand(Card[] cards)
		{
			this.cards = cards;
		}

		public Hand(string[] strArr)
		{
			if (strArr.Length != 5)
			{
				Console.WriteLine("Wrong hand format. Unable to progress.");
			}
			else
			{
				Card[] cards = new Card[5];
				for (int i = 0; i < 5; i++)
				{
					cards[i] = new Card(strArr[i]);
				}
				this.cards = cards;
			}
		}

		public static void Sorting(String[] handOn)
		{
			//Sorting array
			Array.Sort(handOn, StringComparer.InvariantCulture);
		}

		public Card getCard(int index)
		{
			if (index >= 5)
			{
				return null;
			}
			return cards[index];
		}

		public override string ToString()
		{
			string str = "";
			foreach (Card card in this.cards)
			{
				str += card.ToString() + " ";
			}
			if (str.Length > 0)
			{
				str += "(" + this.getHandCategory() + ")";
			}
			return str;
		}

		//This Evaluates what kind of card combination player got
		public void Evaluate()
		{

			if (this.AllSameSuit() != -1 && this.Straight() != -1)
			{
				if (this.getCard(0).Value == 10)
				{
					this.category = HandCategory.ROYAL_FLUSH;
					this.handValue = 9999;
					return;
				}
				else
				{
					this.category = HandCategory.STRAIGHT_FLUSH;
					return;
				}
			}

			if (this.Four() != -1)
			{
				this.category = HandCategory.FOUR_OF_A_KIND;
				return;
			}

			if (this.FullHouse() != -1)
			{
				this.category = HandCategory.FULL_HOUSE;
				return;
			}

			if (this.AllSameSuit() != -1)
			{
				this.category = HandCategory.FLUSH;
				return;
			}

			if (this.Straight() != -1)
			{
				this.category = HandCategory.STRAIGHT;
				return;
			}

			if (this.Three() != -1)
			{
				this.category = HandCategory.THREE_OF_A_KIND;
				return;
			}

			if (this.TwoPairs() != -1)
			{
				this.category = HandCategory.TWO_PAIRS;
				return;
			}

			if (this.Pair() != -1)
			{
				this.category = HandCategory.ONE_PAIR;
				return;
			}

			this.handValue = this.getCard(4).Value;
			this.category = HandCategory.HIGH_CARD;
		}
		
		//Functions to compute Card Combination

		#region
		private int Pair()
		{
			int prev = this.cards[4].Value;
			int total = 0, nOfCards = 1;
			for (int i = 3; i >= 0; i--)
			{
				if (this.cards[i].Value == prev)
				{
					total += this.cards[i].Value;
					nOfCards++;
				}

				if (nOfCards == 2)
				{
					break;
				}
				prev = this.cards[i].Value;
			}
			if (nOfCards == 2)
			{
				this.handValue = total;
				return total;
			}
			return -1;

		}
		private int TwoPairs()
		{
			int prev = this.cards[4].Value;
			int i = 3, total = 0, nOfCards = 1;
			for (; i >= 0; i--)
			{
				if (this.cards[i].Value == prev)
				{
					total += this.cards[i].Value;
					nOfCards++;
				}

				if (nOfCards == 2)
				{
					break;
				}
				else
				{
					total = 0;
					nOfCards = 1;
				}
				prev = this.cards[i].Value;
			}
			if (nOfCards == 2 && i > 0)
			{
				nOfCards = 1;
				prev = this.cards[i - 1].Value;
				for (i = i - 2; i >= 0; i--)
				{
					if (this.cards[i].Value == prev)
					{
						total += this.cards[i].Value;
						nOfCards++;
					}
					if (nOfCards == 2)
					{
						break;
					}
					else
					{
						total = 0;
						nOfCards = 1;
					}
					prev = this.cards[i].Value;
				}
			}
			else
			{
				return -1;
			}

			if (nOfCards == 2)
			{
				this.handValue = total;
				return total;
			}
			return -1;

		}
		private int FullHouse()
		{
			bool changed = false;
			int prev = this.cards[4].Value;
			int total = 0, nOfCards = 1;

			for (int i = 3; i >= 0; i--)
			{
				if (this.cards[i].Value == prev)
				{
					total += this.cards[i].Value;
					nOfCards++;

				}
				else if (changed == false)
				{
					changed = true;
					if (nOfCards < 2)
					{
						this.handValue = -1;
						return -1;
					}

					if (nOfCards == 3)
					{
						this.handValue = total;
					}

				}
				else
				{
					this.handValue = -1;
					return -1;
				}
				prev = this.cards[i].Value;
			}
			this.handValue = total;
			return total;
		}
		private int Four()
		{

			int prev = this.cards[4].Value;
			int total = 0, nOfCards = 1;

			for (int i = 3; i >= 0 && nOfCards < 4; i--)
			{
				if (this.cards[i].Value == prev)
				{
					total += this.cards[i].Value;
					nOfCards++;
				}
				else
				{
					total = 0;
					nOfCards = 1;
				}

				prev = this.cards[i].Value;
			}

			if (nOfCards == 4)
			{
				this.handValue = total;
				return total;
			}
			return -1;
		}
		private int AllSameSuit()
		{

			char prev = this.cards[0].Suit;
			int total = this.cards[0].Value;

			for (int i = 1; i < 5; i++)
			{
				if (this.cards[i].Suit != prev)
				{
					return -1;
				}
				total += this.cards[i].Value;
				prev = this.cards[i].Suit;
			}
			this.handValue = total;
			return total;
		}
		private int Straight()
		{

			int prev = this.cards[0].Value;
			int total = prev;
			for (int i = 1; i < 5; i++)
			{
				if (this.cards[i].Value != prev + 1)
				{
					return -1;
				}
				prev = this.cards[i].Value;
				total += 1;
			}
			this.handValue = total;
			return total;
		}
		private int Three()
		{
			int prev = this.cards[4].Value;
			int total = 0, nOfCards = 1;

			for (int i = 3; i >= 0; i--)
			{
				if (this.cards[i].Value == prev)
				{
					total += this.cards[i].Value;
					nOfCards++;
				}
				else
				{
					total = 0;
					nOfCards = 1;
				}

				prev = this.cards[i].Value;
			}

			if (nOfCards == 3)
			{
				this.handValue = total;
				return total;
			}
			return -1;
		}

        #endregion

        //Pridicting winner 
        public static int Winner(Hand hand1, Hand hand2)
		{

			if (hand1.getHandCategory() > hand2.getHandCategory())
			{
				return 1;
			}
			else if (hand1.getHandCategory() < hand2.getHandCategory())
			{
				return 2;
			}
			else if (hand1.getHandValue() > hand2.getHandValue())
			{
				return 1;
			}
			else if (hand1.getHandValue() < hand2.getHandValue())
			{
				return 2;
			}
			else
			{
				// final tie break!
				for (int i = 4; i >= 0; i--)
				{
					if (hand1.getCard(i).Value > hand2.getCard(i).Value)
					{
						return 1;
					}
					else if (hand1.getCard(i).Value < hand2.getCard(i).Value)
					{
						return 2;
					}
				}
				// theres a tie here...
				return -1;
			}

		}

	}
}