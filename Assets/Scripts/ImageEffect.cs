using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEffect : MonoBehaviour
{
    [SerializeField]
    Shader pixelate;

    protected Material mat;

    void Start()
    {
        mat = new Material(pixelate);
    }
}