namespace Assets.Logic
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The defines the data of a mesh.
    /// </summary>
    public class MeshData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeshData"/> class.
        /// </summary>
        public MeshData()
        {
            this.Vertices = new List<Vector3>();
            this.Triangles = new List<int>();
            this.Uv = new List<Vector2>();
            this.ColTriangles = new List<int>();
            this.ColVertices = new List<Vector3>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the mesh data should use render data for the col.
        /// </summary>
        public bool UseRenderDataForCol { get; set; }

        /// <summary>
        /// Gets the vertices.
        /// </summary>
        public List<Vector3> Vertices { get; private set; }

        /// <summary>
        /// Gets the triangles.
        /// </summary>
        public List<int> Triangles { get; private set; }

        /// <summary>
        /// Gets the uvs.
        /// </summary>
        public List<Vector2> Uv { get; private set; }

        /// <summary>
        /// Gets the col vertices.
        /// </summary>
        public List<Vector3> ColVertices { get; private set; }

        /// <summary>
        /// Gets the col triangles.
        /// </summary>
        public List<int> ColTriangles { get; private set; }

        /// <summary>
        /// Adds a vertex shader to the mesh data.
        /// </summary>
        /// <param name="vertex">The vertex to add.</param>
        public void AddVertex(Vector3 vertex)
        {
            this.Vertices.Add(vertex);
            if (this.UseRenderDataForCol)
            {
                this.ColVertices.Add(vertex);
            }
        }

        /// <summary>
        /// Adds a triangle.
        /// </summary>
        /// <param name="tri">The triangle to add.</param>
        public void AddTriangle(int tri)
        {
            this.Triangles.Add(tri);
            if (this.UseRenderDataForCol)
            {
                this.ColTriangles.Add(tri - (this.Vertices.Count - this.ColVertices.Count));
            }
        }

        /// <summary>
        /// Adds triangles to the meshdata.
        /// </summary>
        public void AddQuadTriangles()
        {
            this.Triangles.Add(this.Vertices.Count - 4);
            this.Triangles.Add(this.Vertices.Count - 3);
            this.Triangles.Add(this.Vertices.Count - 2);
            this.Triangles.Add(this.Vertices.Count - 4);
            this.Triangles.Add(this.Vertices.Count - 2);
            this.Triangles.Add(this.Vertices.Count - 1);
            if (this.UseRenderDataForCol)
            {
                this.ColTriangles.Add(this.ColVertices.Count - 4);
                this.ColTriangles.Add(this.ColVertices.Count - 3);
                this.ColTriangles.Add(this.ColVertices.Count - 2);
                this.ColTriangles.Add(this.ColVertices.Count - 4);
                this.ColTriangles.Add(this.ColVertices.Count - 2);
                this.ColTriangles.Add(this.ColVertices.Count - 1);
            }
        }
    }
}
