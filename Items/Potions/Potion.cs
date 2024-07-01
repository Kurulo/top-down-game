using System.Collections;
using UnityEngine;


public class Potion : Item, IUsable
{
    [Header("Settings")]
    [SerializeField] 
    protected PotionType type;
    public PotionType Type { get { return type; } }

    public virtual void Use()
    {
        
    }

    public virtual void Used()
    {
        
    }

    public virtual void Using()
    {
        
    }
}
