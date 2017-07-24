using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{

    public class TreePlacer : IFeatureGenerator
    {

        private GameObject tree;
        private float length;
        private float spacing = 20.0f;
        private float maxDiplacement = 9.0f;
        private float noiseThreshold = 0.4f;
        private GameObject trees;

        public TreePlacer(float length)
        {
            this.length = length;

            tree = (GameObject)Resources.Load("Tree", typeof(GameObject));
            trees = new GameObject("Trees");
        }

        // Use this for initialization
        public void Generate(Vector2i position, INoiseProvider noiseProvider)
        {

            for (float i = 0; i < length; i += spacing)
            {
                for (float j = 0 / 2; j < length; j += spacing)
                {
                    //if (i == 0 && j == 0) continue;
                    if (Random.value > noiseThreshold)
                    {
                        float xOffset = Random.Range(0, maxDiplacement);
                        float yOffset = Random.Range(0, maxDiplacement);
                        float xPosition = i + xOffset + position.X * length;
                        float yPosition = j + yOffset + position.Z * length;
                        Vector3 treePosition = new Vector3(xPosition, noiseProvider.GetValue(xPosition, yPosition), yPosition);
                        MonoBehaviour.Instantiate(tree, treePosition, Quaternion.identity, trees.transform);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
