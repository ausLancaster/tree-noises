using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{
    public static class Leaf
    {

        // Use this for initialization
        public static MeshBuilder Mesh(float height, float width)
        {
            MeshBuilder mesh = new MeshBuilder();

            // Vertices

            for (int i=0; i<2; i++)
            {
                mesh.vertices.Add(new Vector3(0, 0, 0));
                mesh.vertices.Add(new Vector3(-width/2, height / 4, 0));
                mesh.vertices.Add(new Vector3(width/2, height / 4, 0));
                mesh.vertices.Add(new Vector3(-width / 2, (3 * height) / 4, 0));
                mesh.vertices.Add(new Vector3(width/2, (3 * height) / 4, 0));
                mesh.vertices.Add(new Vector3(0, height, 0));
            }

            // Triangles
            mesh.triangles.Add(0);
            mesh.triangles.Add(1);
            mesh.triangles.Add(2);

            mesh.triangles.Add(1);
            mesh.triangles.Add(3);
            mesh.triangles.Add(2);

            mesh.triangles.Add(2);
            mesh.triangles.Add(3);
            mesh.triangles.Add(4);

            mesh.triangles.Add(3);
            mesh.triangles.Add(5);
            mesh.triangles.Add(4);

            mesh.triangles.Add(6);
            mesh.triangles.Add(8);
            mesh.triangles.Add(7);

            mesh.triangles.Add(8);
            mesh.triangles.Add(10);
            mesh.triangles.Add(7);

            mesh.triangles.Add(7);
            mesh.triangles.Add(10);
            mesh.triangles.Add(9);

            mesh.triangles.Add(10);
            mesh.triangles.Add(11);
            mesh.triangles.Add(9);


            // Normals
            for (int i=0; i<6; i++)
            {
                mesh.normals.Add(Vector3.back);
            }

            for (int i = 0; i < 6; i++)
            {
                mesh.normals.Add(Vector3.forward);
            }

            return mesh;
        }
    }
}
