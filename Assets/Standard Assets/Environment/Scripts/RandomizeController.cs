using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class RandomizeController : MonoBehaviour {


    [SerializeField]
    private TerrainChunkGenerator generator;
    [SerializeField]
    private ShaderInit shaderInit;
    [SerializeField]
    private CharacterPlacer characterPlacer;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private AnimationCurve fov;
    [SerializeField]
    private AnimationCurve timeScale;
    [SerializeField]
    private Vingette vingette;
    [SerializeField]
    private AnimationCurve minRadius;
    [SerializeField]
    private AnimationCurve maxRadius;
    [SerializeField]
    private AnimationCurve saturation;
    [SerializeField]
    private AnimationCurve minRadiusBegin;
    [SerializeField]
    private AnimationCurve maxRadiusBegin;
    [SerializeField]
    private AnimationCurve saturationBegin;


    private bool randomizing = false;
    private bool triggered = false;
    private bool triggering = false;
    private float randomizeTime = 0.8f;
    private float totalRandomizeTime = 1.2f;
    private float totalBeginTime = 6.0f;

// Use this for initialization
void Start () {
        generator.CreateTerrainChunk();
        StartCoroutine(Begin());
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Generate") && !randomizing)
        {
            StartCoroutine(Randomize());
        }
    }

    void RegenerateTerrain()
    {
        generator.RemoveTerrainChunk();
        generator.CreateTerrainChunk();
        shaderInit.Generate();
        characterPlacer.Place();
    }

    IEnumerator Randomize()
    {
        randomizing = true;
        triggering = false;
        triggered = false;

        for (float t = 0; t < totalRandomizeTime || triggered != true; t += Time.unscaledDeltaTime * 1.2f)
        {
            camera.fieldOfView = fov.Evaluate(t);
            Time.timeScale = timeScale.Evaluate(t);
            vingette.minRadius = minRadius.Evaluate(t);
            vingette.maxRadius = maxRadius.Evaluate(t);
            vingette.saturation = saturation.Evaluate(t);

            if (triggering)
            {
                t -= Time.unscaledDeltaTime;
                triggering = false;
                triggered = true;
            }

            if (t > randomizeTime && !triggered)
            {
                RegenerateTerrain();
                triggering = true;
            }

            yield return null;
        }
        camera.fieldOfView = fov.Evaluate(totalRandomizeTime);
        Time.timeScale = timeScale.Evaluate(totalRandomizeTime);
        vingette.minRadius = minRadius.Evaluate(totalRandomizeTime);
        vingette.maxRadius = maxRadius.Evaluate(totalRandomizeTime);
        vingette.saturation = saturation.Evaluate(totalRandomizeTime);

        randomizing = false;
    }

    IEnumerator Begin()
    {

        for (float t = 0; t < totalBeginTime && !randomizing; t += Time.unscaledDeltaTime * 1.2f)
        {
            vingette.minRadius = minRadiusBegin.Evaluate(t);
            vingette.maxRadius = maxRadiusBegin.Evaluate(t);
            vingette.saturation = saturationBegin.Evaluate(t);

            yield return null;
        }
        vingette.minRadius = minRadiusBegin.Evaluate(totalBeginTime);
        vingette.maxRadius = maxRadiusBegin.Evaluate(totalBeginTime);
        vingette.saturation = saturationBegin.Evaluate(totalBeginTime);
    }
}
