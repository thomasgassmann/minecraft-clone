namespace Assets.Logic
{
    using UnityEngine;
    using System;

    /// <summary>
    /// Defines the main block class.
    /// </summary>
    [Serializable]
    public abstract class Block
    {
        private bool changed = true;

        /// <summary>
        /// Defines the tile size relative to the tile map. 
        /// </summary>
        public const float TileSize = 0.25f;

        public bool HasChanged
        {
            get { return this.changed; }
            set { this.changed = value; }
        }

        /// <summary>
        /// Gets the tile of the the texture in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the texture to get.</param>
        /// <returns>Retuns the position of the texture.</returns>
        public abstract Tile GetTexturePosition(Direction direction);

        /// <summary>
        /// Provides the <see cref="MeshData"/> of the <see cref="Block"/>.
        /// </summary>
        /// <param name="chunk">The chunk to get the mesh data from.</param>
        /// <param name="x">The x position of the block.</param>
        /// <param name="y">The y position of the block.</param>
        /// <param name="z">The z position of the block.</param>
        /// <param name="meshData">The meshdata.</param>
        public virtual void GetBlockData(Chunk chunk, WorldPosition position, MeshData meshData)
        {
            meshData.UseRenderDataForCol = true;
            var newPos = position.ClonePosition();
            newPos.Y++;
            if (!chunk.GetBlock(newPos).IsSolid(Direction.Down))
            {
                this.FaceDataUp(chunk, position, meshData);
            }

            newPos = position.ClonePosition();
            newPos.Y--;
            if (!chunk.GetBlock(newPos).IsSolid(Direction.Up))
            {
                this.FaceDataDown(chunk, position, meshData);
            }

            newPos = position.ClonePosition();
            newPos.Z++;
            if (!chunk.GetBlock(newPos).IsSolid(Direction.South))
            {
                this.FaceDataNorth(chunk, position, meshData);
            }

            newPos = position.ClonePosition();
            newPos.Z--;
            if (!chunk.GetBlock(newPos).IsSolid(Direction.North))
            {
                this.FaceDataSouth(chunk, position, meshData);
            }

            newPos = position.ClonePosition();
            newPos.X++;
            if (!chunk.GetBlock(newPos).IsSolid(Direction.West))
            {
                this.FaceDataEast(chunk, position, meshData);
            }

            newPos = position.ClonePosition();
            newPos.X--;
            if (!chunk.GetBlock(newPos).IsSolid(Direction.East))
            {
                this.FaceDataWest(chunk, position, meshData);
            }
        }

        /// <summary>
        /// Faces the mesh data up.
        /// </summary>
        /// <param name="chunk">The chunk.</param>
        /// <param name="position">The world coordinates.</param>
        /// <param name="meshData">The mesh data to edit.</param>
        protected virtual void FaceDataUp(Chunk chunk, WorldPosition position, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y + 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y + 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y + 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y + 0.5f, position.Z - 0.5f));
            meshData.AddQuadTriangles();
            meshData.Uv.AddRange(this.FaceUVs(Direction.Up));
        }

        /// <summary>
        /// Faces the mesh data down.
        /// </summary>
        /// <param name="chunk">The chunk.</param>
        /// <param name="position">The world coordinates.</param>
        /// <param name="meshData">The mesh data to edit.</param>
        protected virtual void FaceDataDown(Chunk chunk, WorldPosition position, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y - 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y - 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y - 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y - 0.5f, position.Z + 0.5f));
            meshData.AddQuadTriangles();
            meshData.Uv.AddRange(this.FaceUVs(Direction.Down));
        }

        /// <summary>
        /// Faces the mesh data north.
        /// </summary>
        /// <param name="chunk">The chunk.</param>
        /// <param name="position">The world coordinates.</param>
        /// <param name="meshData">The mesh data to edit.</param>
        protected virtual void FaceDataNorth(Chunk chunk, WorldPosition position, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y - 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y + 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y + 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y - 0.5f, position.Z + 0.5f));
            meshData.AddQuadTriangles();
            meshData.Uv.AddRange(this.FaceUVs(Direction.North));
        }

        /// <summary>
        /// Faces the mesh data east.
        /// </summary>
        /// <param name="chunk">The chunk.</param>
        /// <param name="position">The world coordinates.</param>
        /// <param name="meshData">The mesh data to edit.</param>
        protected virtual void FaceDataEast(Chunk chunk, WorldPosition position, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y - 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y + 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y + 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y - 0.5f, position.Z + 0.5f));
            meshData.AddQuadTriangles();
            meshData.Uv.AddRange(this.FaceUVs(Direction.East));
        }

        /// <summary>
        /// Faces the mesh data south.
        /// </summary>
        /// <param name="chunk">The chunk.</param>
        /// <param name="position">The world coordinates.</param>
        /// <param name="meshData">The mesh data to edit.</param>
        protected virtual void FaceDataSouth(Chunk chunk, WorldPosition position, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y - 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y + 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y + 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X + 0.5f, position.Y - 0.5f, position.Z - 0.5f));
            meshData.AddQuadTriangles();
            meshData.Uv.AddRange(this.FaceUVs(Direction.South));
        }

        /// <summary>
        /// Faces the mesh data west.
        /// </summary>
        /// <param name="chunk">The chunk.</param>
        /// <param name="position">The world coordinates.</param>
        /// <param name="meshData">The mesh data to edit.</param>
        protected virtual void FaceDataWest(Chunk chunk, WorldPosition position, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y - 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y + 0.5f, position.Z + 0.5f));
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y + 0.5f, position.Z - 0.5f));
            meshData.AddVertex(new Vector3(position.X - 0.5f, position.Y - 0.5f, position.Z - 0.5f));
            meshData.AddQuadTriangles();
            meshData.Uv.AddRange(this.FaceUVs(Direction.West));
        }

        /// <summary>
        /// Gets a value indicating whether the block is solid in the given direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <returns>The result of the operation.</returns>
        public virtual bool IsSolid(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return true;
                case Direction.East:
                    return true;
                case Direction.South:
                    return true;
                case Direction.West:
                    return true;
                case Direction.Up:
                    return true;
                case Direction.Down:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Faces the uvs for the given direction.
        /// </summary>
        /// <param name="direction">The direction to set the uvs to.</param>
        /// <returns>Returns the created vectors.</returns>
        public virtual Vector2[] FaceUVs(Direction direction)
        {
            var uvs = new Vector2[4];
            var tilePos = this.GetTexturePosition(direction);
            uvs[0] = new Vector2(Block.TileSize * tilePos.X + Block.TileSize,
                Block.TileSize * tilePos.Y);
            uvs[1] = new Vector2(Block.TileSize * tilePos.X + Block.TileSize,
                Block.TileSize * tilePos.Y + Block.TileSize);
            uvs[2] = new Vector2(Block.TileSize * tilePos.X,
                Block.TileSize * tilePos.Y + Block.TileSize);
            uvs[3] = new Vector2(Block.TileSize * tilePos.X,
                Block.TileSize * tilePos.Y);
            return uvs;
        }
    }
}
