using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderTest : MonoBehaviour
{
    // Start is called before the first frame update
    public ComputeShader computeShader;
    public RenderTexture renderTexture;
    void Start()
    {
    }

    // Update is called once per frame
    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (renderTexture == null) {
            renderTexture = new RenderTexture(256, 256, 24);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }


        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);

        Graphics.Blit(renderTexture, destination);
    }
}
