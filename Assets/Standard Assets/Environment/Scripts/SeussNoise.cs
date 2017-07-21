using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class SeussNoise : INoiseProvider
    {
        float perlinFreq = 1 / 80.0f;
        float turbulence = 80.0f;
        float amplitude = 1.5f;
        float sinFreq = 1 / 8.0f;

        public float GetValue(float x, float y)
        {
            float x0 = 2 * Mathf.PerlinNoise(x * perlinFreq + 10000, y * perlinFreq + 10000) - 1;
            float y0 = 2 * Mathf.PerlinNoise(x * perlinFreq + 20000, y * perlinFreq + 20000) - 1;
            float result = amplitude * (Mathf.Sin((x + turbulence * x0) * sinFreq) + Mathf.Sin((y + turbulence * x0) * sinFreq));
            if (result < 0.2f) result = 0.2f;
            return result;

/* nice "seuss-like" terrain
float x0 = 2 * Mathf.PerlinNoise(x / 20.0f + 10000, y / 20.0f + 10000) - 1;
float y0 = 2 * Mathf.PerlinNoise(x / 20.0f + 20000, y / 20.0f + 20000) - 1;
map[i, j] = 1.0f * (Mathf.Sin((x+16*x0)/2.0f) + Mathf.Sin((y+16*x0)/2.0f));
*/
        }
    }

}
