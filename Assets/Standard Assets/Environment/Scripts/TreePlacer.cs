using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{

    public class TreePlacer : IFeatureGenerator
    {

        private GameObject tree;
        private float seed;
        private float length;
        private float spacing = 12.0f;
        private float maxDiplacement = 9.0f;
        private float noiseThreshold = 0.6f;
        private GameObject trees;

        public TreePlacer(float length)
        {
            this.length = length;

            tree = (GameObject)Resources.Load("Tree", typeof(GameObject));

        }

        // Use this for initialization
        public void Generate(float seed, INoiseProvider noiseProvider, TerrainChunkSettings settings)
        {
            trees = new GameObject("Trees");

            for (float i = -length/2; i < length/2; i += spacing)
            {
                for (float j = -length/2; j < length/2; j += spacing)
                {
                    if (Random.value > noiseThreshold)
                    {
                        // calculate tree position
                        float xOffset = Random.Range(0, maxDiplacement);
                        float zOffset = Random.Range(0, maxDiplacement);
                        float xPosition = i + xOffset;
                        float zPosition = j + zOffset;

                        if (noiseProvider.GetValue(xPosition, zPosition, seed) < 0.2f) continue;

                        // find y position by choosing lowest point from 4 nearest heightmap points
                        float radius = 0.45f;
                        float minHeight = noiseProvider.GetValue(xPosition-radius, zPosition-radius, seed);
                        float compareHeight = noiseProvider.GetValue(xPosition-radius, zPosition+radius, seed);
                        if (compareHeight < minHeight) minHeight = compareHeight;
                        compareHeight = noiseProvider.GetValue(xPosition+radius, zPosition-radius, seed);
                        if (compareHeight < minHeight) minHeight = compareHeight;
                        compareHeight = noiseProvider.GetValue(xPosition+radius, zPosition+radius, seed);
                        if (compareHeight < minHeight) minHeight = compareHeight;

                        // create and place tree
                        Vector3 treePosition = new Vector3(xPosition, minHeight, zPosition);
                        MonoBehaviour.Instantiate(tree, treePosition, Quaternion.identity, trees.transform);
                    }
                }
            }
        }

        public void Destroy()
        {
            GameObject.Destroy(trees);
        }
    }

}
