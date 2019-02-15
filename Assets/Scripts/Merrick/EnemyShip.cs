using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship {

    private GameObject _player;
    public GameObject player {
        get { return _player; }
    }

    private float _rotSpd, _moveSpd;
    public float rotSpd {
        get { return _rotSpd; }
    }
    public float moveSpd {
        get { return _moveSpd; }
    }

    public void Initialise (GameObject player, float rotSpd, float moveSpd) {
        this._player = player;
        this._rotSpd = rotSpd;
        this._moveSpd = moveSpd;
    }

    public override void OnDeath() {
        // spawn death explosion
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
