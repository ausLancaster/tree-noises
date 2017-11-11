using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    // farly standary dunes with hills

    public class SeussNoise0 : INoiseProvider
    {
        public float seed { get; set; }

        bool dunes = true;

        float turbulenceFreq = 1/80.0f;
        float turbulenceAmp = 120.0f; //80.0f;
        float sinAmplitude = 5.0f;
        float sinFreq = 1 / 12.0f;
        float cutoffRatio = 0.2f;

        float hillMax = 20.0f;
        float hillFrequency = 1 / 120.0f;

        int bumpsMin = 1;
        int bumpsMax = 6;

        float rCoastMin = 60.0f;
        float rCoastMax = 100.0f;
        float rBeginSlope = 50.0f;

        /*
                float turbulenceFreq = 1 / 80.0f;
        float turbulenceAmp = 80.0f;
        float sinAmplitude = 1.5f;
        float sinFreq = 1 / 8.0f;
        float cutoffRatio = 0.2f;
        */

        public SeussNoise0(bool dunes)
        {
            this.dunes = dunes;
        }

        public float GetValue(float x, float y, float seed)
        {
            Random.InitState((int)seed);
            float result = 0;

            // create dunes
            if (dunes)
            {
                var x0 = 2 * Mathf.PerlinNoise((x + seed + 10000) * turbulenceFreq, (y + seed + 20000) * turbulenceFreq) - 1;
                var y0 = 2 * Mathf.PerlinNoise((x + seed + 30000) * turbulenceFreq, (y + seed + 40000) * turbulenceFreq) - 1;
                var dunes = sinAmplitude * (Mathf.Sin((x + turbulenceAmp * x0) * sinFreq) + Mathf.Sin((y + turbulenceAmp * x0) * sinFreq));
                if (dunes < sinAmplitude * cutoffRatio) dunes = sinAmplitude * cutoffRatio;
                result += dunes;
            }


            // add large hills
            var hill = Mathf.PerlinNoise((x + seed + 10000) * hillFrequency, (y + seed + 20000) * hillFrequency);
            result += hill * hillMax;


            // calculate coastline
            var angle = Mathf.Atan(y/x);
            var startAngle = Random.Range(0, 2 * Mathf.PI);
            int bumps = Random.Range(bumpsMin, bumpsMax+1);
            var r1 = (Mathf.Sin(startAngle + bumps * angle + Mathf.Cos((bumps + 3) * angle))+1)*0.5f;
            var rCoast = rCoastMin + (rCoastMax-rCoastMin) * r1;

            // cutoff at radius
            var radius = Mathf.Sqrt(x*x + y*y);
            if (radius > rCoast)
            {
                result = 0;
            } else if (radius > rBeginSlope)
            {
                result *= Mathf.SmoothStep(0, 1, (rCoast - radius)/(rCoast - rBeginSlope));
            }

            // raise flat version
            if (!dunes)
            {
                result += 1.1f;
            }

            return result - 1;

        }
    }

}
