using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : EnemyShip
{
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            BasicRotate();
            if (Vector2.Distance(player.transform.position, transform.position) > shootRange)
            {
                BasicMovement();
            }

            if (Vector2.Distance(player.transform.position, transform.position) <= shootRange)
            {
                // initiate charging explosion
            }
        }
    }
}
