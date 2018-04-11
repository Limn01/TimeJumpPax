using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pixelate : ImageEffect
{
    [SerializeField, Range(0, 2048)]
    int pixelDensity;

    void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        float aspect = Camera.main.aspect;
        Vector2 count = new Vector2(pixelDensity, pixelDensity / aspect);
        Vector2 size = new Vector2(1 / count.x, 1 / count.y);

        mat.SetVector("Density", count);
        mat.SetVector("Size", size);
        Graphics.Blit(_source, _destination, mat);
    }
}