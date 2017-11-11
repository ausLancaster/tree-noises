using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coloring;

public class ShaderInit : MonoBehaviour {

    public Material paletteMaterial;

    public Shader paletteShader;

	// Use this for initialization
	void Start () {

        paletteMaterial.SetColor("_Color00", ColorPalette.Sample(0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
