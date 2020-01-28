using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public GameObject gameAssets;
    private static GameAssets _i;
    public AudioClip EnemyDeathClip;
    public AudioClip CollideWithShield;
    public AudioClip EnemyHit;
    public AudioClip PlayerHit;
    public AudioClip PlayerShoot;
    public AudioClip EnemyShoot;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = (Instantiate(Resources.Load("Prefabs/GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return _i;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameAssets, 3f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
