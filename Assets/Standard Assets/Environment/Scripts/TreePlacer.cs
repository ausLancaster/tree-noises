using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacer : MonoBehaviour {

    public GameObject tree;


    private float groundWidth = 100.0f;
    private float spacing = 10.0f;
    private float maxDiplacement = 9.0f;
    private float noiseThreshold = 0.4f;

	// Use this for initialization
	void Awake () {

        for (float i=-groundWidth/2; i<=groundWidth/2; i+=spacing)
        {
            for (float j = -groundWidth / 2; j <= groundWidth / 2; j += spacing)
            {
                if (i == 0 && j == 0) continue;
                if (Random.value > noiseThreshold)
                {
                    float xOffset = Random.Range(0, maxDiplacement);
                    float yOffset = Random.Range(0, maxDiplacement);
                    Vector3 treePosition = new Vector3(i+xOffset, 0, j+yOffset);
                    Instantiate(tree, treePosition, Quaternion.identity, transform);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
