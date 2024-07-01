using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventorySlot
{
    [SerializeField]
    private Image _itemIcon;

    [SerializeField]
    private TMP_Text _numOfItemTMP;

    public int index;

    public int objectCount = 0;

    private Dictionary<PotionType, int> _potions = new Dictionary<PotionType, int>();
    private Item _currentItem;

    public bool isHaveItem = false;

    public void AddToExistingItem(Item item)
    {
        Debug.Log("Slot id: " + index.ToString());
        if (item is Potion potion)
        {
            Debug.Log(potion.Type);
            if (_potions.ContainsKey(potion.Type))
            {

                _currentItem = potion;
                _potions[potion.Type] += 1;

                SetSlotVisualize();
            }
        }     
    }

    public void AddNewItem(Item item)
    {
        if (item is Potion potion)
        {
           AddPotion(potion);
        }
        else
        {
            AddItem(item);
        }
    }

    public bool RemoveItemFromSlot(Item item)
    {
        if (item is Potion potion) {
            if (RemovePotion(potion)) {
                return true;
            }
        }
        else {
            if (RemoveItem(item)){
                return true;
            }
        }

        return false;
    }



    public bool HasTheSameItemInDictionary(Item item)
    {
        if (item is Potion potion)
        {
            if (_potions.ContainsKey(potion.Type))
                return true;
        }

        return false;
    }
    
    public bool HaveThisItem(Item item)
    {
        if (item is Key key)
        {
            if (_currentItem is Key currentKey)
            {
                if (currentKey.Type == key.Type)
                    return true;
            }

        }
        return false;
    }

    private bool RemoveItem(Item item)
    {
        if (item is Key key)
        {
            if (_currentItem is Key currentKey)
            {
                if (currentKey.Type == key.Type)
                {
                    _currentItem = null;

                    ResetSlot();
                    return true;
                }
            }
        }

        return false;
    }
    private void AddItem(Item item)
    {
        OnSlotVisulisation();

        _currentItem = item;
        isHaveItem = true;

        SetSlotVisualize();
    }

    private void AddPotion(Potion potion)
    {
        OnSlotVisulisation();

        _potions.Add(potion.Type, 1);
        _currentItem = potion;
        isHaveItem = true;

        SetSlotVisualize();
    }

    private bool RemovePotion(Potion potion)
    {
        if (HasTheSameItemInDictionary(potion))
        {
            int itemCount = _potions[potion.Type] - 1;

            if (itemCount != 0)
            {
                _potions[potion.Type] -= 1;
                SetSlotVisualize();
            }
            else if (itemCount == 0)
            {
                _potions[potion.Type] -= 1;
                _potions = new Dictionary<PotionType, int>();
                _currentItem = null;

                ResetSlot();
            }

            return true;
        }

        return false;
    }

    private void SetSlotVisualize()
    {
        if (_currentItem is Potion potion)
        {
            _itemIcon.sprite = _currentItem.Sprite;
            _numOfItemTMP.text = _potions[potion.Type].ToString();
            objectCount = _potions[potion.Type];
        }

        if (_currentItem is Key key)
        {
            _itemIcon.sprite = _currentItem.Sprite;
            _numOfItemTMP.text = "0";
        }
    } 

    private void ResetSlot()
    {
        isHaveItem = false;
        _itemIcon.enabled = false;
        _numOfItemTMP.text = "0";
    }

    private void OnSlotVisulisation()
    {
        _itemIcon.enabled = true;
    }
}
