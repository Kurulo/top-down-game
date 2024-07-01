using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField, Header("Pickable")]
    private Item _item;

    PlayerStateMachine _player;   
    private bool _collised = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerStateMachine>() && !_collised)
        {
            if (_player == null) _player = other.gameObject.GetComponent<PlayerStateMachine>();

            if (_player.Interaction)
            {
                _collised = true;
                InventoryManager inventoryManager = _player.GetComponent<InventoryManager>();
                bool result = inventoryManager.AddItemToInventory(_item);

                if (result)
                {
                    Destroy(gameObject);
                }
            }
        }   
    }
}
