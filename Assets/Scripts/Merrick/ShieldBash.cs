using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : MonoBehaviour {
    
    [System.NonSerialized] public float timeToSelfDisable = -1f;

    private void FixedUpdate() {

        if (timeToSelfDisable < 0) {
            gameObject.SetActive(false);
        }
        else {
            timeToSelfDisable -= Time.fixedDeltaTime;
        }
    }
}
