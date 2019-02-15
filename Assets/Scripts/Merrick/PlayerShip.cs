using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{

    public static PlayerShip playerShip = null;

    // reference to sprites
    public Sprite[] playerSprites;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (playerShip != null) Destroy(playerShip);
        playerShip = this;
    }

    private void Update()
    {
        // alternate between the different images for thrusters
        if (Input.GetAxis("p1 a") > 0 && Input.GetAxis("p2 a") == 0)
        {
            sprite.sprite = playerSprites[2];
        }
        else if (Input.GetAxis("p1 a") == 0 && Input.GetAxis("p2 a") > 0)
        {
            sprite.sprite = playerSprites[1];
        }
                else if (Input.GetAxis("p1 a") > 0 && Input.GetAxis("p2 a") > 0)
        {
            sprite.sprite = playerSprites[3];
        }
        else
        {
            sprite.sprite = playerSprites[0];
        }

    }

    public override void OnDeath()
    {
        // spawn explosions
        // trigger defeat screen

        //placeholder
        if (sprite != null) sprite.enabled = false;
    }

    public override bool ReceiveProjectile(Projectile p)
    {
        if (!p.isPlayerProjectile)
        {
            Damage(p.damage);
            Damage(p.damagePerSecond * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }
}
