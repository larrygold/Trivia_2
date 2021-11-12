using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaApp
{
    public class Players
    {
        public List<string> _players;
        public int[] _positionOfEachPlayer;
        public int[] _goldCoinsOfEachPlayer;

        public Players()
        {
            _players = new List<string>();
            _positionOfEachPlayer = new int[6];
            _goldCoinsOfEachPlayer = new int[6];
        }
    }
}