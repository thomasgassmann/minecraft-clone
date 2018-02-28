namespace Assets.Logic
{
    using System.Collections.Generic;
    using UnityEngine;

    public class DynamicChunkLoader : MonoBehaviour
    {
        private static WorldPosition[] chunkPositions = {   new WorldPosition( 0, 0,  0), new WorldPosition(-1, 0,  0), new WorldPosition( 0, 0, -1), new WorldPosition( 0, 0,  1), new WorldPosition( 1, 0,  0),
                             new WorldPosition(-1, 0, -1), new WorldPosition(-1, 0,  1), new WorldPosition( 1, 0, -1), new WorldPosition( 1, 0,  1), new WorldPosition(-2, 0,  0),
                             new WorldPosition( 0, 0, -2), new WorldPosition( 0, 0,  2), new WorldPosition( 2, 0,  0), new WorldPosition(-2, 0, -1), new WorldPosition(-2, 0,  1),
                             new WorldPosition(-1, 0, -2), new WorldPosition(-1, 0,  2), new WorldPosition( 1, 0, -2), new WorldPosition( 1, 0,  2), new WorldPosition( 2, 0, -1),
                             new WorldPosition( 2, 0,  1), new WorldPosition(-2, 0, -2), new WorldPosition(-2, 0,  2), new WorldPosition( 2, 0, -2), new WorldPosition( 2, 0,  2),
                             new WorldPosition(-3, 0,  0), new WorldPosition( 0, 0, -3), new WorldPosition( 0, 0,  3), new WorldPosition( 3, 0,  0), new WorldPosition(-3, 0, -1),
                             new WorldPosition(-3, 0,  1), new WorldPosition(-1, 0, -3), new WorldPosition(-1, 0,  3), new WorldPosition( 1, 0, -3), new WorldPosition( 1, 0,  3),
                             new WorldPosition( 3, 0, -1), new WorldPosition( 3, 0,  1), new WorldPosition(-3, 0, -2), new WorldPosition(-3, 0,  2), new WorldPosition(-2, 0, -3),
                             new WorldPosition(-2, 0,  3), new WorldPosition( 2, 0, -3), new WorldPosition( 2, 0,  3), new WorldPosition( 3, 0, -2), new WorldPosition( 3, 0,  2),
                             new WorldPosition(-4, 0,  0), new WorldPosition( 0, 0, -4), new WorldPosition( 0, 0,  4), new WorldPosition( 4, 0,  0), new WorldPosition(-4, 0, -1),
                             new WorldPosition(-4, 0,  1), new WorldPosition(-1, 0, -4), new WorldPosition(-1, 0,  4), new WorldPosition( 1, 0, -4), new WorldPosition( 1, 0,  4),
                             new WorldPosition( 4, 0, -1), new WorldPosition( 4, 0,  1), new WorldPosition(-3, 0, -3), new WorldPosition(-3, 0,  3), new WorldPosition( 3, 0, -3),
                             new WorldPosition( 3, 0,  3), new WorldPosition(-4, 0, -2), new WorldPosition(-4, 0,  2), new WorldPosition(-2, 0, -4), new WorldPosition(-2, 0,  4),
                             new WorldPosition( 2, 0, -4), new WorldPosition( 2, 0,  4), new WorldPosition( 4, 0, -2), new WorldPosition( 4, 0,  2), new WorldPosition(-5, 0,  0),
                             new WorldPosition(-4, 0, -3), new WorldPosition(-4, 0,  3), new WorldPosition(-3, 0, -4), new WorldPosition(-3, 0,  4), new WorldPosition( 0, 0, -5),
                             new WorldPosition( 0, 0,  5), new WorldPosition( 3, 0, -4), new WorldPosition( 3, 0,  4), new WorldPosition( 4, 0, -3), new WorldPosition( 4, 0,  3),
                             new WorldPosition( 5, 0,  0), new WorldPosition(-5, 0, -1), new WorldPosition(-5, 0,  1), new WorldPosition(-1, 0, -5), new WorldPosition(-1, 0,  5),
                             new WorldPosition( 1, 0, -5), new WorldPosition( 1, 0,  5), new WorldPosition( 5, 0, -1), new WorldPosition( 5, 0,  1), new WorldPosition(-5, 0, -2),
                             new WorldPosition(-5, 0,  2), new WorldPosition(-2, 0, -5), new WorldPosition(-2, 0,  5), new WorldPosition( 2, 0, -5), new WorldPosition( 2, 0,  5),
                             new WorldPosition( 5, 0, -2), new WorldPosition( 5, 0,  2), new WorldPosition(-4, 0, -4), new WorldPosition(-4, 0,  4), new WorldPosition( 4, 0, -4),
                             new WorldPosition( 4, 0,  4), new WorldPosition(-5, 0, -3), new WorldPosition(-5, 0,  3), new WorldPosition(-3, 0, -5), new WorldPosition(-3, 0,  5),
                             new WorldPosition( 3, 0, -5), new WorldPosition( 3, 0,  5), new WorldPosition( 5, 0, -3), new WorldPosition( 5, 0,  3), new WorldPosition(-6, 0,  0),
                             new WorldPosition( 0, 0, -6), new WorldPosition( 0, 0,  6), new WorldPosition( 6, 0,  0), new WorldPosition(-6, 0, -1), new WorldPosition(-6, 0,  1),
                             new WorldPosition(-1, 0, -6), new WorldPosition(-1, 0,  6), new WorldPosition( 1, 0, -6), new WorldPosition( 1, 0,  6), new WorldPosition( 6, 0, -1),
                             new WorldPosition( 6, 0,  1), new WorldPosition(-6, 0, -2), new WorldPosition(-6, 0,  2), new WorldPosition(-2, 0, -6), new WorldPosition(-2, 0,  6),
                             new WorldPosition( 2, 0, -6), new WorldPosition( 2, 0,  6), new WorldPosition( 6, 0, -2), new WorldPosition( 6, 0,  2), new WorldPosition(-5, 0, -4),
                             new WorldPosition(-5, 0,  4), new WorldPosition(-4, 0, -5), new WorldPosition(-4, 0,  5), new WorldPosition( 4, 0, -5), new WorldPosition( 4, 0,  5),
                             new WorldPosition( 5, 0, -4), new WorldPosition( 5, 0,  4), new WorldPosition(-6, 0, -3), new WorldPosition(-6, 0,  3), new WorldPosition(-3, 0, -6),
                             new WorldPosition(-3, 0,  6), new WorldPosition( 3, 0, -6), new WorldPosition( 3, 0,  6), new WorldPosition( 6, 0, -3), new WorldPosition( 6, 0,  3),
                             new WorldPosition(-7, 0,  0), new WorldPosition( 0, 0, -7), new WorldPosition( 0, 0,  7), new WorldPosition( 7, 0,  0), new WorldPosition(-7, 0, -1),
                             new WorldPosition(-7, 0,  1), new WorldPosition(-5, 0, -5), new WorldPosition(-5, 0,  5), new WorldPosition(-1, 0, -7), new WorldPosition(-1, 0,  7),
                             new WorldPosition( 1, 0, -7), new WorldPosition( 1, 0,  7), new WorldPosition( 5, 0, -5), new WorldPosition( 5, 0,  5), new WorldPosition( 7, 0, -1),
                             new WorldPosition( 7, 0,  1), new WorldPosition(-6, 0, -4), new WorldPosition(-6, 0,  4), new WorldPosition(-4, 0, -6), new WorldPosition(-4, 0,  6),
                             new WorldPosition( 4, 0, -6), new WorldPosition( 4, 0,  6), new WorldPosition( 6, 0, -4), new WorldPosition( 6, 0,  4), new WorldPosition(-7, 0, -2),
                             new WorldPosition(-7, 0,  2), new WorldPosition(-2, 0, -7), new WorldPosition(-2, 0,  7), new WorldPosition( 2, 0, -7), new WorldPosition( 2, 0,  7),
                             new WorldPosition( 7, 0, -2), new WorldPosition( 7, 0,  2), new WorldPosition(-7, 0, -3), new WorldPosition(-7, 0,  3), new WorldPosition(-3, 0, -7),
                             new WorldPosition(-3, 0,  7), new WorldPosition( 3, 0, -7), new WorldPosition( 3, 0,  7), new WorldPosition( 7, 0, -3), new WorldPosition( 7, 0,  3),
                             new WorldPosition(-6, 0, -5), new WorldPosition(-6, 0,  5), new WorldPosition(-5, 0, -6), new WorldPosition(-5, 0,  6), new WorldPosition( 5, 0, -6),
                             new WorldPosition( 5, 0,  6), new WorldPosition( 6, 0, -5), new WorldPosition( 6, 0,  5) };

        public World world;

        private readonly IList<WorldPosition> updateList = new List<WorldPosition>();

        private readonly IList<WorldPosition> buildList = new List<WorldPosition>();

        private int timer = 0;

        public void Update()
        {
            if (!this.DeleteChunks())
            {
                this.FindChunksToLoad();
                this.LoadAndRenderChunks();
            }
        }

        private void FindChunksToLoad()
        {
            var playerPos = new WorldPosition(
                Mathf.FloorToInt(transform.position.x / Chunk.Size) * Chunk.Size,
                Mathf.FloorToInt(transform.position.y / Chunk.Size) * Chunk.Size,
                Mathf.FloorToInt(transform.position.z / Chunk.Size) * Chunk.Size);
            if (this.updateList.Count == 0)
            {
                for (var i = 0; i < chunkPositions.Length; i++)
                {
                    var newChunkPos = new WorldPosition(
                        chunkPositions[i].X * Chunk.Size + playerPos.X,
                        0,
                        chunkPositions[i].Z * Chunk.Size + playerPos.Z);
                    var newChunk = this.world.GetChunk(newChunkPos);
                    if (newChunk != null && (newChunk.IsRendered || this.updateList.Contains(newChunkPos)))
                    {
                        continue;
                    }

                    for (var y = -4; y < 4; y++)
                    {
                        for (var x = newChunkPos.X - Chunk.Size; x <= newChunkPos.X + Chunk.Size; x += Chunk.Size)
                        {
                            for (var z = newChunkPos.Z - Chunk.Size; z <= newChunkPos.Z + Chunk.Size; z += Chunk.Size)
                            {
                                this.buildList.Add(new WorldPosition(x, y * Chunk.Size, z));
                            }
                        }

                        this.updateList.Add(new WorldPosition(newChunkPos.X, y * Chunk.Size, newChunkPos.Z));
                    }

                    return;
                }
            }
        }

        private void LoadAndRenderChunks()
        {
            if (this.buildList.Count != 0)
            {
                for (var i = 0; i < this.buildList.Count && i < 8; i++)
                {
                    this.BuildChunk(this.buildList[0]);
                    this.buildList.RemoveAt(0);
                }

                return;
            }

            if (this.updateList.Count != 0)
            {
                var chunk = this.world.GetChunk(new WorldPosition(this.updateList[0].X, this.updateList[0].Y, this.updateList[0].Z));
                if (chunk != null)
                {
                    chunk.UpdateRequired = true;
                }

                this.updateList.RemoveAt(0);
            }
        }

        private void BuildChunk(WorldPosition pos)
        {
            if (this.world.GetChunk(pos) == null)
            {
                this.world.CreateChunk(pos);
            }
        }

        private bool DeleteChunks()
        {
            if (this.timer == 100)
            {
                var chunksToDelete = new List<WorldPosition>();
                foreach (var chunk in world.Chunks)
                {
                    var distance = Vector3.Distance(
                        new Vector3(chunk.Value.Position.X, 0, chunk.Value.Position.Z),
                        new Vector3(transform.position.x, 0, transform.position.z));
                    if (distance > 64)
                    {
                        chunksToDelete.Add(chunk.Key);
                    }
                }

                chunksToDelete.ForEach(x => this.world.DestroyChunk(x));
                this.timer = 0;
                return true;
            }

            this.timer++;
            return false;
        }
    }
}
