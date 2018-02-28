namespace Assets.Logic
{
    /// <summary>
    /// Defines the location on the tile map.
    /// </summary>
    public struct Tile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Tile(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets the X coordinate.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate.
        /// </summary>
        public int Y { get; private set; }
    }
}
