using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;

public class CubeGenerator : MonoBehaviour {

    private float groundHeight = 0.22f;
    private float height = 0.2f;
    private float offSet = 0.3f;
    private float minLength = 0.2f;
    private float maxLength = 0.4f;

	// Use this for initialization
	void Start () {
        MeshBuilder mg = Cube.Mesh(UnityEngine.Random.Range(minLength, maxLength), offSet);
        mg.Rotate(UnityEngine.Random.rotationUniform);
        GetComponent<MeshFilter>().mesh = mg.Generate();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Interact")) {
            Start();
        }


    }
}
