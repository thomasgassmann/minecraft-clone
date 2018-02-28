namespace Assets.Logic
{
    using System;

    /// <summary>
    /// Defines a location in the world.
    /// </summary>
    [Serializable]
    public struct WorldPosition : IEquatable<WorldPosition>, ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldPosition"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        public WorldPosition(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate.
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Gets a value indicating whether the given world position equals the current world position.
        /// </summary>
        /// <param name="other">The other world position to compare.</param>
        /// <returns>Returns a value indicating the equality of the two given objects.</returns>
        public bool Equals(WorldPosition other)
        {
            return other.GetHashCode() == this.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is WorldPosition)
            {
                return this.Equals((WorldPosition)obj);
            }

            return false;
        }

        /// <summary>
        /// Compares two world position.
        /// </summary>
        /// <param name="left">The left world position,</param>
        /// <param name="right">The right world position.</param>
        /// <returns>Returns two if the two world positions are equal.</returns>
        public static bool operator ==(WorldPosition left, WorldPosition right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares the inequality two world position.
        /// </summary>
        /// <param name="left">The left world position,</param>
        /// <param name="right">The right world position.</param>
        /// <returns>Returns two if the two world positions aren't equal.</returns>
        public static bool operator !=(WorldPosition left, WorldPosition right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 47;
                hash = hash * 227 + this.X.GetHashCode();
                hash = hash * 227 + this.Y.GetHashCode();
                hash = hash * 227 + this.Z.GetHashCode();
                return hash;
            }
        }

        /// <inheritdoc />
        public object Clone()
        {
            return new WorldPosition(this.X, this.Y, this.Z);
        }

        /// <summary>
        /// Clones the world position.
        /// </summary>
        /// <returns>Returns the world position.</returns>
        public WorldPosition ClonePosition()
        {
            return (WorldPosition)this.Clone();
        }
    }
}