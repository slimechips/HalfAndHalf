using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship {
    
    public static PlayerShip playerShip = null;

    private void Start() {
        if (playerShip != null) Destroy(playerShip);
        playerShip = this;
    }

    public override void OnDeath() {
        // spawn explosions
        // trigger defeat screen

        //placeholder
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;
    }

    public override bool ReceiveProjectile(Projectile p) {
        if (!p.isPlayerProjectile) {
            Damage(p.damage);
            Damage(p.damagePerSecond * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }
}
