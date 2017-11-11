using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coloring {

    public static class ColorPalette
    {

        static Vector3 offset;
        static Vector3 amp;
        static Vector3 freq;
        static Vector3 phase;


        static ColorPalette()
        {
            offset = new Vector3(Random.Range(0.0f, 1.0f),
                                Random.Range(0.0f, 1.0f),
                                Random.Range(0.0f, 1.0f)
                                );

            amp = new Vector3(Random.Range(0.0f, 1.0f),
                        Random.Range(0.0f, 1.0f),
                        Random.Range(0.0f, 1.0f)
                        );

            freq = new Vector3(Random.Range(1.0f, 1.57f),
                        Random.Range(1.0f, 1.57f),
                        Random.Range(1.0f, 1.57f)
                        );

            phase = new Vector3(Random.Range(0.0f, 3.14f),
                        Random.Range(0.0f, 3.14f),
                        Random.Range(0.0f, 3.14f)
                        );
        }

        public static Color Sample(float t)
        {
            Color color =  new Color(
                offset.x + amp.x * Mathf.Cos(6.28318f * (freq.x * t + phase.x)),
                offset.y + amp.y * Mathf.Cos(6.28318f * (freq.y * t + phase.y)),
                offset.z + amp.z * Mathf.Cos(6.28318f * (freq.z * t + phase.z))
                );

            return color;
        }


    }

}


