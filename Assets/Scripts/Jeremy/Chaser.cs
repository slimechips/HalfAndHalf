using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : EnemyShip
{
    private void Update()
    {
        Debug.Log(rotSpd);

        if (player != null)
        {
            Vector3 targetDir = player.transform.position - transform.position;
            float step = rotSpd * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
