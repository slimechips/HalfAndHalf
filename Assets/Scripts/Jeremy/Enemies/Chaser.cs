using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : EnemyShip
{
    private void Update()
    {
        if (player != null)
        {
            BasicMovement();

            // if within a certain distance, fire a bullet
            if (Vector2.Distance(player.transform.position, transform.position) < shootRange && canShoot)
            {
                Shoot();
            }
        }
    }
}
