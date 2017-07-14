using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSys;
using Geometry;

public class Test : MonoBehaviour {

    public Material material;

	// Use this for initialization
	void Awake () {

        gameObject.AddComponent<MeshRenderer>();
        GetComponent<Renderer>().material = material;
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();

        LSystem LS = new LSystem();
        LinkedListA<string> ll = LS.DoIterations(4);
        Turtle turtle = new Turtle(null, 30);
        turtle.RenderSymbols(ll);

        MeshGenerator mg = turtle.meshGenerator;
        mf.mesh = mg.Generate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
