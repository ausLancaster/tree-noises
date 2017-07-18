using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;

namespace Terrain {

    public class HeightMap
    {
        public float[,] map;
        public int width;
        public int length;
        public float size;

        public HeightMap(int width, int length, float size) {

            this.width = width;
            this.length = length;
            this.size = size;

            map = new float[width, length];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    float x = size * (i - width/2);
                    float y = size * (j - length/2);

                    map[i, j] = SeussLike(x, y, 1/40.0f, 40.0f, 2.0f, 1/4.0f);

                    // nice compact seusslike map[i, j] = SeussLike(x, y, 1/20.0f, 16.0f, 1.0f, 1/2.0f);
                }
            }
        }

        public static float SeussLike(float x, float y, float perlinFreq, float turbulence, float amplitude, float sinFreq)
        {
            float xSeed = UnityEngine.Random.Range(0, 10000);
            float ySeed = UnityEngine.Random.Range(0, 10000);

            float x0 = 2 * Mathf.PerlinNoise(x *perlinFreq + 10000, y * perlinFreq + 10000) - 1;
            float y0 = 2 * Mathf.PerlinNoise(x * perlinFreq + 20000, y * perlinFreq + 20000) - 1;
            float result = amplitude * (Mathf.Sin((x + turbulence * x0) * sinFreq) + Mathf.Sin((y + turbulence * x0) * sinFreq));
            //result *= Mathf.PerlinNoise(x * (perlinFreq / 2) + 10000, y * (perlinFreq / 2) + 10000);

            if (result < 0) result = 0;
            return result;
        }

        /* nice "seuss-like" terrain
        float x0 = 2 * Mathf.PerlinNoise(x / 20.0f + 10000, y / 20.0f + 10000) - 1;
        float y0 = 2 * Mathf.PerlinNoise(x / 20.0f + 20000, y / 20.0f + 20000) - 1;
        map[i, j] = 1.0f * (Mathf.Sin((x+16*x0)/2.0f) + Mathf.Sin((y+16*x0)/2.0f));
        */

        public MeshGenerator Generate()
        {
            MeshGenerator mg = new MeshGenerator();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    float x = size * (i - width / 2);
                    float y = size * (j - length / 2);

                    mg.vertices.Add(new Vector3(x, map[i, j], y));
                }
            }

            for (int i=0; i<map.GetLength(0)-1; i++)
            {
                for (int j=0; j<map.GetLength(1)-1; j++)
                {
                    int x00 = i + j * width;
                    int x01 = i + (j + 1) * width;
                    int x10 = i + 1 + j * width;
                    int x11 = i + 1 + (j + 1) * width;

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
                int ia = mg.triangles[i];
                int ib = mg.triangles[i+1];
                int ic = mg.triangles[i+2];

                Vector3 e1 = mg.vertices[ia] - mg.vertices[ib];
                Vector3 e2 = mg.vertices[ic] - mg.vertices[ib];
                Vector3 no = Vector3.Cross(e1, e2);

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
