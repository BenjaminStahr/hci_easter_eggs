using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShaderInterfaceScript : MonoBehaviour
{
    public Material matBlur;
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {

        Graphics.Blit(src, dest, matBlur);

    }
}
