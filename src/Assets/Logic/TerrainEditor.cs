namespace Assets.Logic
{
    using UnityEngine;

    /// <summary>
    /// Contains usefull methods to modify the terrain of the map.
    /// </summary>
    public static class TerrainEditor
    {
        /// <summary>
        /// Gets the world position for a block by its <see cref="Vector3"/>.
        /// </summary>
        /// <param name="position">The position of the block.</param>
        /// <returns>Returns the world position.</returns>
        public static WorldPosition GetBlockPosition(Vector3 position)
        {
            return new WorldPosition(
                Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.y),
                Mathf.RoundToInt(position.z));
        }

        /// <summary>
        /// Gets the world position of a raycast hit.
        /// </summary>
        /// <param name="hit">The hit to get the position from.</param>
        /// <param name="adjacent">Adjacent the block.</param>
        /// <returns>Returns the position.</returns>
        public static WorldPosition GetBlockPosition(RaycastHit hit, bool adjacent = false)
        {
            return TerrainEditor.GetBlockPosition(new Vector3(
                TerrainEditor.MoveWithinBlock(hit.point.x, hit.normal.x, adjacent),
                TerrainEditor.MoveWithinBlock(hit.point.y, hit.normal.y, adjacent),
                TerrainEditor.MoveWithinBlock(hit.point.z, hit.normal.z, adjacent)));
        }

        /// <summary>
        /// Sets the selected block of the raycast.
        /// </summary>
        /// <param name="hit">The hit block.</param>
        /// <param name="block">The new block to set.</param>
        /// <param name="adjacent">Adjacent the block.</param>
        /// <returns>Returns a value indicating the success of the operation.</returns>
        public static bool SetBlock(RaycastHit hit, Block block, bool adjacent = false)
        {
            var chunk = hit.collider.GetComponent<Chunk>();
            if (chunk == null)
            {
                return false;
            }

            var position = TerrainEditor.GetBlockPosition(hit, adjacent);
            chunk.World.SetBlock(position, block);
            return true;
        }

        /// <summary>
        /// Gets a block at the given raycast hit.
        /// </summary>
        /// <param name="hit">The raycast hit.</param>
        /// <param name="adjacent">Adjacent the block.</param>
        /// <returns>Returns the block.</returns>
        public static Block GetBlock(RaycastHit hit, bool adjacent = false)
        {
            var chunk = hit.collider.GetComponent<Chunk>();
            return chunk == null ?
                null : chunk.World.GetBlock(TerrainEditor.GetBlockPosition(hit, adjacent));
        }

        /// <summary>
        /// Moves within the block.
        /// </summary>
        /// <param name="pos">The position to move.</param>
        /// <param name="norm">The normal value.</param>
        /// <param name="adjacent">Adjacent the block.</param>
        /// <returns>Returns the moved position.</returns>
        private static float MoveWithinBlock(float pos, float norm, bool adjacent)
        {
            return (pos - (int)pos == 0.5f || pos - (int)pos == -0.5f) ?
                (adjacent ? pos + norm / 2 : pos - norm / 2) :
                pos;
        }
    }
}