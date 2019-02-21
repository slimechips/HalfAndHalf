using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    [SerializeField] private GameObject player = null;

    private Vector3 lastPosition;

    private Vector3 speed;

    private void Update() {

        speed = player.GetComponent<Rigidbody2D>().velocity;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                                         transform.position.z) + 2*speed;

    }
}
