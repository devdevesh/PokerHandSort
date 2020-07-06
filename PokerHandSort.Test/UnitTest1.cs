using System;
using Xunit;
using PokerHandSort.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace PokerHandSort.Test
{
    public class SorterTest
    {
		[Fact]
		public void testWinner1()
		{
            //Player 1 must win with ONE PAIR
            String[] handOneStr = { "9C", "9D", "8D", "7C", "3C" };
			String[] handTwoStr = { "2S", "KD", "TH", "9H", "8H" };

			Hand.Sorting(handOneStr);
			Hand.Sorting(handTwoStr);
			Hand handOne = new Hand(handOneStr);
			Hand handTwo = new Hand(handTwoStr);
			handOne.Evaluate();
			handTwo.Evaluate();

			int abc = Hand.Winner(handOne, handTwo);
			Assert.Equal(1, abc); // 1 is for player 1
		}
        [Fact]
        public void testWinner2()
        { 
            // Player 2 must with FOUR OF A KIND
            String[] handOneStr = { "TC", "2C", "JC", "7C", "3C" };
            String[] handTwoStr = { "9H", "9C", "9S", "7D", "9D" };

            Hand.Sorting(handOneStr);
            Hand.Sorting(handTwoStr);
            Hand handOne = new Hand(handOneStr);
            Hand handTwo = new Hand(handTwoStr);
            handOne.Evaluate();
            handTwo.Evaluate();

            int abc = Hand.Winner(handOne, handTwo);
            Assert.Equal(2, Hand.Winner(handOne, handTwo)); // 2 is for player 1
        }
        [Fact]
        public void testWinner3()
        {
            //This should be a tie
            String[] handOneStr = { "9C", "9D", "8D", "7C", "3C" };
            String[] handTwoStr = { "9H", "9S", "8C", "7D", "3D" };

            Hand.Sorting(handOneStr);
            Hand.Sorting(handTwoStr);
            Hand handOne = new Hand(handOneStr);
            Hand handTwo = new Hand(handTwoStr);
            handOne.Evaluate();
            handTwo.Evaluate();

            int abc = Hand.Winner(handOne, handTwo);
            Assert.Equal(-1, Hand.Winner(handOne, handTwo)); //Tie 
        }
        [Fact]
        public void testWinner4()
        {
            //Player 1 must win with Full House
            String[] handOneStr = { "TC", "2C", "TS", "2S", "TD" };
            String[] handTwoStr = { "AH", "KH", "JH", "QH", "TH" };

            Hand.Sorting(handOneStr);
            Hand.Sorting(handTwoStr);
            Hand handOne = new Hand(handOneStr);
            Hand handTwo = new Hand(handTwoStr);
            handOne.Evaluate();
            handTwo.Evaluate();

            int abc = Hand.Winner(handOne, handTwo);
            Assert.Equal(1, Hand.Winner(handOne, handTwo));

        }
    }
}
