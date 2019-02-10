using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {

    public float thrust = 1;
    [System.NonSerialized] public new Rigidbody2D rigidbody;

    public string axisName = "";

    private void Start() {
        
        rigidbody = transform.parent.GetComponent<Rigidbody2D>();
        if (rigidbody == null) {
            Debug.LogWarning("the parent of " + gameObject.name + " does not have a rigidbody, self-deleting");
            Destroy(this);
        }
    }

    private void Update() {
        rigidbody.AddForceAtPosition(thrust * Input.GetAxis(axisName) * transform.up, transform.position, ForceMode2D.Force);
    }
}
