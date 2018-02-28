namespace Assets.Logic
{
    using UnityEngine;
    using System.Collections.Generic;
    using Blocks;
    using Serialization;
    using Generation;

    /// <summary>
    /// Defines the world class that contains all chunks of the world.
    /// </summary>
    public class World : MonoBehaviour
    {
        private static readonly GameObject chunkPrefab;

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        public World()
        {
            this.Chunks = new Dictionary<WorldPosition, Chunk>();
            this.Title = "World";
        }

        static World()
        {
            World.chunkPrefab = (GameObject)Resources.Load("Chunk");
        }

        /// <summary>
        /// Gets all chunks mapped to a world position.
        /// </summary>
        public IDictionary<WorldPosition, Chunk> Chunks { get; private set; }

        /// <summary>
        /// Gets or sets the title of the world.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Starts the world class.
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// Updates the world class.
        /// </summary>
        public void Update()
        {
        }

        public void OnDestroy()
        {
            foreach (var chunk in this.Chunks.Keys)
            {
                this.DestroyChunk(chunk);
            }
        }

        /// <summary>
        /// Creates a chunk at the given position.
        /// </summary>
        /// <param name="position">The world coordinates.</param>
        public void CreateChunk(WorldPosition position)
        {
            var newChunkObject = UnityEngine.Object.Instantiate(
                World.chunkPrefab,
                new Vector3(position.X, position.Y, position.Z),
                Quaternion.Euler(Vector3.zero)) as GameObject;
            var newChunk = newChunkObject.GetComponent<Chunk>();
            newChunk.Position = position;
            newChunk.World = this;
            this.Chunks.Add(position, newChunk);
            var terrainGenerator = new ChunkGenerator();
            terrainGenerator.GenerateChunk(newChunk);
            newChunk.SetBlocksUnmodified();
            WorldRepository.LoadChunk(newChunk);
        }

        /// <summary>
        /// Destroys the chunk at the given world position.
        /// </summary>
        /// <param name="position">The world position of the chunk.</param>
        public void DestroyChunk(WorldPosition position)
        {
            var chunk = default(Chunk);
            if (this.Chunks.TryGetValue(position, out chunk))
            {
                WorldRepository.SaveChunk(chunk);
                UnityEngine.Object.Destroy(chunk.gameObject);
                this.Chunks.Remove(position);
            }
        }

        /// <summary>
        /// Gets a chunk at the specified position in the current world.
        /// </summary>
        /// <param name="position">The world coordinates of the chunk.</param>
        /// <returns>The chunk found at the position.</returns>
        public Chunk GetChunk(WorldPosition position)
        {
            var multiple = (float)Chunk.Size;
            var newPos = new WorldPosition(
                Mathf.FloorToInt(position.X / multiple) * Chunk.Size,
                Mathf.FloorToInt(position.Y / multiple) * Chunk.Size,
                Mathf.FloorToInt(position.Z / multiple) * Chunk.Size);
            var chunk = default(Chunk);
            this.Chunks.TryGetValue(newPos, out chunk);
            return chunk;
        }

        /// <summary>
        /// Gets a block at the specified position.
        /// </summary>
        /// <param name="x">The world coordinates of the block.</param>
        /// <returns>Returns the block found at the given position.</returns>
        public Block GetBlock(WorldPosition position)
        {
            var chunk = this.GetChunk(position);
            if (chunk != null)
            {
                var newPos = new WorldPosition(
                    position.X - chunk.Position.X,
                    position.Y - chunk.Position.Y,
                    position.Z - chunk.Position.Z);
                return chunk.GetBlock(newPos);
            }
            else
            {
                return new Air();
            }
        }

        /// <summary>
        /// Sets a block in the world.
        /// </summary>
        /// <param name="worldPosition">The world position of the block to set.</param>
        /// <param name="block">The block to place at the position.</param>
        public void SetBlock(WorldPosition worldPosition, Block block)
        {
            var chunk = this.GetChunk(worldPosition);
            if (chunk != null)
            {
                chunk.SetBlock(
                    new WorldPosition(
                        worldPosition.X - chunk.Position.X,
                        worldPosition.Y - chunk.Position.Y,
                        worldPosition.Z - chunk.Position.Z),
                    block);
                chunk.UpdateRequired = true;
                this.UpdateIfEqual(
                    worldPosition.X - chunk.Position.X,
                    0,
                    new WorldPosition(worldPosition.X - 1, worldPosition.Y, worldPosition.Z));
                this.UpdateIfEqual(
                    worldPosition.X - chunk.Position.X,
                    Chunk.Size - 1,
                    new WorldPosition(worldPosition.X + 1, worldPosition.Y, worldPosition.Z));
                this.UpdateIfEqual(
                    worldPosition.Y - chunk.Position.Y,
                    0,
                    new WorldPosition(worldPosition.X, worldPosition.Y - 1, worldPosition.Z));
                this.UpdateIfEqual(
                    worldPosition.Y - chunk.Position.Y,
                    Chunk.Size - 1,
                    new WorldPosition(worldPosition.X, worldPosition.Y + 1, worldPosition.Z));
                this.UpdateIfEqual(
                    worldPosition.Z - chunk.Position.Z,
                    0,
                    new WorldPosition(worldPosition.X, worldPosition.Y, worldPosition.Z - 1));
                this.UpdateIfEqual(
                    worldPosition.Z - chunk.Position.Z,
                    Chunk.Size - 1,
                    new WorldPosition(worldPosition.X, worldPosition.Y, worldPosition.Z + 1));
            }
        }

        private void UpdateIfEqual(int value1, int value2, WorldPosition position)
        {
            if (value1 == value2)
            {
                var chunk = this.GetChunk(position);
                if (chunk != null)
                {
                    chunk.UpdateRequired = true;
                }
            }
        }
    }
}