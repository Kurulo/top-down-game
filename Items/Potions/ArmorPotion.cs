using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor Potion", menuName = "Item/Potion/Armor Potion", order = 1)]
public class ArmorPotion : Potion
{
    public override void Use()
    {
        Debug.Log("Used: Armor");
    }
}
