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


    private bool randomizing = false;
    private bool triggered = false;
    private bool triggering = false;
    private float randomizeTime = 0.8f;
    private float totalTime = 1.2f;

// Use this for initialization
void Start () {
        generator.CreateTerrainChunk();
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

        for (float t = 0; t < totalTime || triggered != true; t += Time.unscaledDeltaTime * 1.2f)
        {
            print(t);
            camera.fieldOfView = fov.Evaluate(t);

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
        camera.fieldOfView = fov.Evaluate(totalTime);
        randomizing = false;
    }
}
