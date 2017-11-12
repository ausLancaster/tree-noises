using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSys;
using Geometry;
using UnityEngine.Profiling;

public class TreeGenerator : MonoBehaviour {

    public Material material;
    private Turtle turtle;
    private LinkedListA<string> ll;
    private int seed;

	// Use this for initialization
	void Awake () {

        seed = System.DateTime.Now.Millisecond;

        Dictionary<string, List<Rule>> grammar = new Dictionary<string, List<Rule>>();
        grammar["T"] = new List<Rule>();
        grammar["T"].Add(new Rule(1.0f, "A[#T]T"));
        grammar["A"] = new List<Rule>();
        grammar["A"].Add(new Rule(0.6f, "AA"));
        grammar["A"].Add(new Rule(0.4f, "A"));

        Profiler.BeginSample("DoLSystem");
        LSystem LS = new LSystem("T", grammar);
        ll = LS.DoIterations(UnityEngine.Random.Range(3,7));
        Profiler.EndSample();
        DoTurtle(0);


	}

    public void DoTurtle(float breath)
    {
        Random.InitState(seed);
        turtle = new LSys.Turtle(breath);
        Profiler.BeginSample("render symbols");
        turtle.RenderSymbols(ll);
        Profiler.EndSample();
        MeshBuilder mg = turtle.meshGenerator;
        GetComponent<MeshFilter>().mesh = mg.Generate();
    }
}
