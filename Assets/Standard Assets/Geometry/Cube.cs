using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{
    public static class Cube
    {

        public static MeshBuilder Mesh(float length)
        {
            return Mesh(length, 0);
        }

        public static MeshBuilder Mesh(float length, float maxOffset)
        {
            MeshBuilder mesh = new MeshBuilder();

            // Vertices

            Vector3 v0 = new Vector3(-length * .5f, -length * .5f, -length * .5f);
            Vector3 v1 = new Vector3(-length * .5f, length * .5f, -length * .5f);
            Vector3 v2 = new Vector3(length * .5f, length * .5f, -length * .5f);
            Vector3 v3 = new Vector3(length * .5f, -length * .5f, -length * .5f);
            Vector3 v4 = new Vector3(-length * .5f, -length * .5f, length * .5f);
            Vector3 v5 = new Vector3(-length * .5f, length * .5f, length * .5f);
            Vector3 v6 = new Vector3(length * .5f, length * .5f, length * .5f);
            Vector3 v7 = new Vector3(length * .5f, -length * .5f, length * .5f);

           if (maxOffset != 0)
            {
                v0 += new Vector3(UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset));
                v1 += new Vector3(UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset));
                v2 += new Vector3(UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset));
                v3 += new Vector3(UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset));
                v4 += new Vector3(UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset));
                v5 += new Vector3(UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset));
                v6 += new Vector3(UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset));
                v7 += new Vector3(UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset), UnityEngine.Random.Range(0, maxOffset));

            }

            mesh.vertices.Add(v0);
            mesh.vertices.Add(v1);
            mesh.vertices.Add(v2);
            mesh.vertices.Add(v3);

            mesh.vertices.Add(v1);
            mesh.vertices.Add(v5);
            mesh.vertices.Add(v6);
            mesh.vertices.Add(v2);

            mesh.vertices.Add(v2);
            mesh.vertices.Add(v6);
            mesh.vertices.Add(v7);
            mesh.vertices.Add(v3);

            mesh.vertices.Add(v3);
            mesh.vertices.Add(v7);
            mesh.vertices.Add(v4);
            mesh.vertices.Add(v0);

            mesh.vertices.Add(v0);
            mesh.vertices.Add(v4);
            mesh.vertices.Add(v5);
            mesh.vertices.Add(v1);

            mesh.vertices.Add(v7);
            mesh.vertices.Add(v6);
            mesh.vertices.Add(v5);
            mesh.vertices.Add(v4);

            // Triangles

            for (int i=0; i<6; i++)
            {
                mesh.triangles.Add(i * 4);
                mesh.triangles.Add(i * 4 + 1);
                mesh.triangles.Add(i * 4 + 2);
                mesh.triangles.Add(i * 4);
                mesh.triangles.Add(i * 4 + 2);
                mesh.triangles.Add(i * 4 + 3);
            }


            // Normals
            mesh.normals.Add(Vector3.back);
            mesh.normals.Add(Vector3.back);
            mesh.normals.Add(Vector3.back);
            mesh.normals.Add(Vector3.back);

            mesh.normals.Add(Vector3.up);
            mesh.normals.Add(Vector3.up);
            mesh.normals.Add(Vector3.up);
            mesh.normals.Add(Vector3.up);

            mesh.normals.Add(Vector3.right);
            mesh.normals.Add(Vector3.right);
            mesh.normals.Add(Vector3.right);
            mesh.normals.Add(Vector3.right);

            mesh.normals.Add(Vector3.down);
            mesh.normals.Add(Vector3.down);
            mesh.normals.Add(Vector3.down);
            mesh.normals.Add(Vector3.down);

            mesh.normals.Add(Vector3.left);
            mesh.normals.Add(Vector3.left);
            mesh.normals.Add(Vector3.left);
            mesh.normals.Add(Vector3.left);

            mesh.normals.Add(Vector3.forward);
            mesh.normals.Add(Vector3.forward);
            mesh.normals.Add(Vector3.forward);
            mesh.normals.Add(Vector3.forward);

            return mesh;
        }
    }
}
