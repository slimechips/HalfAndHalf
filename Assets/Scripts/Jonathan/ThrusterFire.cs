using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterFire : MonoBehaviour
{

    public string axisName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        bool v = (Input.GetAxis(axisName) > 0);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, v? 1f:0f);

   }

}
