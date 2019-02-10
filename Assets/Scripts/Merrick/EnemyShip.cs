using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship {


    public override void OnDeath() {
        // spawn death expxlosion
        Destroy(gameObject);
    }

    public override bool ReceiveProjectile(Projectile p) {
        if (p.isPlayerProjectile) {
            Damage(p.damage);
            Damage(p.damagePerSecond * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }
}
