using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private int maxChasers, maxShooters, maxKamikazes, maxRapids;
    private int curChasers, curShooters, curKamikazes, curRapids;

    // store in array: maxHealth, rotSpd, moveSpd, shotSpd, shootRange, shootDelay
    [SerializeField]
    private float[] chaserStats = new float[7], shooterStats = new float[7], kamikazeStats = new float[7], rapidStats = new float[7];

    private Object chaserPf, shooterPf, kamikazePf, rapidPf;
    //private List<GameObject> chasers, shooters, kamikazes;

    private float enemySpawnTime = 5f, timer = 5f;
    private float diffTime = 5f, diffTimer = 5f;

    void Awake()
    {
        //chasers = new List<GameObject>();
        //shooters = new List<GameObject>();
        //kamikazes = new List<GameObject>();

        // do the resources.load at the start to save memory
        chaserPf = Resources.Load("Prefabs/Chaser", typeof(GameObject));
        shooterPf = Resources.Load("Prefabs/Shooter", typeof(GameObject));
        kamikazePf = Resources.Load("Prefabs/Kamikaze", typeof(GameObject));
        rapidPf = Resources.Load("Prefabs/Rapid", typeof(GameObject));
    }

    private void Start()
    {
        //InvokeRepeating("Spawner", 3, 10);
    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = enemySpawnTime;
            Spawner();
        }

        if(diffTimer > 0)
        {
            diffTimer -= Time.deltaTime;
        }
        else
        {
            diffTimer = diffTime;
            enemySpawnTime -= 0.5f;
            enemySpawnTime = Mathf.Max(enemySpawnTime, 3f);
            maxChasers += 1;
            maxShooters += 1;
            maxKamikazes += 1;
            maxRapids += 1;
        }
    }

    private void Spawner()
    {
        List<string> enemies = new List<string>();
        if (curChasers < maxChasers) enemies.Add("chaser");
        if (curShooters < maxShooters) enemies.Add("shooter");
        if (curKamikazes < maxKamikazes) enemies.Add("kamikaze");
        if (curRapids < maxRapids) enemies.Add("rapid");

        int choice = Random.Range(0, enemies.Count);

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

            case "rapid":
                go = Instantiate(rapidPf) as GameObject;
                go.GetComponent<Rapid>().Initialise(player, gameObject, rapidStats);
                curRapids++;
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

            case "rapid":
                curRapids--;
                break;
        }
    }
}
