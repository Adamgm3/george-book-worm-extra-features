using UnityEngine;

public class PS1CameraEffect : MonoBehaviour
{
    [Header("PS1 Settings")]
    public int renderWidth = 320;
    public int renderHeight = 240;
    public bool use43Ratio = true;

    private RenderTexture renderTexture;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        renderTexture = new RenderTexture(renderWidth, renderHeight, 16);
        renderTexture.filterMode = FilterMode.Point; // No smoothing - keeps pixels chunky
        cam.targetTexture = renderTexture;
    }

    void OnGUI()
    {
        GUI.depth = -1;

        // Black background for letterbox
        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);

        // Calculate 4:3 area
        float targetAspect = use43Ratio ? 4f / 3f : (float)renderWidth / renderHeight;
        float windowAspect = (float)Screen.width / Screen.height;

        float scaleHeight = windowAspect / targetAspect;
        Rect renderRect;

        if (scaleHeight < 1.0f)
        {
            // Letterbox top and bottom
            int newHeight = (int)(Screen.height * scaleHeight);
            int yOffset = (Screen.height - newHeight) / 2;
            renderRect = new Rect(0, yOffset, Screen.width, newHeight);
        }
        else
        {
            // Pillarbox left and right
            float scaleWidth = 1.0f / scaleHeight;
            int newWidth = (int)(Screen.width * scaleWidth);
            int xOffset = (Screen.width - newWidth) / 2;
            renderRect = new Rect(xOffset, 0, newWidth, Screen.height);
        }

        GUI.color = Color.white;
        GUI.DrawTexture(renderRect, renderTexture);
    }

    void OnDestroy()
    {
        if (renderTexture != null)
            renderTexture.Release();
    }
}