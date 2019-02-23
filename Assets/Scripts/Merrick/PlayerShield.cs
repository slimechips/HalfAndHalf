using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour, ICollidesWithProjectiles {

    [SerializeField] private bool player1 = true;

    [SerializeField] private float rotateAngle = 90;
    [SerializeField] private float minimumOnDuration = 0.5f;

    private bool on = false;
    private float currentDuration = 0;
    private GameObject child = null;

    private void Start() {
        child = transform.GetChild(0).gameObject;
        if (child == null) {
            Debug.LogWarning(gameObject.name + " does not have a child, self-deleting");
            Destroy(this);
        }
        child.SetActive(false);
    }

    private void Update() {
    }

    private void FixedUpdate() {

        if (ComboManager.State == ComboManager.ComboState.ShieldStrike) {
            transform.up = transform.parent.up;
        }
        else if (on) {
            if (player1) {
                transform.up = Vector3.RotateTowards(transform.parent.right,
                                                     new Vector3(Input.GetAxis("p1 left horizontal"), Input.GetAxis("p1 left vertical"), 0).normalized,
                                                     Mathf.Deg2Rad * rotateAngle, 0);
            }
            else {
                transform.up = Vector3.RotateTowards(-transform.parent.right,
                                                     new Vector3(Input.GetAxis("p2 left horizontal"), Input.GetAxis("p2 left vertical"), 0).normalized,
                                                     Mathf.Deg2Rad * rotateAngle, 0);
            }
            currentDuration += Time.fixedDeltaTime;
        }

        if (Input.GetButton(player1 ? "p1 x" : "p2 x")
            && ComboManager.State != ComboManager.ComboState.BulletSpinClock
            && ComboManager.State != ComboManager.ComboState.BulletSpinAntiC
            && ComboManager.State != ComboManager.ComboState.ShipOfTheLine
            && ComboManager.State != ComboManager.ComboState.Fortress) {
            if (!on) {
                on = true;
                currentDuration = 0;
                child.SetActive(true);
            }
        }
        else {
            if (on && currentDuration > minimumOnDuration) {
                on = false;
                child.SetActive(false);
            }
        }
    }

    public bool ReceiveProjectile(Projectile p) {
        return on && !p.isPlayerProjectile;
    }

    public void Reset() {
        on = false;
        child.SetActive(false);
    }
}
