using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName ="Key", menuName ="Item/Keys/Key", order = 1)]
public class Key : Item
{
    [Header("Settings")]
    public KeyType Type;
}
