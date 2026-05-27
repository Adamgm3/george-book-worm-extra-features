using UnityEngine;

public class PS1CameraEffect : MonoBehaviour
{
    [Header("PS1 Settings")]
    public int renderWidth = 320;
    public int renderHeight = 240;
    public bool use43Ratio = true;

    public RenderTexture renderTexture;
    private Camera cam;

    public static int collectibleCount = 0;
    public int collectibleGoal = 5;
    void Start()
    {
        cam = GetComponent<Camera>();

        renderTexture.filterMode = FilterMode.Point;

        cam.targetTexture = renderTexture;
    }
    void OnDestroy()
    {
        if (renderTexture != null)
            renderTexture.Release();
    }

    //void OnGUI()
    //{
    //    GUI.depth = -1;
    //    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), renderTexture, ScaleMode.StretchToFill);
    //}
}