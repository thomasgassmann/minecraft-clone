namespace Assets.Logic.Blocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class OakWood : Block
    {
        public override Tile GetTexturePosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Tile(2, 1);
                case Direction.Down:
                    return new Tile(2, 1);
            }

            return new Tile(1, 1);
        }
    }
}
