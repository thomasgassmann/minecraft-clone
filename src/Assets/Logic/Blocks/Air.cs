namespace Assets.Logic.Blocks
{
    using System;

    [Serializable]
    public class Air : Block
    {
        public override void GetBlockData(Chunk chunk, WorldPosition position, MeshData meshData)
        {
        }

        public override Tile GetTexturePosition(Direction direction)
        {
            return new Tile(0, 0);
        }

        public override bool IsSolid(Direction direction)
        {
            return false;
        }
    }
}