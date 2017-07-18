using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class TerrainGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {

        HeightMap hm = new HeightMap(200, 200, 1.0f);
        Mesh mesh = hm.Generate().Generate();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }   
	
	// Update is called once per frame
	void Update () {
		
	}
}
