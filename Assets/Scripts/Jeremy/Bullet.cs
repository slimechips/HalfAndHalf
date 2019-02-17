using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    private float shotSpd = 5f;

    private float destroyTime = 10f;

    public void Initialise(float shotSpd)
    {
        this.shotSpd = shotSpd;

        // destroys the bullet if it doesnt hit the player within destroyTime
        Invoke("BulletDecay", destroyTime);
    }

    // Update is called once per frame
    private void Update()
    {
        BulletMove(shotSpd);
    }

    private void BulletDecay()
    {
        Destroy(gameObject);
    }
}
