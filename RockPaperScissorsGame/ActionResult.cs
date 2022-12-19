using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsGame
{
    internal class ActionResult
    {
        public ActionResult() : base()
        {
        }

        public int ChoiceItem1 { get; set; }
        public int ChoiceItem2 { get; set; }
        public int WinnerItem { get; set; }
    }
}
