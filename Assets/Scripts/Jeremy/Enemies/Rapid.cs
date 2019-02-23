using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapid : EnemyShip
{
    private void Start()
    {
        reward = 25;
    }
    // Update is called once per fram
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
