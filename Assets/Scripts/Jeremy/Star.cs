using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private float margin = 10f;
    private float height;
    private float width;

    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize * 2f;
        width = height * Screen.width / Screen.height;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 finPos = screenPos;
        if(screenPos.x < -margin || screenPos.x > Camera.main.pixelWidth + margin)
        {
            if (screenPos.x < -margin) finPos = Vector3.right * (Camera.main.pixelWidth + margin / 2) + screenPos;
            else finPos = Vector3.left * (Camera.main.pixelWidth + margin / 2) + screenPos;
        }
        else if (screenPos.y < -margin || screenPos.y > Camera.main.pixelHeight + margin)
        {
            if (screenPos.y < -margin) finPos = Vector3.up * (Camera.main.pixelHeight + margin/2) + screenPos;
            else finPos = Vector3.down * (Camera.main.pixelHeight + margin / 2) + screenPos;
        }
        transform.position = Camera.main.ScreenToWorldPoint(finPos);
    }
}
