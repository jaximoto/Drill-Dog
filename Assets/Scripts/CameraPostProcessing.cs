using UnityEngine;

public class CameraPostProcessing : MonoBehaviour
{

    public Material postProcessMaterial;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, postProcessMaterial);
    }
}
