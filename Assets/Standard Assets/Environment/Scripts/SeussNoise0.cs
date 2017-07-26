using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    // farly standary dunes with hills

    public class SeussNoise0 : INoiseProvider
    {
        public float seed { get; set; }

        float turbulenceFreq = 1/80.0f;
        float turbulenceAmp = 120.0f; //80.0f;
        float sinAmplitude = 5.0f;
        float sinFreq = 1 / 12.0f;
        float cutoffRatio = 0.2f;

        float hillMax = 30.0f;
        float hillFrequency = 1 / 120.0f;

        float rMax = 100.0f;
        float rWidth = 40.0f;

        /*
                float turbulenceFreq = 1 / 80.0f;
        float turbulenceAmp = 80.0f;
        float sinAmplitude = 1.5f;
        float sinFreq = 1 / 8.0f;
        float cutoffRatio = 0.2f;
        */

        public float GetValue(float x, float y, float seed)
        {

            // create dunes
            var x0 = 2 * Mathf.PerlinNoise((x + seed + 10000) * turbulenceFreq, (y + seed + 20000) * turbulenceFreq) - 1;
            var y0 = 2 * Mathf.PerlinNoise((x + seed + 30000) * turbulenceFreq, (y + seed + 40000) * turbulenceFreq) - 1;
            var result = sinAmplitude * (Mathf.Sin((x + turbulenceAmp * x0) * sinFreq) + Mathf.Sin((y + turbulenceAmp * x0) * sinFreq));
            if (result < sinAmplitude * cutoffRatio) result = sinAmplitude * cutoffRatio;

            // add large hills
            var hill = Mathf.PerlinNoise((x + seed + 10000) * hillFrequency, (y + seed + 20000) * hillFrequency);
            result += hill * hillMax;

            // cutoff at radius
            var radius = Mathf.Sqrt(x*x + y*y);
            if (radius > rMax)
            {
                result = 0;
            } else if (radius > rMax - rWidth)
            {
                result *= Mathf.SmoothStep(0, 1, (rMax-radius)/rWidth);
            }

            return result - 1;

        }
    }

}
