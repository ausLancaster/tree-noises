using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    // thick roses, achieved by changing sinFreq according to large noise

    public class SeussNoise1 : INoiseProvider
    {
        float largeVariableFreq = 1 / 200.0f; // larger frequency for pointier roses

        float turbulenceFreq = 1 / 80.0f;
        float turbulenceAmp = 80.0f;
        float sinAmplitude = 1.5f;
        float sinFreq = 1 / 8.0f;

        float cutoffRatio = 0.2f;

        float hillMax = 20.0f;
        float hillFrequency = 1 / 200.0f;

        /*
        float perlinFreq = 1 / 80.0f;
        float turbulence = 80.0f;
        float amplitude = 1.5f;
        float sinFreq = 1 / 8.0f;
        float cutoffRatio = 0.2f;
        */

        public float GetValue(float x, float y, float seed)
        {
            // setUp large varibales
            var variable0 = Mathf.PerlinNoise((x+10000)* largeVariableFreq, (y+20000) * largeVariableFreq);
            var sinAmplitude = Mathf.Lerp(2.0f, 8.0f, variable0);
            var sinFreq = (1 / 10.0f) / sinAmplitude;

            // create dunes
            var x0 = 2 * Mathf.PerlinNoise((x+10000)* turbulenceFreq, (y+20000) * turbulenceFreq) - 1;
            var y0 = 2 * Mathf.PerlinNoise((x+30000)* turbulenceFreq, (y+ 40000) * turbulenceFreq) - 1;
            var result = sinAmplitude * (Mathf.Sin((x + turbulenceAmp * x0) * sinFreq) + Mathf.Sin((y + turbulenceAmp * x0) * sinFreq));
            if (result < sinAmplitude * cutoffRatio) result = sinAmplitude * cutoffRatio;

            // add large hills
            var hill = Mathf.PerlinNoise(x * hillFrequency + 10000, y * hillFrequency + 20000);
            result += hill * hillMax;
            return result;

        }
    }

}
