
using ModestTree;
using System.Collections;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _destroyable;
    [SerializeField] private GameObject[] _items;
    
    public void Detected()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        SpawnDestroyed();
        SpawnItems();

        this.gameObject.SetActive(false);
    }

    private void SpawnDestroyed()
    {
        Instantiate(_destroyable, this.transform.position, _destroyable.transform.rotation, this.transform.parent);
    }

    private void SpawnItems()
    {
        if (_items.Length > 0) 
        { 
            foreach (var item in _items)
            {
                if (item != null)
                    Instantiate(item, this.transform.position, item.transform.rotation, this.transform.parent);
            }
        }
    } 
}
