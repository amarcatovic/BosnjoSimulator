using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Foliage
{
    public class Foliage2D_Mesh
    {
        #region Fields and Properties
        /// <summary>
        /// List of vertices to build a mesh from.
        /// </summary>
        private List<Vector3> meshVerts;
        /// <summary>
        /// List of vertex indices that form triangles.
        /// </summary>
        private List<int> meshIndices;
        /// <summary>
        /// List of UV coordinates for each vertex.
        /// </summary>
        private List<Vector2> meshUVs;
        #endregion

        #region Constructor
        public Foliage2D_Mesh()
        {
            meshVerts = new List<Vector3>();
            meshUVs = new List<Vector2>();
            meshIndices = new List<int>();
        }
        #endregion

        #region General Methods
        /// <summary>
        /// Clears all verices, indices, uvs from this mesh.
        /// </summary>
        public void Clear()
        {
            meshVerts.Clear();
            meshIndices.Clear();
            meshUVs.Clear();
        }

        /// <summary>
        /// Clears out the mesh, fills in the data, and recalculates normals and bounds.
        /// </summary>
        /// <param name="mesh">An already existing mesh to fill out.</param>
        public void Build(ref Mesh mesh)
        {
            // round off a few decimal points to try and get better pixel-perfect results
            for (int i = 0; i < meshVerts.Count; i += 1)
                meshVerts[i] = new Vector3(
                         (float)System.Math.Round(meshVerts[i].x, 3),
                         (float)System.Math.Round(meshVerts[i].y, 3),
                         (float)System.Math.Round(meshVerts[i].z, 3));

            mesh.Clear();
            mesh.vertices = meshVerts.ToArray();
            mesh.uv = meshUVs.ToArray();
            mesh.triangles = meshIndices.ToArray();

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        #endregion
        #region Class Methods
        /// <summary>
        /// Generates triangles from a list of vertices.
        /// </summary>
        /// <param name="widthSegments">The number of horizontal segments.</param>
        /// <param name="heightSegments">The number of vertical segments.</param>
        /// <param name="hVertices">The number of horizontal vertices.</param>
        public void GenerateTriangles(int widthSegments, int heightSegments, int hVertices)
        {
            for (int y = 0; y < heightSegments; y++)
            {
                for (int x = 0; x < widthSegments; x++)
                {
                    meshIndices.Add((y * hVertices) + x);
                    meshIndices.Add(((y + 1) * hVertices) + x);
                    meshIndices.Add((y * hVertices) + x + 1);

                    meshIndices.Add(((y + 1) * hVertices) + x);
                    meshIndices.Add(((y + 1) * hVertices) + x + 1);
                    meshIndices.Add((y * hVertices) + x + 1);
                }
            }
        }

        /// <summary>
        /// Adds a vertex to the meshVerts list and a UV point to the meshUVs list.
        /// </summary>
        /// <param name="vertexPoss">The position of a vertex.</param>
        /// <param name="z">The position of a vertex on the Z axis.</param>
        /// <param name="UV">The UV coordinate of the current vertex.</param>
        public void AddVertex(Vector2 vertexPoss, float z, Vector2 UV)
        {
            meshVerts.Add(new Vector3(vertexPoss.x, vertexPoss.y, z));
            meshUVs.Add(UV);
        }
        #endregion
    }
}
