using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class Test : MonoBehaviour {

    public Material material;

	// Use this for initialization
	void Start () {

        var settings = new TerrainChunkSettings(129, 50, 0);
        var noiseProvider = new SeussNoise();
        for (int i=-1; i<2; i++)
        {
            for (int j=-1; j<2; j++)
            {
                var terrain = new TerrainChunk(settings, noiseProvider, i, j, material);
                terrain.CreateTerrain();
            }
        }


    }   
	
	// Update is called once per frame
	void Update () {
		
	}
}
