using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class RandomizeController : MonoBehaviour {

    public TerrainChunkGenerator generator;

	// Use this for initialization
	void Start () {
        generator.CreateTerrainChunk();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Generate"))
        {
            generator.RemoveTerrainChunk();
            generator.CreateTerrainChunk();
        }
    }
}
