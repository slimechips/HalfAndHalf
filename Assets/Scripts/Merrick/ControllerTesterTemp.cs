using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTesterTemp : MonoBehaviour {

    private void Update() {
        string pressedbuttons = "";

        List<string> axes = new List<string>() {
            "p1 left horizontal",
            "p1 left vertical",
            "p1 right horizontal",
            "p1 right vertical",
            "p1 a",
            "p1 b",
            "p1 x",
            "p1 y",
        };

        foreach(string s in axes) {
            if (Mathf.Abs(Input.GetAxis(s)) > 0.1f) pressedbuttons += s + ":" + Input.GetAxis(s) + ", ";
        }

        Debug.Log(pressedbuttons);
    }
}
