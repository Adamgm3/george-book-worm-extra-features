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

    public static int collectibleCount = 0;
    public int collectibleGoal = 5;

    void OnGUI()
    {
        GUI.depth = 1;

        // Black background for letterbox
        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);

        float targetAspect = use43Ratio ? 4f / 3f : (float)renderWidth / renderHeight;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        Rect renderRect;

        if (scaleHeight < 1.0f)
        {
            int newHeight = (int)(Screen.height * scaleHeight);
            int yOffset = (Screen.height - newHeight) / 2;
            renderRect = new Rect(0, yOffset, Screen.width, newHeight);
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            int newWidth = (int)(Screen.width * scaleWidth);
            int xOffset = (Screen.width - newWidth) / 2;
            renderRect = new Rect(xOffset, 0, newWidth, Screen.height);
        }

        GUI.color = Color.white;
        GUI.DrawTexture(renderRect, renderTexture);

        // Draw collectible counter on top inside the game area
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(renderRect.x + 20, renderRect.y + 20, 200, 50), collectibleCount + "/" + collectibleGoal, style);
    }

    void OnDestroy()
    {
        if (renderTexture != null)
            renderTexture.Release();
    }
}