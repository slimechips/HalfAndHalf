using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

    public float speed = 1;
    private float duration = 5f;

    public void Start()
    {
        GameObject sound = SoundManager.PlayerShoot();
        Destroy(sound, 3f);
    }

    private void FixedUpdate() {
        duration -= Time.deltaTime;
        if (duration < 0) Destroy(gameObject);
        transform.position += transform.up * Time.fixedDeltaTime * speed;
    }
}
