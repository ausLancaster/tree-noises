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
        private float spacing = 10.0f;
        private float maxDiplacement = 0.0f;//9.0f;
        private float noiseThreshold = 0.0f;//0.4f;
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
                    //if (i == 0 && j == 0) continue;
                    if (Random.value > noiseThreshold)
                    {
                        // calculate tree position
                        float xOffset = Random.Range(0, maxDiplacement);
                        float zOffset = Random.Range(0, maxDiplacement);
                        float xPosition = i + xOffset;
                        float zPosition = j + zOffset;


                        //float minHeight = noiseProvider.GetValue(xPosition, zPosition, seed);
                        // find y position by choosing lowest point from 4 nearest heightmap points
                        float radius = 0.45f;//settings.length / (settings.resolution - 1);
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
