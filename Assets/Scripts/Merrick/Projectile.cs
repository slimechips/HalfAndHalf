using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    [SerializeField] public bool isPlayerProjectile = false;
    [SerializeField] private bool destroyOnContact = true;
    [SerializeField] public float damage = 10;
    [SerializeField] public float damagePerSecond = 0;



    public void OnCollisionEnter2D(Collision2D collision) {
        ICollidesWithProjectiles target = collision.collider.GetComponent<ICollidesWithProjectiles>();
        if (target != null) {
            if (target.ReceiveProjectile(this)) {
                if (destroyOnContact) Destroy(this);
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision) {
        ICollidesWithProjectiles target = collision.collider.GetComponent<ICollidesWithProjectiles>();
        if (target != null) {
            target.ReceiveProjectile(this);
        }
    }
}
