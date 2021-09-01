using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Models
{
    //The Api_Scores Response data model
    public class ScoreReponse
    {
        public ScoreReponse()
        {
            FrameProgressScores = new List<string>();

            GameCompleted = false;
        }

        public List<string> FrameProgressScores
        {
            get;
            set;
        }

        public bool GameCompleted
        {
            get;
            set;
        }
    }
}
