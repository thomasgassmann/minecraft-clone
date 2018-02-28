namespace Assets.Logic.Blocks
{
    using System;

    [Serializable]
    public class Grass : Block
    {
        public override Tile GetTexturePosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Tile(2, 0);
                case Direction.Down:
                    return new Tile(1, 0);
            }

            return new Tile(3, 0);
        }
    }
}
