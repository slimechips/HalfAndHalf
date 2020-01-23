using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D collider)
    {
        ICollideWithWalls target = collider.GetComponent<ICollideWithWalls>();
        if (target != null)
        {
            target.CollideWithWall(this);
        }
    }
}
