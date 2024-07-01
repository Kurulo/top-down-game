using UnityEngine;

public class Shooting {
    private BulletPool _pool;
    private Transform _targetTrnsf;

    public Shooting(BulletPool pool, Transform targetTrnsf) {
        _pool = pool;
        _targetTrnsf = targetTrnsf;
    }

    public void Shoot() {
        Bullet bullet = _pool.GetLastBullet();

        if (bullet != null) {
            Debug.Log("Shooot");

            bullet.SetDestination(_targetTrnsf.position);

            bullet.gameObject.SetActive(true);
            _pool.RemoveFromPool(bullet);
        }
    }
}
