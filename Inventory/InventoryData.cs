using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryData
{
    private List<Item> _items = new List<Item>();

    public List<Item> GetAllItems()
    {
        if (_items != null)
        {
            return _items;
        }

        else return null;
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public List<Potion> GetAllPotions()
    {
        List<Potion> potions = new List<Potion>();
        foreach (Potion potion in _items) { potions.Add(potion);}
        return potions;
    }
}
