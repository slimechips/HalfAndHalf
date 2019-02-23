using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {

    private bool nitroOn = false;
    public float thrust = 2f;
    public float nitroThrust = 10f;
    [System.NonSerialized] public bool waiting = false;
    private float nitroDelay = 0.25f;
    private float nitroDuration = 1f;
    [System.NonSerialized] public float timeCounter = -0.25f;
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
            waiting = true;
            timeCounter = nitroDelay;
        }
        if (waiting && timeCounter <= 0) {
            waiting = false;
            nitroOn = true;
            timeCounter = nitroDuration;
        }
        else if (nitroOn && timeCounter < 0) {
            nitroOn = false;
            rigidbody.velocity *= 0.5f;
            rigidbody.angularVelocity *= 0.5f;
        }
    }

    private void FixedUpdate() {
        timeCounter -= Time.fixedDeltaTime;
        if (ComboManager.State == ComboManager.ComboState.BulletSpinClock) {
        }
        else if (ComboManager.State == ComboManager.ComboState.BulletSpinAntiC) {
        }
        else if (ComboManager.State == ComboManager.ComboState.ShieldStrike) {
        }
        else if (ComboManager.State == ComboManager.ComboState.ShipOfTheLine) {
        }
        //else if (ComboManager.State == ComboManager.ComboState.Fortress) {
        //}
        //else if (ComboManager.State == ComboManager.ComboState.SwordAndBoardP1) {
        //}
        //else if (ComboManager.State == ComboManager.ComboState.SwordAndBoardP2) {
        //}
        else if (nitroOn) rigidbody.AddForceAtPosition(nitroThrust * transform.up, transform.position, ForceMode2D.Force);
        else if (Input.GetAxis(player1 ? "p1 b" : "p2 b") > 0.1f) rigidbody.AddForceAtPosition(thrust * transform.up, transform.position, ForceMode2D.Force);
    }

    public void ResetNitro() {
        waiting = false;
        nitroOn = false;
        timeCounter = -0.25f;
    }
}
