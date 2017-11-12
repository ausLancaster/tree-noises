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
        private float spacing = 20.0f;
        private float maxDiplacement = 9.0f;
        private float noiseThreshold = 0.0f;//0.4f;
        private GameObject trees;

        public TreePlacer(float length)
        {
            this.length = length;

            tree = (GameObject)Resources.Load("Tree", typeof(GameObject));
            trees = new GameObject("Trees");
        }

        // Use this for initialization
        public void Generate(float seed, INoiseProvider noiseProvider)
        {

            for (float i = -length/2; i < length/2; i += spacing)
            {
                for (float j = -length/2; j < length/2; j += spacing)
                {
                    //if (i == 0 && j == 0) continue;
                    if (Random.value > noiseThreshold)
                    {
                        float xOffset = Random.Range(0, maxDiplacement);
                        float yOffset = Random.Range(0, maxDiplacement);
                        float xPosition = i + xOffset;
                        float yPosition = j + yOffset;
                        Vector3 treePosition = new Vector3(xPosition, noiseProvider.GetValue(xPosition, yPosition, seed), yPosition);
                        MonoBehaviour.Instantiate(tree, treePosition, Quaternion.identity, trees.transform);
                    }
                }
            }
        }
    }

}
