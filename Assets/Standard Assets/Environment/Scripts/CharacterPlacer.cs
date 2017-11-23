using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class CharacterPlacer : MonoBehaviour {

    public TerrainChunkGenerator tcg;
    private float relativeHeight = 0.98f;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, relativeHeight, 0);
        Place();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetButtonDown("Generate"))
        {
            Place();
        }

    }

    private void Place()
    {
        float newHeight = tcg.GetHeight(transform.position.x, transform.position.z) + relativeHeight;
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
