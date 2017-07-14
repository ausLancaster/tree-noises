using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{
    public class MeshGenerator
    {

        public List<Vector3> vertices;
        public List<int> triangles;
        public List<Vector3> normals;

        public MeshGenerator()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();
            normals = new List<Vector3>();
        }

        public void Add(MeshGenerator mg)
        {
            int oldSize = vertices.Count;

            vertices.AddRange(mg.vertices);
            normals.AddRange(mg.normals);

            for (int i=0; i<mg.triangles.Count; i++)
            {
                triangles.Add(mg.triangles[i] + oldSize);
            }

        }

        public Mesh Generate()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.normals = normals.ToArray();

            return mesh;
        }

        public void Rotate(Quaternion quat)
        {
            for (int i=0; i < vertices.Count; i++)
            {
                vertices[i] = quat * vertices[i];
                normals[i] = quat * normals[i];
            }
        }

        public void Translate(Vector3 translation)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i] = vertices[i] + translation;
            }
        }
    }
}
