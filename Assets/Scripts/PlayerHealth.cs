using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int health = 5;
    public int damage = 1;
    [SerializeField] EnemyMovement enemy;
    public string scene = "2";
    public WormController controller;

    private void Start()
    {
        controller = GetComponentInParent<WormController>();
    }

    public void Update()
    {
        Debug.Log("Player health: " + health);
        if (health <= 0)
        {
            Debug.Log("Player died! Restarting scene...");
            SceneManager.LoadScene(scene);
        }
    }

}
