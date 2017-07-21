using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;

namespace Terrain {

    public class Terrain
    {
        private float[,] heightMap;
        private float length;
        private float height;

        public Terrain(float[,] heightMap, float length, float height) {

            this.heightMap = heightMap;
            this.length = length;
            this.height = height;
        }

        public MeshBuilder Generate()
        {
            var mg = new MeshBuilder();

            for (int i = 0; i < heightMap.GetLength(0); i++)
            {
                for (int j = 0; j < heightMap.GetLength(1); j++)
                {
                    float x = i * (length / (heightMap.GetLength(0)-1));
                    float y = j * (length / (heightMap.GetLength(1)-1));

                    mg.vertices.Add(new Vector3(x, heightMap[i, j], y));
                }
            }

            for (int i=0; i< heightMap.GetLength(0)-1; i++)
            {
                for (int j=0; j< heightMap.GetLength(1)-1; j++)
                {
                    int x00 = i + j * heightMap.GetLength(0);
                    int x01 = i + (j + 1) * heightMap.GetLength(0);
                    int x10 = i + 1 + j * heightMap.GetLength(0);
                    int x11 = i + 1 + (j + 1) * heightMap.GetLength(0);

                    mg.triangles.Add(x00);
                    mg.triangles.Add(x10);
                    mg.triangles.Add(x11);

                    mg.triangles.Add(x00);
                    mg.triangles.Add(x11);
                    mg.triangles.Add(x01);

                }
            }

            for (int i = 0; i < mg.vertices.Count; i++)
            {
                mg.normals.Add(Vector3.zero);
            }

            for (int i=0; i<mg.triangles.Count; i+=3)
            {
                var ia = mg.triangles[i];
                var ib = mg.triangles[i+1];
                var ic = mg.triangles[i+2];

                var e1 = mg.vertices[ia] - mg.vertices[ib];
                var e2 = mg.vertices[ic] - mg.vertices[ib];
                var no = Vector3.Cross(e1, e2);

                mg.normals[ia] += no;
                mg.normals[ib] += no;
                mg.normals[ic] += no;
            }

            for (int i = 0; i < mg.normals.Count; i++)
            {
                mg.normals[i] = Vector3.Normalize(mg.normals[i]);
            }

            return mg;
        }
    }


}
