using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShield : MonoBehaviour, ICollidesWithProjectiles {

    [SerializeField] private bool player1 = true;
    [System.NonSerialized] public float timeToSelfDisable = 5f;

    private void FixedUpdate() {

        if (timeToSelfDisable < 0) {
            gameObject.SetActive(false);
        }
        else {
            timeToSelfDisable -= Time.fixedDeltaTime;
        }
    }

    public bool ReceiveProjectile(Projectile p) {
        return !p.isPlayerProjectile;
    }
}
