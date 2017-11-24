using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coloring;

namespace Terrain
{
    public class ShaderInit : MonoBehaviour
    {

        public Material paletteMaterial;
        public Material seaMaterial;
        public Material skyMaterial;
        public Material groundMaterial;
        public Material hillsMaterial;

        // Use this for initialization
        void Start()
        {
            InitSea();
            InitSky();
            InitGround();
            InitPalette();

        }

        void InitSea()
        {
            float mid = Random.Range(0.4f, 0.7f); // 0.3f
            float step = Random.Range(0, 0.2f);
            float top = mid + step;
            float bottom = mid - step;

            float sampleSpace = Random.Range(0, 0.1f);

            seaMaterial.SetColor("_Color1", ColorPalette.Sample(0) * bottom); // 0.3f
            seaMaterial.SetColor("_Color2", ColorPalette.Sample(sampleSpace) * mid); // 0.6f
            seaMaterial.SetColor("_Color3", ColorPalette.Sample(sampleSpace * 2) * top); // 1.0f
            float brightness = Random.Range(0.5f, 1.0f);
            Color seaHighlight = brightness * Color.white + (1 - brightness) * ColorPalette.Sample(0.1f);
            seaMaterial.SetColor("_Color4", seaHighlight);

            seaMaterial.SetInt("_Steps", Random.Range(1, 4));
            seaMaterial.SetFloat("_Step1", Random.Range(0.3f, 0.5f));
            seaMaterial.SetFloat("_Step2", Random.Range(0.5f, 0.7f));
            seaMaterial.SetFloat("_Step3", Random.Range(0.7f, 0.95f));

        }

        void InitSky()
        {
            skyMaterial.SetColor("_Color1", ColorPalette.Sample(0.3f));
            skyMaterial.SetColor("_Color2", ColorPalette.Sample(0.35f));
            skyMaterial.SetColor("_Color3", ColorPalette.Sample(0.4f));
        }

        void InitGround()
        {
            groundMaterial.SetColor("_Color", ColorPalette.Sample(0.7f) * 0.8f);
            hillsMaterial.SetColor("_Color", ColorPalette.Sample(0.8f) * 0.8f);
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
        public void Generate()
        {
            ColorPalette.Randomize();
            Start();
        }
    }

}
