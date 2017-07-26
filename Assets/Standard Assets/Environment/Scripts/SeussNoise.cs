using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class SeussNoise : INoiseProvider
    {

        float perlinFrequency = 1 / 100.0f;
        float perlinAmplitude = 20.0f;

        public float GetValue(float x, float y, float seed)
        {

            var result = Mathf.PerlinNoise(x * perlinFrequency + 10000, y * perlinFrequency + 20000);
            result -= Mathf.PerlinNoise(x * perlinFrequency + 20000, y * perlinFrequency + 30000);
            result *= perlinAmplitude;
            return result;
        }
    }

}
