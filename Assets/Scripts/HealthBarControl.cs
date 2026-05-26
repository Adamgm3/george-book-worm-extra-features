using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarControl : MonoBehaviour
{

    private int temp;
    [SerializeField] PlayerHealth player;
    public List<Image> healthSquares = new List<Image>();
    [SerializeField] Image healthPrefab;

    void Start()
    {
        temp = player.health;

        for (int i = 0; i < player.health; i++)
        {
            Image newSquare = Instantiate(healthPrefab, transform);
            healthSquares.Add(newSquare);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health < temp)
        {
            Destroy(healthSquares[healthSquares.Count - 1].gameObject);

            healthSquares.RemoveAt(healthSquares.Count - 1);

            temp = player.health;
        }
    }

}
