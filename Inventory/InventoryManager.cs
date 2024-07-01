using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> _inventory = new List<Item>();

    private InventoryData _inventoryData;

    private Item _lastItem;
    private Dictionary<PotionType, int> _potionSafe = new Dictionary<PotionType, int>();

    public Item LastRemovedItem;

    public event Action AddItemEvent;
    public event Action RemoveItemEvent;

    private void Start()
    {
        _inventoryData = new InventoryData();

        _inventory = _inventoryData.GetAllItems();
    }

    public bool AddItemToInventory(Item item)
    {
        _inventoryData.AddItem(item);

        ReloadInventory();
        SetToDictionary();

        AddItemEvent?.Invoke();

        return true;
    }

    public List<Item> GetAllItems() 
    {
        return _inventory; 
    }

    public void UsePotion(PotionType type)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            var item = _inventory[i];

            if (item is Potion potion)
                if (potion.Type.Equals(type))
                {
                    potion.Use();
                    _inventory.Remove(potion);
                    LastRemovedItem = potion;

                    RemoveItemEvent?.Invoke();
                    return;
                }
        }
    }

    public void UseKey(KeyType keyType)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            var item = _inventory[i];

            if (item is Key key) 
                if (key.Type.Equals(keyType))
                {
                    _inventory.Remove(key);
                    LastRemovedItem = key;

                    RemoveItemEvent?.Invoke();
                    return;
                }
        }
    }

    public bool IsItemInInventory(PotionType wantedPotion = PotionType.None, KeyType wantedKey = KeyType.None)
    {
        if (wantedPotion != PotionType.None)
        {       
            foreach (Item item in _inventory)
            {
                if (item is Potion potion)
                    if (potion.Type == wantedPotion)
                    {
                        return true;
                    }
            }
        }

        if (wantedKey != KeyType.None)
        {
            foreach (Item item in _inventory)
            {
                if (item is Key key)
                    if (key.Type == wantedKey)
                    {   
                        return true;
                    }
            }
        }

        return false;
    }

    private void ReloadInventory() 
    {
        _inventory = _inventoryData.GetAllItems();
        _lastItem = _inventory.Last();
    }

    private void SetToDictionary()
    {
        if (_lastItem is Potion potion)
            if (!_potionSafe.ContainsKey(potion.Type))
                _potionSafe.Add(potion.Type, 1);
            else
                _potionSafe[potion.Type] += 1;
    }
}
