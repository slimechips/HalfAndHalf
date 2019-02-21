using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {

    private bool nitroOn = false;
    public float thrust = 2f;
    //[System.NonSerialized]
    public float nitroThrust = 10f;
    private float nitroDuration = 1f;
    private float timeCounter = 1f;
    private float timeBuffer = -0.25f;
    [System.NonSerialized] public new Rigidbody2D rigidbody;

    [SerializeField] private bool player1 = true;

    private void Start() {

        rigidbody = transform.parent.GetComponent<Rigidbody2D>();
        if (rigidbody == null) {
            Debug.LogWarning("the parent of " + gameObject.name + " does not have a rigidbody, self-deleting");
            Destroy(this);
        }
    }

    private void Update() {
        if (Input.GetButtonDown(player1 ? "p1 y" : "p2 y") && !nitroOn && timeCounter < timeBuffer) {
            nitroOn = true;
            timeCounter = nitroDuration;
        }
        if (nitroOn && timeCounter < 0) {
            nitroOn = false;
            rigidbody.velocity *= 0.5f;
            rigidbody.angularVelocity *= 0.5f;
        }
    }

    private void FixedUpdate() {
        timeCounter -= Time.fixedDeltaTime;
        if (nitroOn) rigidbody.AddForceAtPosition(nitroThrust * transform.up, transform.position, ForceMode2D.Force);
        else if (Input.GetAxis(player1 ? "p1 b" : "p2 b") > 0.1f) rigidbody.AddForceAtPosition(thrust * transform.up, transform.position, ForceMode2D.Force);
    }
}
