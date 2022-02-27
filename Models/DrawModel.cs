using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack21.Models
{
    public class DrawModel:CardNameValue
    {
        public int Index { get; set; }
        public bool IsInitialAce { get; set; } = false;
    }
}
