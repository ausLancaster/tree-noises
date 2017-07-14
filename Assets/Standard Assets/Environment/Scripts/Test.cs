using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSys;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        LSystem LS = new LSystem();
        LinkedListA<string> ll = LS.DoIterations(4);
        print(LSystem.LLToString(ll));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
