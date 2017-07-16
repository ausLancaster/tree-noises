using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSys;
using Geometry;

public class TreeGenerator : MonoBehaviour {

    public Material material;
    private Turtle turtle;
    private LinkedListA<string> ll;
    private int seed;

	// Use this for initialization
	void Awake () {

        seed = UnityEngine.Random.Range(0, 10000);

        Dictionary<string, List<Rule>> grammar = new Dictionary<string, List<Rule>>();
        grammar["T"] = new List<Rule>();
        grammar["T"].Add(new Rule(1.0f, "A[#T]T"));
        grammar["A"] = new List<Rule>();
        grammar["A"].Add(new Rule(0.6f, "AA"));
        grammar["A"].Add(new Rule(0.4f, "A"));

        LSystem LS = new LSystem("T", grammar);
        ll = LS.DoIterations(UnityEngine.Random.Range(3,7));
        DoTurtle(0);


	}

    public void DoTurtle(float breath)
    {
        Random.InitState(seed);
        turtle = new LSys.Turtle(breath);
        turtle.RenderSymbols(ll);
        MeshGenerator mg = turtle.meshGenerator;
        GetComponent<MeshFilter>().mesh = mg.Generate();
    }
}
