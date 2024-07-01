using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "Healt Potion", menuName = "Item/Potion/Health Potion", order = 0)]

public class HealthPotion : Potion
{
    public float HealingAmount = 20;

    public override void Use()
    {

    }
}
