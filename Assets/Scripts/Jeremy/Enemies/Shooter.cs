using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyShip
{
    private void Start()
    {
        reward = 15;
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            BasicRotate();
            if (Vector2.Distance(player.transform.position, transform.position) > moveRange)
            {
                BasicMovement();
            }

            // if within a certain distance, fire a bullet
            if (Vector2.Distance(player.transform.position, transform.position) < shootRange && canShoot)
            {
                Shoot();
            }
        }
    }
}
