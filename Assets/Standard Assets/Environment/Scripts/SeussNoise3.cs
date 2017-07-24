using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    // Stepped terrain

    public class SeussNoise3 : INoiseProvider
    {
        float largeVariableFreq = 1 / 20.0f;

        float maxAmplitude = 20.0f;
        int steps = 10;
        float firstStep = 0.4f;
        float perlinFrequency = 1 / 100.0f;

        float cutoffRatio = 0.2f;

        public float GetValue(float x, float y)
        {

            // create dunes
            var result = Mathf.PerlinNoise(x * perlinFrequency + 10000, y * perlinFrequency + 20000);

            if (result < firstStep)
            {
                result = firstStep;
            } else
            {
                for (int i=1; i<=steps; i++)
                {
                    if (result < firstStep + (i*(1-firstStep))/steps)
                    {
                        result = firstStep + (i * (1 - firstStep)) / steps;
                        break;
                    }
                }
            }

            result *= maxAmplitude;

            return result;
        }
    }

}
