using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private int _bulletCount;

    [SerializeField]
    private Transform _gunTransform;
    [SerializeField]
    private Transform _bodyTransform;

    public Transform BodyTransform { get { return _bodyTransform; } }

    [SerializeField]
    private List<Bullet> _bullets;

    private void Start() {
        CreatePool(_bulletCount);
    }

    private void CreatePool(int count) {
        for (int i = 0; i < count; i++) {
            Bullet bullet = CreateAndInitializingBullet();
            bullet.gameObject.SetActive(false);

            _bullets.Add(bullet);        
        }
    }

    public Bullet GetLastBullet() {
        if (_bullets.Count != 0)
            return _bullets[_bullets.Count - 1];
        else return null;
    }

    public void RemoveFromPool(Bullet bullet) {
        _bullets.RemoveAt(_bullets.Count - 1);
    } 

    public void ReturnBullet(Bullet bullet) {
        _bullets.Add(bullet);
    }

    private Bullet CreateAndInitializingBullet() {
        Bullet bullet =
                Instantiate(
                    _bulletPref,
                    _gunTransform.position,
                    Quaternion.identity).
                GetComponent<Bullet>();

        bullet.Init(_gunTransform, this);

        return bullet;
    }
}
