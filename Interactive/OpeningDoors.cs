using UnityEngine;

public class OpeningDoors : MonoBehaviour
{
    private float time = 0f;

    [SerializeField]
    private KeyType keyNeeded;

    PlayerStateMachine _playerState;
    InventoryManager _inventory;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerStateMachine>())
        {
            if (_playerState == null) { _playerState = other.gameObject.GetComponent<PlayerStateMachine>(); }
            if (_inventory == null) { _inventory = other.gameObject.GetComponent<InventoryManager>(); }

            if (_playerState.Interaction)
            {
                if (_inventory.IsItemInInventory(PotionType.None, keyNeeded))
                {
                    _inventory.UseKey(keyNeeded);

                    Animator animator = GetComponent<Animator>();
                    animator.SetTrigger("Open");

                    return;
                }
            }
        }
    }
}
