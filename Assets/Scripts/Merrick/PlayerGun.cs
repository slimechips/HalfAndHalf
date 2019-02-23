using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour {

    [SerializeField] private bool player1 = true;
    [SerializeField] private PlayerBullet bullet = null;

    [SerializeField] private float rotateAngle = 90;
    [SerializeField] private float fireRate = 5;
    [System.NonSerialized] public float cooldown = 0;
    private float _period = 10;
    private float period {
        get {
            if (ComboManager.State == ComboManager.ComboState.BulletSpinClock
            || ComboManager.State == ComboManager.ComboState.BulletSpinAntiC) {
                return 0.02f;
            }
            else if (ComboManager.State == ComboManager.ComboState.ShipOfTheLine) {
                return 0.04f;
            }
            else return _period;
        }
        set {
            _period = value;
        }
    }

    private void Start() {
        period = 1 / fireRate;
        if (bullet == null) {
            Debug.Log("no bullet prefab");
            this.enabled = false;
        }
    }

    private void FixedUpdate() {

        if (ComboManager.State == ComboManager.ComboState.BulletSpinClock
            || ComboManager.State == ComboManager.ComboState.BulletSpinAntiC
            || ComboManager.State == ComboManager.ComboState.ShieldStrike
            || ComboManager.State == ComboManager.ComboState.ShipOfTheLine) {

            if (player1) transform.up = -transform.parent.right;
            else transform.up = transform.parent.right;
        }
        else {
            if (player1) {
                if (!Input.GetButton("p1 x"))
                    transform.up = Vector3.RotateTowards(-transform.parent.right,
                                                         new Vector3(Input.GetAxis("p1 left horizontal"), Input.GetAxis("p1 left vertical"), 0).normalized,
                                                         Mathf.Deg2Rad * rotateAngle, 0);
            }
            else {
                if (!Input.GetButton("p2 x"))
                    transform.up = Vector3.RotateTowards(transform.parent.right,
                                                         new Vector3(Input.GetAxis("p2 left horizontal"), Input.GetAxis("p2 left vertical"), 0).normalized,
                                                         Mathf.Deg2Rad * rotateAngle, 0);
            }
        }

        if (cooldown > 0) {
            cooldown -= Time.fixedDeltaTime;
        }
        else if (ComboManager.State == ComboManager.ComboState.ShieldStrike) {; }
        else if (player1) {
            if ((Input.GetButton("p1 a") && !Input.GetButton("p1 x"))
                 || ComboManager.State == ComboManager.ComboState.BulletSpinClock
                 || ComboManager.State == ComboManager.ComboState.BulletSpinAntiC
                 || ComboManager.State == ComboManager.ComboState.ShipOfTheLine) {
                cooldown += period;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
        else {
            if ((Input.GetButton("p2 a") && !Input.GetButton("p2 x"))
                 || ComboManager.State == ComboManager.ComboState.BulletSpinClock
                 || ComboManager.State == ComboManager.ComboState.BulletSpinAntiC
                 || ComboManager.State == ComboManager.ComboState.ShipOfTheLine) {
                cooldown += period;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }
}
