using UnityEngine;
using TMPro;

public class CollectibleUI : MonoBehaviour
{
    public static CollectibleUI Instance;

    public TextMeshProUGUI collectibleText;
    public int goal = 5;

    private int count = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddCollectible()
    {
        count++;
        collectibleText.text = count + "/" + goal;
    }
}