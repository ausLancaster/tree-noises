using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{
    public static class Cylinder
    {
        public static List<int> verticesToBeUntwisted;
        public static List<int> normalsToBeUntwisted;

        public static MeshGenerator Mesh(float radius, float length, int segments)
        {
            return Mesh(radius, radius, length, segments);
        }

        public static MeshGenerator Mesh(float radiusBottom, float radiusTop, float length, int segments)
        {
            return Mesh(radiusBottom, radiusTop, length, segments, Quaternion.identity);
        }

        // Use this for initialization
        public static MeshGenerator Mesh(float radiusBottom, float radiusTop, float length, int segments, Quaternion twist)
        {
            MeshGenerator mesh = new MeshGenerator();
            float angle = (Mathf.PI * 2) / segments;
            verticesToBeUntwisted = new List<int>();
            normalsToBeUntwisted = new List<int>();

            // Vertices
            Vector3[] vertices = new Vector3[segments * 6 + 2];
            int bottom = vertices.Length - 2;
            int top = vertices.Length - 1;

            for (int i = 0; i < segments; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector3 v = new Vector3(radiusBottom * Mathf.Cos(-angle * i), 0, radiusBottom * Mathf.Sin(-angle * i));
                    verticesToBeUntwisted.Add(mesh.vertices.Count);
                    mesh.vertices.Add(v);
                }
            }

            for (int i = 0; i < segments; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mesh.vertices.Add(new Vector3(radiusTop * Mathf.Cos(-angle * i), length, radiusTop * Mathf.Sin(-angle * i)));
                }
            }

            mesh.vertices.Add(new Vector3(0, 0, 0));
            mesh.vertices.Add(new Vector3(0, length, 0));

            // Triangles
            for (int i = 0; i < segments; i++)
            {
                int v0 = i * 3;
                int v1 = i * 3 + 3; if (v1 == segments * 3) v1 = 0;
                int v2 = i * 3 + segments * 3;
                int v3 = i * 3 + segments * 3 + 3; if (v3 == segments * 6) v3 = segments * 3;

                mesh.triangles.Add(v0);
                mesh.triangles.Add(bottom);
                mesh.triangles.Add(v1);

                mesh.triangles.Add(v2);
                mesh.triangles.Add(v3);
                mesh.triangles.Add(top);

                mesh.triangles.Add(v0 + 1);
                mesh.triangles.Add(v1 + 2);
                mesh.triangles.Add(v2 + 1);

                mesh.triangles.Add(v1 + 2);
                mesh.triangles.Add(v3 + 2);
                mesh.triangles.Add(v2 + 1);
            }

            // Normals


            for (int i = 0; i < segments; i++)
            {
                normalsToBeUntwisted.Add(mesh.normals.Count);
                mesh.normals.Add(Vector3.down);
                normalsToBeUntwisted.Add(mesh.normals.Count);
                mesh.normals.Add(new Vector3(Mathf.Cos(-angle * i - angle / 2), 0, Mathf.Sin(-angle * i - angle / 2)));
                normalsToBeUntwisted.Add(mesh.normals.Count);
                mesh.normals.Add(new Vector3(Mathf.Cos(-angle * i + angle / 2), 0, Mathf.Sin(-angle * i + angle / 2)));
            }

            for (int i = 0; i < segments; i++)
            {
                mesh.normals.Add(Vector3.up);
                mesh.normals.Add(new Vector3(Mathf.Cos(-angle * i - angle / 2), 0, Mathf.Sin(-angle * i - angle / 2)));
                mesh.normals.Add(new Vector3(Mathf.Cos(-angle * i + angle / 2), 0, Mathf.Sin(-angle * i + angle / 2)));
            }
            normalsToBeUntwisted.Add(mesh.normals.Count);
            mesh.normals.Add(Vector3.down);
            mesh.normals.Add(Vector3.up);

            return mesh;
        }
    }
}
