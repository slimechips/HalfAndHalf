using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidesWithProjectiles {

    bool ReceiveProjectile(Projectile p);
    //returns true if the projectile "hit", false if its ignored like enemy projectiles passing through enemies
}
