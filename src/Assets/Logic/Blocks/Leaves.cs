using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Logic.Blocks
{
    public class Leaves : Block
    {
        public override Tile GetTexturePosition(Direction direction)
        {
            return new Tile(0, 1);
        }

        public override bool IsSolid(Direction direction)
        {
            return false;
        }
    }
}
