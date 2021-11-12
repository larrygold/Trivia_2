using System.Collections.Generic;

namespace TriviaApp
{
    public class Questions
    {
        public List<string> _popQuestions;
        public List<string> _scienceQuestions;
        public List<string> _rockQuestions;
        public List<string> _sportsQuestions;

        public Questions()
        {
            _popQuestions = new List<string>();
            _scienceQuestions = new List<string>();
            _rockQuestions = new List<string>();
            _sportsQuestions = new List<string>();
        }
    }
}