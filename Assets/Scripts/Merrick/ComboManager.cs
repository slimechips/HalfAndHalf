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
        //SwordAndBoardP1,
        //SwordAndBoardP2,
        Cooldown,
    }

    public enum Button {
        rb,
        b,
        x,
        y,
    }

    public static ComboState State = ComboState.None;

    private Button p1LastButton = Button.rb;
    private float p1ButtonTime = 1;
    private Button p2LastButton = Button.rb;
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

    [SerializeField] private ShieldBash shieldBashDamage = null;

    private void Start() {
        State = ComboState.None; // should be using singleton here but i'm a lazy ass
        rigidbody = transform.GetComponent<Rigidbody2D>();
        if (rigidbody == null) {
            Debug.LogWarning(gameObject.name + " does not have a rigidbody, self-deleting");
            Destroy(this);
        }
        savedDrag = rigidbody.drag;
        savedAngularDrag = rigidbody.angularDrag;
    }

    private void Update() { // Do not shift to FixedUpdate! ButtonDOWN requires update or a annoyingly long fix.

        if (Input.GetButtonDown("p1 rb")) { p1LastButton = Button.rb; p1ButtonTime = 0; }
        else if (Input.GetButtonDown("p1 x")) { p1LastButton = Button.x; p1ButtonTime = 0; }
        else if (Input.GetButtonDown("p1 y")) { p1LastButton = Button.y; p1ButtonTime = 0; }

        if (Input.GetButtonDown("p2 rb")) { p2LastButton = Button.rb; p2ButtonTime = 0; }
        else if (Input.GetButtonDown("p2 x")) { p2LastButton = Button.x; p2ButtonTime = 0; }
        else if (Input.GetButtonDown("p2 y")) { p2LastButton = Button.y; p2ButtonTime = 0; }

        if (State == ComboState.None) {

            PlayerShip.playerShip.energy += 5 * Time.fixedDeltaTime;

            float angle1 = Vector3.Angle(new Vector3(Input.GetAxis("p1 left horizontal"), Input.GetAxis("p1 left vertical"), 0), transform.up);
            float angle2 = Vector3.Angle(new Vector3(Input.GetAxis("p2 left horizontal"), Input.GetAxis("p2 left vertical"), 0), transform.up);
            Debug.Log(angle1 + ", " + angle2 + ", " + (angle1 < 25 && angle2 < 25)
                     /*&& Input.GetButton("p1 x") && Input.GetButton("p2 x")
                     && p1LastButton == Button.y && p1ButtonTime < 0.25f
                     && p2LastButton == Button.y && p2ButtonTime < 0.25f*/);


            if (PlayerShip.playerShip.energy >= 70
                && angle1 < 25 && angle2 > 155
                && p1LastButton == Button.y && p1ButtonTime < 0.25f
                && p2LastButton == Button.rb && p2ButtonTime < 0.25f) {
                PlayerShip.playerShip.energy -= 70;
                BulletSpin(true);
            }
            else if (PlayerShip.playerShip.energy >= 70
                     && angle2 < 25 && angle1 > 155
                     && p2LastButton == Button.y && p2ButtonTime < 0.25f
                     && p1LastButton == Button.rb && p1ButtonTime < 0.25f) {
                PlayerShip.playerShip.energy -= 70;
                BulletSpin(false);
            }
            else if (PlayerShip.playerShip.energy >= 30
                     && angle1 < 30 && angle2 < 30
                     && Input.GetButton("p1 x") && Input.GetButton("p2 x")
                     && p1LastButton == Button.y && p1ButtonTime < 0.25f
                     && p2LastButton == Button.y && p2ButtonTime < 0.25f) {
                PlayerShip.playerShip.energy -= 30;
                ShieldStrike();
                Debug.Log("shieldbash");
            }
            else if (PlayerShip.playerShip.energy >= 80
                     && angle1 < 115 && angle2 < 115
                     && angle1 > 65 && angle2 > 65
                     && Input.GetButton("p1 rb") && Input.GetButton("p2 rb")
                     && p1LastButton == Button.rb && p1ButtonTime < 0.25f
                     && p2LastButton == Button.rb && p2ButtonTime < 0.25f) {
                PlayerShip.playerShip.energy -= 80;
                ShipOfTheLine(); // controls are awkward, may change
                Debug.Log("ship");
            }
            else if (PlayerShip.playerShip.energy >= 40
                     && p1LastButton == Button.x && p1ButtonTime < 0.15f
                     && p2LastButton == Button.x && p2ButtonTime < 0.15f) {
                PlayerShip.playerShip.energy -= 40;
                Fortress(); // window is shorter to avoid triggering when trying to do shield strike
            }
            //else if (PlayerShip.playerShip.energy >= 20
                     //&& Input.GetButton("p1 x")
                     //&& p1LastButton == Button.rb && p1ButtonTime < 0.25f
                     //&& p2LastButton == Button.rb && p2ButtonTime < 0.25f) {
                //PlayerShip.playerShip.energy -= 20;
                //SwordAndBoard(true);
            //}
            //else if (PlayerShip.playerShip.energy >= 20
                     //&& Input.GetButton("p2 x")
                     //&& p1LastButton == Button.rb && p1ButtonTime < 0.25f
                     //&& p2LastButton == Button.rb && p2ButtonTime < 0.25f) {
                //PlayerShip.playerShip.energy -= 20;
                //SwordAndBoard(false);
            //}
        }
        else if (State == ComboState.Cooldown) {
            PlayerShip.playerShip.energy += 5 * Time.fixedDeltaTime;
            timer -= Time.deltaTime;
            if (timer < 0) State = ComboState.None;
        }
        else {
            timer -= Time.deltaTime;
            if (timer < 0) {
                State = ComboState.Cooldown;
                rigidbody.drag = savedDrag;
                rigidbody.angularDrag = savedAngularDrag;
                timer = 1f;
            }
        }

        p1ButtonTime += Time.deltaTime;
        p2ButtonTime += Time.deltaTime;
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
        rigidbody.drag = savedDrag * 10;
        rigidbody.velocity = transform.up * 50f;
        rigidbody.angularVelocity = 0;
        shieldBashDamage.timeToSelfDisable = 1f;
        shieldBashDamage.gameObject.SetActive(true);
        timer = 1f;
        State = ComboState.ShieldStrike;
    }

    private void ShipOfTheLine() {
        p1Thruster.ResetNitro(); p2Thruster.ResetNitro();
        p1Gun.cooldown = 0; p2Gun.cooldown = 0;
        p1Shield.Reset(); p2Shield.Reset();
        rigidbody.drag = 0;
        rigidbody.velocity = transform.up * 5f;
        rigidbody.angularVelocity = 0;
        timer = 1.5f;
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

    //private void SwordAndBoard(bool player2side) {
        //p1Thruster.ResetNitro(); p2Thruster.ResetNitro();
        //if (player2side) { p1BigShield.gameObject.SetActive(true); p1BigShield.timeToSelfDisable = 5f; }
        //else { p2BigShield.gameObject.SetActive(true); p2BigShield.timeToSelfDisable = 5f; }
        //timer = 5f;
        //State = player2side? ComboState.SwordAndBoardP2 : ComboState.SwordAndBoardP1;
    //}
}
