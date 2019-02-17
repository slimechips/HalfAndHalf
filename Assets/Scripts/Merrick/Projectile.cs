using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] public bool isPlayerProjectile = false;
    [SerializeField] private bool destroyOnContact = true;
    [SerializeField] public float damage = 10;
    [SerializeField] public float damagePerSecond = 0;

    public void OnTriggerEnter2D(Collider2D collider) {
        ICollidesWithProjectiles target = collider.GetComponent<ICollidesWithProjectiles>();
        if (target != null) {
            if (target.ReceiveProjectile(this)) {
                if (destroyOnContact) Destroy(gameObject);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collider) {
        ICollidesWithProjectiles target = collider.GetComponent<ICollidesWithProjectiles>();
        if (target != null) {
            target.ReceiveProjectile(this);
        }
    }

    public void BulletMove(float shotSpd)
    {
        this.transform.position += transform.up * Time.deltaTime * shotSpd;
    }
}
