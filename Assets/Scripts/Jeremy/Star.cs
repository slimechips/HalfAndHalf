using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private float margin = 10f;
    private Vector3 scale;

    private float period, phase;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;

        period = Random.Range(8f, 16f);
        phase = Random.Range(0, Mathf.PI * 2);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // scale/alpha goes up and down
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Sin(phase + Time.time / period * 2 * Mathf.PI));
        transform.localScale = scale * (0.5f + Mathf.Sin(phase + Time.time / period * 2 * Mathf.PI));

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
