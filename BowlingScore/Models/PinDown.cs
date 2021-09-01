using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Models
{
    //The Api_Scores input parameter data model
    public class PinDown
    {
        public PinDown()
        {
            pinsDowned = new List<int>();
        }

        public List<int> pinsDowned
        {   get;
            set;
        }
    }
}
