using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class PlayerSurroundings
    {
        public List<Direction> walls = new List<Direction>();
        public List<Direction> empty = new List<Direction>();
        public Direction? finish = null;
    }
}
