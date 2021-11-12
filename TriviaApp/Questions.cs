using System.Collections.Generic;

namespace TriviaApp
{
    public class Questions
    {
        public List<string> _popQuestions;
        public List<string> _scienceQuestions;
        public List<string> _rockQuestions;
        public List<string> _sportsQuestions;
        public List<List<string>> _decksOfQuestions;

        public Questions()
        {
            _popQuestions = new List<string>();
            _scienceQuestions = new List<string>();
            _rockQuestions = new List<string>();
            _sportsQuestions = new List<string>();
            _decksOfQuestions = new List<List<string>>
            {_popQuestions, _scienceQuestions, _sportsQuestions,
                _rockQuestions};

        }
    }
}