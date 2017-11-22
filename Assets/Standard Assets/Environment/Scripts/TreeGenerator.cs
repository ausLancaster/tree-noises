using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSys;
using Geometry;
using UnityEngine.Profiling;

public class TreeGenerator : MonoBehaviour {

    public Material material;
    private TurteInstructionSet instructionSet;
    private LinkedListA<string> ll;
    private int seed;

    private int iterationMin = 6; // 3
    private int iterationMax = 7; // 7

	// Use this for initialization
	void Awake () {

        seed = System.DateTime.Now.Millisecond;
        Random.InitState(seed);

        // create turtle instruction set
        instructionSet = new TurteInstructionSet();
        instructionSet.AddInstruction('[', new SaveInstruction());
        instructionSet.AddInstruction(']', new ResumeInstruction());
        instructionSet.AddInstruction('#', new RandomRotateInstruction());
        instructionSet.AddInstruction('A', new BranchInstruction());

        // create grammar
        Dictionary<string, List<Rule>> grammar = new Dictionary<string, List<Rule>>();
        grammar["T"] = new List<Rule>();
        grammar["T"].Add(new Rule(1.0f, "A[#T]T"));
        grammar["A"] = new List<Rule>();
        grammar["A"].Add(new Rule(0.6f, "AA"));
        grammar["A"].Add(new Rule(0.4f, "A"));

        LSystem LS = new LSystem("T", grammar);
        ll = LS.DoIterations(UnityEngine.Random.Range(iterationMin,iterationMax));
        DoTurtle(0);


	}

    public void DoTurtle(float breath)
    {
        Random.InitState(seed);

        instructionSet.RenderSymbols(ll);
        MeshBuilder mg = instructionSet.GetMesh();
        GetComponent<MeshFilter>().mesh = mg.Generate();
    }
}


/*

                this.grammar["+"] = () => Rotate(0, 0, angle);
            this.grammar["-"] = () => Rotate(0, 0, -angle);
            this.grammar["|"] = () => Rotate(0, 0, 180);
            this.grammar["\\"] = () => Rotate(angle, 0, 0);
            this.grammar["/"] = () => Rotate(-angle, 0, 0);
            this.grammar["^"] = () => Rotate(0, angle, 0);
            this.grammar["&"] = () => Rotate(0, -angle, 0);

 */
