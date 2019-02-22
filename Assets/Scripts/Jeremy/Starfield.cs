using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour
{
    public int numStars;

    void Start()
    {
        for(int i = 0; i < numStars; i++)
        {
            float x = Random.Range(0, Camera.main.pixelWidth);
            float y = Random.Range(0, Camera.main.pixelHeight);

            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 20f));

            float scale = Random.Range(0.3f, 0.5f);

            float rotation = Random.Range(0f, 360f);

            int choice = Random.Range(1, 3);

            GameObject go = Instantiate(Resources.Load("Prefabs/Star" + choice.ToString())) as GameObject;
            go.transform.position = pos;
            go.transform.localScale = Vector3.one * scale;
            go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        }
    }
}
