using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour {

    [SerializeField] private bool player1 = true;
    [SerializeField] private PlayerBullet bullet = null;

    [SerializeField] private float rotateAngle = 90;
    [SerializeField] private float fireRate = 5;
    private float cooldown = 0;
    private float period = 10;

    private void Start() {
        period = 1 / fireRate;
        if (bullet == null) {
            Debug.Log("no bullet prefab");
            this.enabled = false;
        }
    }

    private void Update() {
        if (player1) {
            transform.up = Vector3.RotateTowards(-transform.parent.right, 
                                                 new Vector3(Input.GetAxis("p1 left horizontal"), Input.GetAxis("p1 left vertical"), 0).normalized, 
                                                 Mathf.Deg2Rad * rotateAngle, 0);
        }
        else {
            transform.up = Vector3.RotateTowards(transform.parent.right,
                                                 new Vector3(Input.GetAxis("p2 left horizontal"), Input.GetAxis("p2 left vertical"), 0).normalized,
                                                 Mathf.Deg2Rad * rotateAngle, 0);
        }
    }

    private void FixedUpdate() {
        if (cooldown > 0) {
            cooldown -= Time.fixedDeltaTime;
        }
        else if (player1) {
            if (Input.GetButton("p1 b")) {
                cooldown += period;
                Instantiate(bullet, transform.position, transform.rotation);
                //bullet.transform.localRotation = transform.localRotation;
            }
        }
        else {
            if (Input.GetButton("p2 b")) {
                cooldown += period;
                Instantiate(bullet, transform.position, transform.rotation);
                //bullet.transform.localRotation = transform.localRotation;
            }
        }
    }
}
