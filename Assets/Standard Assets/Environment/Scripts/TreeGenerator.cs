using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSys;
using Geometry;

public class TreeGenerator : MonoBehaviour {

    public Material material;

	// Use this for initialization
	void Awake () {

        Dictionary<string, List<Rule>> grammar = new Dictionary<string, List<Rule>>();
        grammar["T"] = new List<Rule>();
        grammar["T"].Add(new Rule(1.0f, "A[#T]T"));
        grammar["A"] = new List<Rule>();
        grammar["A"].Add(new Rule(0.6f, "AA"));
        grammar["A"].Add(new Rule(0.4f, "A"));

        LSystem LS = new LSystem("T", grammar);
        LinkedListA<string> ll = LS.DoIterations(UnityEngine.Random.Range(3,7));
        Turtle turtle = new Turtle(null, 18);
        turtle.RenderSymbols(ll);

        MeshGenerator mg = turtle.meshGenerator;
        GetComponent<MeshFilter>().mesh = mg.Generate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
