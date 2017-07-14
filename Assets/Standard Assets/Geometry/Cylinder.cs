using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{
    public static class Cylinder
    {

        // Use this for initialization
        public static MeshGenerator Mesh(float radius, float length, int segments)
        {
            MeshGenerator mesh = new MeshGenerator();
            float angle = (Mathf.PI * 2) / segments;

            // Vertices
            Vector3[] vertices = new Vector3[segments * 6 + 2];
            int bottom = vertices.Length - 2;
            int top = vertices.Length - 1;

            for (int i = 0; i < segments; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mesh.vertices.Add(new Vector3(radius * Mathf.Cos(-angle * i), 0, radius * Mathf.Sin(-angle * i)));
                }
            }

            for (int i = 0; i < segments; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mesh.vertices.Add(new Vector3(radius * Mathf.Cos(-angle * i), length, radius * Mathf.Sin(-angle * i)));
                }
            }

            mesh.vertices.Add(new Vector3(0, 0, 0));
            mesh.vertices.Add(new Vector3(0, length, 0));

            // Triangles
            for (int i = 0; i < segments; i++)
            {
                int v0 = i*3;
                int v1 = i*3 + 3; if (v1 == segments * 3) v1 = 0;
                int v2 = i*3 + segments * 3;
                int v3 = i*3 + segments * 3 + 3; if (v3 == segments * 6) v3 = segments * 3;

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
                mesh.normals.Add(Vector3.down);
                mesh.normals.Add(new Vector3(Mathf.Cos(-angle * i - angle / 2), 0, Mathf.Sin(-angle * i - angle / 2)));
                mesh.normals.Add(new Vector3(Mathf.Cos(-angle * i + angle / 2), 0, Mathf.Sin(-angle * i + angle / 2)));
            }

            for (int i = 0; i < segments; i++)
            {
                mesh.normals.Add(Vector3.up);
                mesh.normals.Add(new Vector3(Mathf.Cos(-angle * i - angle / 2), 0, Mathf.Sin(-angle * i - angle / 2)));
                mesh.normals.Add(new Vector3(Mathf.Cos(-angle * i + angle / 2), 0, Mathf.Sin(-angle * i + angle / 2)));
            }

            mesh.normals.Add(Vector3.down);
            mesh.normals.Add(Vector3.up);

            return mesh;
        }
    }
}
