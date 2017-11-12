using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public interface IFeatureGenerator
    {

        void Generate(float seed, INoiseProvider noiseProvider);
    }
}
