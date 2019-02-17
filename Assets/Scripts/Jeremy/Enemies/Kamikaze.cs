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
            BasicMovement();
        }

    }
}
