using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : EnemyShip
{
    private ParticleSystem ps;
    private bool charged = false;
    private bool speedUp = false;

    private float missedRange = 20f;
    private float explodeRange = 1f;
    private float chargeSpd = 7f;

    private Vector3 oldPos;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        reward = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            if(!speedUp) BasicRotate();

            if (Vector2.Distance(player.transform.position, transform.position) > shootRange && !ps.isPlaying && !charged)
            {
                BasicMovement();
            }

            if (Vector2.Distance(player.transform.position, transform.position) <= shootRange)
            {
                // initiate charging explosion
                if(!ps.isPlaying && !charged)
                {
                    ps.Play();
                    charged = true;
                    oldPos = transform.position;
                }
            }

            if (!ps.isPlaying && charged)
            {
                speedUp = true;
                // charge towards target
                transform.position += transform.up * Time.deltaTime * chargeSpd;

                if (Vector2.Distance(player.transform.position, transform.position) < explodeRange)
                {
                    OnDeath();
                    // explode
                    player.GetComponent<Ship>().Damage(10);
                    player.GetComponent<PlayerShip>().DamageFlash();
                }

                if (Vector2.Distance(oldPos, transform.position) > missedRange)
                {
                    // missed the player
                    charged = false;
                    speedUp = false;
                }
            }
        }
    }
}
