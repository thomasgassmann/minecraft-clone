namespace Assets.Logic.Blocks
{
    using System;

    [Serializable]
    public class Cobblestone : Block
    {
        public override Tile GetTexturePosition(Direction direction)
        {
            return new Tile(0, 0);
        }
    }
}
