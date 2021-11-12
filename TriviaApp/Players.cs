using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriviaApp
{
    public class Players
    {
        public List<string> _players;
        private int[] _positionOfEachPlayer;
        private int[] _goldCoinsOfEachPlayer;
        public bool[] _isInPenaltyBoxForEachPlayer;

        public int NumberPlayers => _players.Count;

        public Players()
        {
            _players = new List<string>();
            _positionOfEachPlayer = new int[6];
            _goldCoinsOfEachPlayer = new int[6];
            _isInPenaltyBoxForEachPlayer = new bool[6];
        }

        public void Add(string playerName)
        {
            _players.Add(playerName);
            DisplayPlayerAdded(playerName);
        }

        public int GetPlace(int playerIndex)
        {
            return _positionOfEachPlayer[playerIndex];
        }

        public void AddToPlace(int playerIndex, int addAmount)
        {
            _positionOfEachPlayer[playerIndex] += addAmount;
        }

        public int GetGoldCoins(int playerIndex)
        {
            return _goldCoinsOfEachPlayer[playerIndex];
        }

        public void AddToPurse(int playerIndex, int amount)
        {
            _goldCoinsOfEachPlayer[playerIndex] += amount;
        }

        public bool DoesAPlayerHave(int amount)
        {
            return _goldCoinsOfEachPlayer.All(x => x != 6);
        }

        private void DisplayPlayerAdded(string playerName)
        {
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + NumberPlayers);
        }

    }
}