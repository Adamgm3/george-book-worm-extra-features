using System;
using UnityEngine;

public class PickUpCollectable : MonoBehaviour
{
    [SerializeField] CollectableManager manager;
    [SerializeField] private string collectibleID;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(collectibleID))
        {
            collectibleID = Guid.NewGuid().ToString();
        }
    }
    private void Start()
    {

        if (SaveSystem.Instance.saveData.collectedIDs.Contains(collectibleID))
        {
            Destroy(gameObject);
        }
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CollectableManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        Collect();
    }
    private void Collect()
    {
        manager.Add(1);
        if (!SaveSystem.Instance.saveData.collectedIDs.Contains(collectibleID))
        {
            SaveSystem.Instance.saveData.collectedIDs.Add(collectibleID);
        }
        SaveSystem.Instance.SaveGame();
        manager.Add(1);
        Destroy(gameObject);
    }
    [ContextMenu("Generate New GUID")]
    private void GenerateNewGuid()
    {
        collectibleID = Guid.NewGuid().ToString();
    }
}
