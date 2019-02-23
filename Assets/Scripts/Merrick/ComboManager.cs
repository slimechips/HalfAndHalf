using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour {

    public enum ComboState {
        None,
        BulletSpinClock,
        BulletSpinAntiC,
        ShieldStrike,
        ShipOfTheLine,
        Fortress,
        SwordAndBoardP1,
        SwordAndBoardP2,
        Cooldown,
    }

    public enum Button {
        a,
        b,
        x,
        y,
    }

    public static ComboState State = ComboState.None;

    private Button p1LastButton = Button.a;
    private float p1ButtonTime = 1;
    private Button p2LastButton = Button.a;
    private float p2ButtonTime = 1;

    private float timer = 0;

    private new Rigidbody2D rigidbody;
    private float savedDrag;
    private float savedAngularDrag;

    [SerializeField] private Thruster p1Thruster = null;
    [SerializeField] private Thruster p2Thruster = null;

    [SerializeField] private PlayerGun p1Gun = null;
    [SerializeField] private PlayerGun p2Gun = null;

    [SerializeField] private PlayerShield p1Shield = null;
    [SerializeField] private PlayerShield p2Shield = null;

    [SerializeField] private BigShield p1BigShield = null;
    [SerializeField] private BigShield p2BigShield = null;

    private void Start() {
        State = ComboState.None; // should be using singleton here but i'm a lazy ass
        rigidbody = transform.GetComponent<Rigidbody2D>();
        if (rigidbody == null) {
            Debug.LogWarning("the parent of " + gameObject.name + " does not have a rigidbody, self-deleting");
            Destroy(this);
        }
        savedDrag = rigidbody.drag;
        savedAngularDrag = rigidbody.angularDrag;
    }

    private void Update() { // Do not shift to FixedUpdate! ButtonDOWN requires update or a annoyingly long fix.

        if (Input.GetButtonDown("p1 a")) { p1LastButton = Button.a; p1ButtonTime = 0; }
        else if (Input.GetButtonDown("p1 x")) { p1LastButton = Button.x; p1ButtonTime = 0; }
        else if (Input.GetButtonDown("p1 y")) { p1LastButton = Button.y; p1ButtonTime = 0; }

        if (Input.GetButtonDown("p2 a")) { p2LastButton = Button.a; p2ButtonTime = 0; }
        else if (Input.GetButtonDown("p2 x")) { p2LastButton = Button.x; p2ButtonTime = 0; }
        else if (Input.GetButtonDown("p2 y")) { p2LastButton = Button.y; p2ButtonTime = 0; }

        if (State == ComboState.None) {

            float angle1 = Vector3.Angle(new Vector3(Input.GetAxis("p1 left horizontal"), Input.GetAxis("p1 left vertical"), 0), transform.up);
            float angle2 = Vector3.Angle(new Vector3(Input.GetAxis("p2 left horizontal"), Input.GetAxis("p2 left vertical"), 0), transform.up);

            if (angle1 < 15 && angle2 > 165
                && p1LastButton == Button.y && p1ButtonTime < 0.25f
                && p2LastButton == Button.a && p2ButtonTime < 0.25f) {
                BulletSpin(true);
            }
            else if (angle2 < 15 && angle1 > 165
                     && p2LastButton == Button.y && p2ButtonTime < 0.25f
                     && p1LastButton == Button.a && p1ButtonTime < 0.25f) {
                BulletSpin(false);
            }
            else if (angle1 < 25 && angle2 < 25
                     && Input.GetButton("p1 x") && Input.GetButton("p2 x")
                     && p1LastButton == Button.y && p1ButtonTime < 0.25f
                     && p2LastButton == Button.y && p2ButtonTime < 0.25f) {
                ShieldStrike();
            }
            else if (angle1 < 105 && angle2 < 105
                     && angle1 > 75 && angle2 > 75
                     && Input.GetButton("p1 a") && Input.GetButton("p2 a")
                     && p1LastButton == Button.a && p1ButtonTime < 0.25f
                     && p2LastButton == Button.a && p2ButtonTime < 0.25f) {
                ShipOfTheLine(); // controls are awkward, may change
            }
            else if (p1LastButton == Button.x && p1ButtonTime < 0.15f
                     && p2LastButton == Button.x && p2ButtonTime < 0.15f) {
                Fortress(); // window is shorter to avoid triggering when trying to do shield strike
            }
            else if (Input.GetButton("p1 x")
                     && p1LastButton == Button.a && p1ButtonTime < 0.25f
                     && p2LastButton == Button.a && p2ButtonTime < 0.25f) {
                SwordAndBoard(true);
            }
            else if (Input.GetButton("p2 x")
                     && p1LastButton == Button.a && p1ButtonTime < 0.25f
                     && p2LastButton == Button.a && p2ButtonTime < 0.25f) {
                SwordAndBoard(false);
            }
        }
        else if (State == ComboState.Cooldown) {
            timer += Time.deltaTime;
            if (timer < 0) State = ComboState.None;
        }
        else {
            timer += Time.deltaTime;
            if (timer < 0) {
                State = ComboState.Cooldown;
                rigidbody.drag = savedDrag;
                rigidbody.angularDrag = savedAngularDrag;
            }
        }
    }

    private void LateUpdate() {
        if (p1Thruster.waiting && Input.GetButtonDown("p2 x")) p2Thruster.timeCounter = p1Thruster.timeCounter;
        else if (p2Thruster.waiting && Input.GetButtonDown("p1 x")) p1Thruster.timeCounter = p2Thruster.timeCounter;
    }

    private void BulletSpin(bool clockwise) {
        p1Thruster.ResetNitro(); p2Thruster.ResetNitro();
        p1Gun.cooldown = 0; p2Gun.cooldown = 0;
        p1Shield.Reset(); p2Shield.Reset();
        rigidbody.angularVelocity = clockwise ? 540 : -540;
        rigidbody.angularDrag = 0;
        timer = 2f;
        State = clockwise ? ComboState.BulletSpinClock : ComboState.BulletSpinAntiC;
    }

    private void ShieldStrike() {
        p1Thruster.ResetNitro(); p2Thruster.ResetNitro();
        rigidbody.velocity = transform.up * 2f;
        rigidbody.drag = savedDrag * 2;
        rigidbody.angularVelocity = 0;
        timer = 1f;
        State = ComboState.ShieldStrike;
    }

    private void ShipOfTheLine() {
        p1Thruster.ResetNitro(); p2Thruster.ResetNitro();
        p1Gun.cooldown = 0; p2Gun.cooldown = 0;
        p1Shield.Reset(); p2Shield.Reset();
        rigidbody.velocity = transform.up * 2f;
        rigidbody.drag = 0;
        rigidbody.angularVelocity = 0;
        timer = 3f;
        State = ComboState.ShipOfTheLine;
    }

    private void Fortress() {
        p1Thruster.ResetNitro(); p2Thruster.ResetNitro();
        p1Shield.Reset(); p2Shield.Reset();
        p1BigShield.gameObject.SetActive(true); p1BigShield.timeToSelfDisable = 2f;
        p2BigShield.gameObject.SetActive(true); p2BigShield.timeToSelfDisable = 2f;
        timer = 2f;
        State = ComboState.Fortress;
    }

    private void SwordAndBoard(bool player2side) {
        p1Thruster.ResetNitro(); p2Thruster.ResetNitro();
        if (!player2side) { p1BigShield.gameObject.SetActive(true); p1BigShield.timeToSelfDisable = 5f; }
        else { p2BigShield.gameObject.SetActive(true); p2BigShield.timeToSelfDisable = 5f; }
        timer = 5f;
        State = player2side? ComboState.SwordAndBoardP2 : ComboState.SwordAndBoardP1;
    }
}
