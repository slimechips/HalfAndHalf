using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

    public float speed = 1;
    private float duration = 5f;

    private void FixedUpdate() {
        duration -= Time.deltaTime;
        if (duration < 0) Destroy(gameObject);
        transform.position += transform.up * Time.fixedDeltaTime * speed;
    }
}
