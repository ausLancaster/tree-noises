using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Vingette : MonoBehaviour
{
    [SerializeField]
    private Shader _shader;
    [Range(0, 1)]
    public float minRadius;
    [Range(0, 1)]
    public float maxRadius;
    [Range(0, 1)]
    public float saturation;

    Material _material;

    void OnEnable()
    {
        _material = new Material(_shader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        _material.SetFloat("_MinRadius", minRadius);
        _material.SetFloat("_MaxRadius", maxRadius);
        _material.SetFloat("_Saturation", saturation);

        Graphics.Blit(src, dst, _material, 0);
    }
}
