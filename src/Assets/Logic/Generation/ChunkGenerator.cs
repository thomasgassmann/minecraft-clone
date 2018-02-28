namespace Assets.Logic.Generation
{
    using Blocks;
    using UnityEngine;

    public class ChunkGenerator
    {
        private float stoneBaseHeight = -24;

        private float stoneBaseNoise = 0.05f;

        private float stoneBaseNoiseHeight = 4;

        private float stoneMountainHeight = 48;

        private float stoneMountainFrequency = 0.008f;

        private float stoneMinHeight = -12;

        private float dirtBaseHeight = 1;

        private float dirtNoise = 0.04f;

        private float dirtNoiseHeight = 3;

        private float caveFrequency = 0.025f;

        private int caveSize = 7;

        public void GenerateChunk(Chunk chunk)
        {
            for (var x = chunk.Position.X; x < chunk.Position.X + Chunk.Size; x++)
            {
                for (var z = chunk.Position.Z; z < chunk.Position.Z + Chunk.Size; z++)
                {
                    this.GenerateChunkColumn(chunk, x, z);
                }
            }
        }

        public void GenerateChunkColumn(Chunk chunk, int columnX, int columnZ)
        {
            var stoneHeight = Mathf.FloorToInt(this.stoneBaseHeight);
            stoneHeight += ChunkGenerator.GetNoise(columnX, 0, columnZ, this.stoneMountainFrequency, Mathf.FloorToInt(this.stoneMountainHeight));
            if (stoneHeight < this.stoneMinHeight)
            {
                stoneHeight = Mathf.FloorToInt(this.stoneMinHeight);
            }

            stoneHeight += ChunkGenerator.GetNoise(columnX, 0, columnZ, this.stoneBaseNoise, Mathf.FloorToInt(this.stoneBaseNoiseHeight));
            var dirtHeight = stoneHeight + Mathf.FloorToInt(this.dirtBaseHeight);
            dirtHeight += ChunkGenerator.GetNoise(columnX, 100, columnZ, this.dirtNoise, Mathf.FloorToInt(this.dirtNoiseHeight));
            for (var y = chunk.Position.Y; y < chunk.Position.Y + Chunk.Size; y++)
            {
                var caveChance = ChunkGenerator.GetNoise(columnX, y, columnZ, this.caveFrequency, 100);
                var toSet = new WorldPosition(columnX - chunk.Position.X, y - chunk.Position.Y, columnZ - chunk.Position.Z);
                if (y <= stoneHeight && this.caveSize < caveChance)
                {
                    chunk.SetBlock(toSet, new Cobblestone());
                }
                else if (y <= dirtHeight && this.caveSize < caveChance)
                {
                    chunk.SetBlock(toSet, new Grass());
                }
                else
                {
                    chunk.SetBlock(toSet, new Air());
                }
            }
        }

        private static int GetNoise(int x, int y, int z, float scale, int max)
        {
            return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
        }
    }
}
