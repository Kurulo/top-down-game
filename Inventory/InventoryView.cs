using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class InventoryView : MonoBehaviour
{
    [SerializeField]
    private InventorySlot[] m_inventorySlots;

    private InventoryManager m_inventoryManager;

    private List<Item> m_items = new List<Item>();

    [Inject]
    private void Construct(PlayerStateMachine player)
    {
        m_inventoryManager = player.GetComponent<InventoryManager>();
    }

    private void OnEnable()
    {
        m_inventoryManager.AddItemEvent += ShowInventory;
        m_inventoryManager.RemoveItemEvent += RemoveItemFromSlot;
    }

    private void ShowInventory()
    {
        m_items = m_inventoryManager.GetAllItems();

        foreach (InventorySlot slot in m_inventorySlots)
        {
            if(slot.isHaveItem)
            {
                if (slot.HasTheSameItemInDictionary(m_items.Last()))
                {
                    slot.AddToExistingItem(m_items.Last());
                    return;
                }

                continue;
            }
            else
            {
                foreach (InventorySlot searchingSlot  in m_inventorySlots)
                {
                    if (searchingSlot.HasTheSameItemInDictionary(m_items.Last()))
                    {
                        searchingSlot.AddToExistingItem(m_items.Last());
                        return;
                    }
                }

                slot.AddNewItem(m_items.Last());
                return;
            }
        }
    }

    private void RemoveItemFromSlot() 
    {
        foreach(InventorySlot slot in m_inventorySlots) {
            if (slot.isHaveItem) {
                if (slot.RemoveItemFromSlot(m_inventoryManager.LastRemovedItem))
                    return;
            }            
        }
    }

    private void OnDisable()
    {
        m_inventoryManager.AddItemEvent -= ShowInventory;
        m_inventoryManager.RemoveItemEvent -= RemoveItemFromSlot;
    }
}
