using System;
using System.Collections.Generic;

namespace UglyTrivia
{
    public class Players
    {
        public List<string> _players { get; set; }
        public int[] _positionOfEachPlayer { get; set; }
        public int[] _goldCoinsOfEachPlayer { get; set; }
        public bool[] _isInPenaltyBoxForEachPlayer { get; set; }
        public int NumberPlayers => _players.Count;

        public Players()
        {
            _players = new List<string>();
            _positionOfEachPlayer = new int[6];
            _goldCoinsOfEachPlayer = new int[6];
            _isInPenaltyBoxForEachPlayer = new bool[6];
        }

        public void AddPlayer(string playerName)
        {
            _players.Add(playerName);
            DisplayPlayerAdded(playerName);
        }

        private void DisplayPlayerAdded(string playerName)
        {
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + NumberPlayers);
        }
    }
}