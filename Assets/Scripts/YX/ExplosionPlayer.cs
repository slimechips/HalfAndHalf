using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", 2f);
    }

    // Update is called once per frame
    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
