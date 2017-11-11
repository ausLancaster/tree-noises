using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coloring;

public class ShaderInit : MonoBehaviour {

    public Material paletteMaterial;
    public Material seaMaterial;
    public Material skyMaterial;
    public Material groundMaterial;
    public Material hillsMaterial;

	// Use this for initialization
	void Start () {
        InitSea();
        InitSky();
        InitGround();
        InitPalette();

    }

    void InitSea()
    {
        seaMaterial.SetColor("_Color1", ColorPalette.Sample(0) * 0.3f);
        seaMaterial.SetColor("_Color2", ColorPalette.Sample(0.05f) * 0.6f);
        seaMaterial.SetColor("_Color3", ColorPalette.Sample(0.1f) * 1.0f);
        seaMaterial.SetColor("_Color4", new Color(1.0f, 1.0f, 1.0f));

    }

    void InitSky()
    {
        skyMaterial.SetColor("_Color1", ColorPalette.Sample(0.3f));
        skyMaterial.SetColor("_Color2", ColorPalette.Sample(0.35f));
        skyMaterial.SetColor("_Color3", ColorPalette.Sample(0.4f));
    }

    void InitGround()
    {
        groundMaterial.SetColor("_Color", ColorPalette.Sample(0.7f));
        hillsMaterial.SetColor("_Color", ColorPalette.Sample(0.8f));
    }

    void InitPalette()
    {
        paletteMaterial.SetColor("_Color00", ColorPalette.Sample(0));
        paletteMaterial.SetColor("_Color01", ColorPalette.Sample(0.05f));
        paletteMaterial.SetColor("_Color02", ColorPalette.Sample(0.1f));
        paletteMaterial.SetColor("_Color03", ColorPalette.Sample(0.15f));
        paletteMaterial.SetColor("_Color04", ColorPalette.Sample(0.2f));
        paletteMaterial.SetColor("_Color05", ColorPalette.Sample(0.25f));
        paletteMaterial.SetColor("_Color06", ColorPalette.Sample(0.3f));
        paletteMaterial.SetColor("_Color07", ColorPalette.Sample(0.35f));
        paletteMaterial.SetColor("_Color08", ColorPalette.Sample(0.4f));
        paletteMaterial.SetColor("_Color09", ColorPalette.Sample(0.45f));
        paletteMaterial.SetColor("_Color10", ColorPalette.Sample(0.5f));
        paletteMaterial.SetColor("_Color11", ColorPalette.Sample(0.55f));
        paletteMaterial.SetColor("_Color12", ColorPalette.Sample(0.6f));
        paletteMaterial.SetColor("_Color13", ColorPalette.Sample(0.65f));
        paletteMaterial.SetColor("_Color14", ColorPalette.Sample(0.7f));
        paletteMaterial.SetColor("_Color15", ColorPalette.Sample(0.75f));
        paletteMaterial.SetColor("_Color16", ColorPalette.Sample(0.8f));
        paletteMaterial.SetColor("_Color17", ColorPalette.Sample(0.85f));
        paletteMaterial.SetColor("_Color18", ColorPalette.Sample(0.9f));
        paletteMaterial.SetColor("_Color19", ColorPalette.Sample(0.95f));
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Generate"))
        {
            ColorPalette.Randomize();
            Start();
        }
    }
}
