using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int health = 5;
    public int damage = 1;
    [SerializeField] EnemyMovement enemy;
    public string scene = "2";


    public void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Player died! Restarting scene...");
            SceneManager.LoadScene(scene);
        }
    }

}
