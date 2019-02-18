using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

    public float speed = 1;

    private void FixedUpdate() {
        transform.position += transform.up * Time.fixedDeltaTime * speed;
    }
}
