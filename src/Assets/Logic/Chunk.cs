namespace Assets.Logic
{
    using Blocks;
    using System;
    using System.Diagnostics;
    using UnityEngine;

    /// <summary>
    /// Stores data of the blocks inside of a chunk and creates mesh of the voxels for rendering and collisons.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        /// <summary>
        /// Defines the x, y and z size of a chunk.
        /// </summary>
        public const int Size = 16;

        private MeshFilter filter;

        private MeshCollider meshCollider;

        private bool updateRequired = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        public Chunk()
        {
            this.Blocks = new Block[Chunk.Size, Chunk.Size, Chunk.Size];
        }

        /// <summary>
        /// Gets or sets a value indicating whether an update is required or not.
        /// </summary>
        public bool UpdateRequired
        {
            get { return this.updateRequired; }
            internal set { this.updateRequired = value; }
        }

        public bool IsRendered { get; set; }

        /// <summary>
        /// Gets the world containing the chunk.
        /// </summary>
        public World World { get; internal set; }

        /// <summary>
        /// Gets the position of the chunk in the world.
        /// </summary>
        public WorldPosition Position { get; internal set; }

        /// <summary>
        /// Gets or sets the blocks.
        /// </summary>
        public Block[,,] Blocks { get; set; }

        /// <summary>
        /// Checks whether the given index is inside a chunk or outside.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>Returns a value indicating if the given index is valid.</returns>
        public static bool InRange(int index)
        {
            return !(index < 0 || index >= Chunk.Size);
        }

        /// <summary>
        /// Checks whether the given index is inside a chunk or outside.
        /// </summary>
        /// <param name="index">The wold position to check.</param>
        /// <returns>Returns a value indicating if the given index is valid.</returns>
        public static bool InRange(WorldPosition position)
        {
            return Chunk.InRange(position.X) &&
                Chunk.InRange(position.Y) &&
                Chunk.InRange(position.Z);
        }

        /// <summary>
        /// Starts the chunk class.
        /// </summary>
        public void Start()
        {
            this.filter = this.gameObject.GetComponent<MeshFilter>();
            this.meshCollider = this.gameObject.GetComponent<MeshCollider>();
        }

        /// <summary>
        /// Updates the chunk class.
        /// </summary>
        public void Update()
        {
            if (this.UpdateRequired)
            {
                this.UpdateRequired = false;
                this.UpdateChunk();
            }
        }

        /// <summary>
        /// Gets a block at a specific location in the Chunk.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <returns>Returns the block at the given position.</returns>
        public Block GetBlock(WorldPosition position)
        {
            if (Chunk.InRange(position))
            {
                return this.Blocks[position.X, position.Y, position.Z];
            }

            return this.World.GetBlock(new WorldPosition(
                this.Position.X + position.X,
                this.Position.Y + position.Y,
                this.Position.Z + position.Z));
        }

        public void SetBlock(WorldPosition position, Block block)
        {
            if (Chunk.InRange(position))
            {
                this.Blocks[position.X, position.Y, position.Z] = block;
            }
            else
            {
                this.World.SetBlock(
                    new WorldPosition(
                        this.Position.X + position.X,
                        this.Position.Y + position.Y,
                        this.Position.Z + position.Z),
                    block);
            }
        }

        /// <summary>
        /// Updates the chunk.
        /// </summary>
        public void UpdateChunk()
        {
            this.IsRendered = true;
            var meshData = new MeshData();
            for (int x = 0; x < Chunk.Size; x++)
            {
                for (int y = 0; y < Chunk.Size; y++)
                {
                    for (int z = 0; z < Chunk.Size; z++)
                    {
                        this.Blocks[x, y, z].GetBlockData(this, new WorldPosition(x, y, z), meshData);
                    }
                }
            }

            this.Render(meshData);
        }

        public void SetBlocksUnmodified()
        {
            foreach (var block in this.Blocks)
            {
                block.HasChanged = false;
            }
        }

        /// <summary>
        /// Renders all meshes of the Chunk.
        /// </summary>
        /// <param name="meshData">The mesh data to render.</param>
        public void Render(MeshData meshData)
        {
            this.filter.mesh.Clear();
            this.filter.mesh.vertices = meshData.Vertices.ToArray();
            this.filter.mesh.triangles = meshData.Triangles.ToArray();
            this.filter.mesh.uv = meshData.Uv.ToArray();
            this.filter.mesh.RecalculateNormals();
            this.meshCollider.sharedMesh = null;
            var mesh = new Mesh();
            mesh.vertices = meshData.ColVertices.ToArray();
            mesh.triangles = meshData.ColTriangles.ToArray();
            mesh.RecalculateNormals();
            this.meshCollider.sharedMesh = mesh;
        }
    }
}