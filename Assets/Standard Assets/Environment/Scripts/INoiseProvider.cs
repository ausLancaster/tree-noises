using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public interface INoiseProvider
    {
        // Use this for initialization
        float GetValue(float x, float z, float seed);
    }

}
