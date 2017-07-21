using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class RockPlacer : MonoBehaviour {

    public GameObject rock;

    private float groundWidth = 200.0f;
    private float spacing = 1.0f;
    private float gap = 0.2f;

    // Use this for initialization
    void Start()
    {
        /*print("placing rocks");
        for (float i = -groundWidth / 2; i < groundWidth / 2; i += spacing)
        {
            for (float j = -groundWidth / 2; j < groundWidth / 2; j += spacing)
            {
                float height = HeightMap.SeussLike(i, j, 1 / 40.0f, 40.0f, 2.0f, 1 / 4.0f);
                if (height > 0.2f - gap && height < 0.2f + gap)
                {
                    Instantiate(rock, new Vector3(i, 0.2f, j), Quaternion.identity, transform);
                }
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
