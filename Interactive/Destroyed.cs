using System.Collections;
using UnityEngine;

public class Destroyed : MonoBehaviour
{
    [SerializeField] private float _gravityPower = 10f;

    private Rigidbody[] _rigidbodies;

    private void OnEnable()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        
        foreach(var rigidbody in _rigidbodies)
        {
            rigidbody.AddExplosionForce(100f, this.transform.position, 100f);
        }

        StartCoroutine(EndDestroy());
    }

    private void Update()
    {
        

        foreach(var rigidbody in _rigidbodies)
        {
            rigidbody.velocity = Vector3.up * Physics.gravity.y * (_gravityPower * 100f) * Time.deltaTime;
        }
    }

    private IEnumerator EndDestroy()
    {
        yield return new WaitForSeconds(5.0f);

        Destroy(this.gameObject);
    }
}
