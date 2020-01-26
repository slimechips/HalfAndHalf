using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private float radius = 5f;
    private float angle = 0f;
    private Vector2 leftCentre, rightCentre;
    private RotationMode CurRotationMode = RotationMode.LEFT;
    private enum RotationMode { LEFT, RIGHT };

    // Start is called before the first frame update
    void Start()
    {
        leftCentre = new Vector2(transform.position.x - radius, transform.position.y);
        rightCentre = new Vector2(transform.position.x + radius, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        BossMovement();
    }

    private void BossMovement()
    {
        EllipticalMovement();    
    }

    private void EllipticalMovement()
    {
        switch (CurRotationMode)
        {
            case RotationMode.LEFT:
                if (angle >= 2 * Mathf.PI)
                {
                    CurRotationMode = RotationMode.RIGHT;
                    angle = 0f;
                    RightRotate();
                }
                else
                {
                    LeftRotate();
                }
                break;
            case RotationMode.RIGHT:
                if (angle <= -2 * Mathf.PI)
                {
                    CurRotationMode = RotationMode.LEFT;
                    angle = 0f;
                    LeftRotate();
                }
                else
                {
                    RightRotate();
                }
                break;
        }

    }

    private void LeftRotate()
    {
        angle += (2 * Mathf.PI) / speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius + leftCentre.x;
        float y = Mathf.Sin(angle) * radius + leftCentre.y;
        transform.position = new Vector2(x, y);
    }

    private void RightRotate()
    {
        angle -= (2 * Mathf.PI) / speed * Time.deltaTime;
        float x = -Mathf.Cos(angle) * radius + rightCentre.x;
        float y = -Mathf.Sin(angle) * radius + rightCentre.y;
        transform.position = new Vector2(x, y);
    }
}
