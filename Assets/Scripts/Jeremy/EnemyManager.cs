using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private int maxChasers, maxShooters, maxKamikazes;
    private int curChasers, curShooters, curKamikazes;

    // store in array: maxHealth, rotSpd, moveSpd, shotSpd, shootRange, shootDelay
    [SerializeField]
    private float[] chaserStats = new float[7], shooterStats = new float[7], kamikazeStats = new float[7];

    private Object chaserPf, shooterPf, kamikazePf;
    //private List<GameObject> chasers, shooters, kamikazes;


    void Awake()
    {
        //chasers = new List<GameObject>();
        //shooters = new List<GameObject>();
        //kamikazes = new List<GameObject>();

        // do the resources.load at the start to save memory
        chaserPf = Resources.Load("Prefabs/Chaser", typeof(GameObject));
        shooterPf = Resources.Load("Prefabs/Shooter", typeof(GameObject));
        kamikazePf = Resources.Load("Prefabs/Kamikaze", typeof(GameObject));
    }

    private void Start()
    {
        InvokeRepeating("Spawner", 3, 10);
    }

    private void Spawner()
    {
        int choice = Random.Range(0, 3);

        string[] enemies = new string[] { "chaser", "shooter", "kamikaze" };

        SpawnEnemy(enemies[choice]);

    }

    private void SpawnEnemy(string enemy)
    {
        GameObject go = null;
        switch (enemy)
        {
            default:
                break;

            case "chaser":
                go = Instantiate(chaserPf) as GameObject;
                go.GetComponent<Chaser>().Initialise(player, gameObject, chaserStats);
                curChasers++;
                break;

            case "shooter":
                go = Instantiate(shooterPf) as GameObject;
                go.GetComponent<Shooter>().Initialise(player, gameObject, shooterStats);
                curShooters++;
                break;

            case "kamikaze":
                go = Instantiate(kamikazePf) as GameObject;
                go.GetComponent<Kamikaze>().Initialise(player, gameObject, kamikazeStats);
                curKamikazes++;
                break;
        }

        // randomly choose a point along the bounds

        // first choose an axis
        float x_coord = 0;
        float y_coord = 0;
        int axis = Random.Range(0, 4);
        if(axis == 0)
        {
            // btm
            x_coord = Random.Range(0, (float)Camera.main.pixelWidth);
        }
        else if(axis == 1)
        {
            // left
            y_coord = Random.Range(0, (float)Camera.main.pixelHeight);
        }
        else if(axis == 2)
        {
            // top
            x_coord = Random.Range(0, (float)Camera.main.pixelWidth);
            y_coord = Camera.main.pixelHeight;
        }
        else
        {
            // right
            x_coord = Camera.main.pixelWidth;
            y_coord = Random.Range(0, (float)Camera.main.pixelHeight);
        }
        Vector3 camPos = Camera.main.ScreenToWorldPoint(new Vector3(x_coord, y_coord, 0));
        go.transform.position = (Vector2)camPos;
    }

    public void OnDeath(string enemy)
    {
        switch (enemy)
        {
            default:
                break;

            case "chaser":
                curChasers--;
                break;

            case "shooter":
                curShooters--;
                break;

            case "kamikaze":
                curKamikazes--;
                break;
        }
    }
}
