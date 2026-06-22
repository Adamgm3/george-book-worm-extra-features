using System.Runtime.Serialization;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public int collected;

    public int Add(int num) => collected += num;
}
