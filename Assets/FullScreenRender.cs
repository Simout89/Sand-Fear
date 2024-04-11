using UnityEngine;

[ExecuteInEditMode]
public class FullScreenRenderer : MonoBehaviour
{
    [SerializeField] private Material materialToUse;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, materialToUse);
    }
}