using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public UnityEngine.Tilemaps.Tilemap tilemap;
    [SerializeField] public int tileMapXMax, tileMapXMin, tileMapYMax, tileMapYMin;
    [SerializeField] private StageManager stageManager;
    public float WorldXMax { get; private set; }
    public float WorldXMin { get; private set; }
    public float WorldYMax { get; private set; }
    public float WorldYMin { get; private set; }
    //public List<Stage> stageList = new List<Stage>();

    private static EnemyManager _current;
    public static EnemyManager current {
        get
        {
            if (_current == null)
            {
                throw new System.NullReferenceException();
            }
            return _current;
        }
        set
        {
            _current = value;
        }
    }
    private Stage curStage { get { return stageManager.CurStage; } }
    //private static Stage _curStage;
    //private static Stage curStage
    //{
    //    set
    //    {
    //        if (current.stageList.Count == 0)
    //        {
    //            _curStage = null;
    //        }
    //        else
    //        {
    //            _curStage = value;
    //            curStageNumber = current.stageList.IndexOf(value) + 1;
    //            current.LoadStage(value);
    //            value.StartLevel(current);
    //        }
    //    }
    //    get { return _curStage; }
    //}
    //[SerializeField] private static int curStageNumber;

    [SerializeField]
    private int maxChasers, maxShooters, maxKamikazes, maxRapids;
    [SerializeField]
    private int totalChasers, totalShooters, totalKamikazes, totalRapids;
    [SerializeField]
    private int spawnedChasers, spawnedShooters, spawnedKamikazes, spawnedRapids;

    [SerializeField]
    private int curChasers, curShooters, curKamikazes, curRapids;

    // store in array: maxHealth, rotSpd, moveSpd, shotSpd, shootRange, shootDelay
    [SerializeField]
    private float[] chaserStats = new float[7], shooterStats = new float[7], kamikazeStats = new float[7], rapidStats = new float[7];

    private Object chaserPf, shooterPf, kamikazePf, rapidPf;
    //private List<GameObject> chasers, shooters, kamikazes;

    private float enemySpawnTime, timer;
    private float diffTime, diffTimer;
    private int diffCount = 0;

    void Awake()
    {
        LoadPrefabs();
        current = this;
        SetSpawningZones();
        //curStage = stageList[0];
        //chasers = new List<GameObject>();
        //shooters = new List<GameObject>();
        //kamikazes = new List<GameObject>();

        // do the resources.load at the start to save memory

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
            TriggerEnemyCheck();
            timer = enemySpawnTime;
            curStage.SpawnEnemies(current);
        }

        if(diffTimer > 0)
        {
            diffTimer -= Time.deltaTime;
        }
        else if (diffCount > 0)
        {
            diffTimer = diffTime;
            enemySpawnTime -= 0.5f;
            enemySpawnTime = Mathf.Max(enemySpawnTime, 3f);
            maxChasers += 1;
            maxShooters += 1;
            maxKamikazes += 1;
            maxRapids += 1;
            diffCount--;
        }
    }

    private void LoadPrefabs()
    {
        chaserPf = Resources.Load("Prefabs/Chaser", typeof(GameObject));
        shooterPf = Resources.Load("Prefabs/Shooter", typeof(GameObject));
        kamikazePf = Resources.Load("Prefabs/Kamikaze", typeof(GameObject));
        rapidPf = Resources.Load("Prefabs/Rapid", typeof(GameObject));
    }

    private void SetSpawningZones()
    {
        Vector3 bottomLeft = tilemap.GetCellCenterWorld(new Vector3Int(tileMapXMin, tileMapYMin, 0));
        Vector3 topRight = tilemap.GetCellCenterWorld(new Vector3Int(tileMapXMax, tileMapYMax, 0));
        WorldXMin = bottomLeft.x;
        WorldYMin = bottomLeft.y;
        WorldXMax = topRight.x;
        WorldYMax = topRight.y;
    }

    public void LoadStage(Stage stage)
    {
        maxChasers = stage.MaxChasers;
        totalChasers = stage.TotalChasers;
        spawnedChasers = 0;
        maxKamikazes = stage.MaxKamikazes;
        totalKamikazes = stage.TotalKamikazes;
        spawnedKamikazes = 0;
        maxRapids = stage.MaxRapids;
        totalRapids = stage.TotalRapids;
        spawnedRapids = 0;
        maxShooters = stage.MaxShooters;
        totalShooters = stage.TotalShooters;
        spawnedShooters = 0;
        diffTime = stage.MaxIncreaseInterval;
        diffTimer = stage.MaxIncreaseInterval;
        diffCount = stage.MaxIncreaseCount;
        timer = stage.SpawnInterval;
        enemySpawnTime = stage.SpawnInterval;
    }

    public bool Spawner()
    {
        List<string> enemies = new List<string>();
        if (curChasers < maxChasers && spawnedChasers < totalChasers) enemies.Add("chaser");
        if (curShooters < maxShooters && spawnedShooters < totalShooters) enemies.Add("shooter");
        if (curKamikazes < maxKamikazes && spawnedKamikazes < totalKamikazes) enemies.Add("kamikaze");
        if (curRapids < maxRapids && spawnedRapids < totalRapids) enemies.Add("rapid");

        if (enemies.Count == 0) return false;
        int choice = Random.Range(0, enemies.Count);

        return SpawnEnemy(enemies[choice]);

    }

    private bool SpawnEnemy(string enemy)
    {
        GameObject go = null;

        Vector3 camPos;
        int maxSpawnAttemptsLeft = 10;
        do
        {
            --maxSpawnAttemptsLeft;
            // randomly choose a point along the bounds
            // first choose an axis
            float x_coord = 0;
            float y_coord = 0;
            int axis = Random.Range(0, 4);
            if (axis == 0)
            {
                // btm
                x_coord = Random.Range(0, (float)Camera.main.pixelWidth);
            }
            else if (axis == 1)
            {
                // left
                y_coord = Random.Range(0, (float)Camera.main.pixelHeight);
            }
            else if (axis == 2)
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
            camPos = Camera.main.ScreenToWorldPoint(new Vector3(x_coord, y_coord, 0));
        } while (WithinBounds(camPos) != ValidCoord.VALID && maxSpawnAttemptsLeft > 0);

        if (WithinBounds(camPos) != ValidCoord.VALID) return false;

        switch (enemy)
        {
            default:
                return false;

            case "chaser":
                go = Instantiate(chaserPf) as GameObject;
                go.GetComponent<Chaser>().Initialise(player, gameObject, chaserStats);
                curChasers++;
                spawnedChasers++;
                break;

            case "shooter":
                go = Instantiate(shooterPf) as GameObject;
                go.GetComponent<Shooter>().Initialise(player, gameObject, shooterStats);
                curShooters++;
                spawnedShooters++;
                break;

            case "kamikaze":
                go = Instantiate(kamikazePf) as GameObject;
                go.GetComponent<Kamikaze>().Initialise(player, gameObject, kamikazeStats);
                curKamikazes++;
                spawnedKamikazes++;
                break;

            case "rapid":
                go = Instantiate(rapidPf) as GameObject;
                go.GetComponent<Rapid>().Initialise(player, gameObject, rapidStats);
                curRapids++;
                spawnedRapids++;
                break;

        }
        go.transform.position = (Vector2)camPos;
        return true;
    }

    private bool TriggerEnemyCheck()
    {
        Stage.Result result = curStage.EnemyCheck();

        if (result == Stage.Result.USE_DEFAULT)
        {
            result = DefaultEnemyCheck();
        }

        switch (result)
        {
            case Stage.Result.IN_PROGRESS:
                return false;
            case Stage.Result.FINISHED:
                stageManager.GoNextStage();
                return true;
            default:
                return false;
        }
    }

    public void UpdateCurEnemies(EnemyShip enemy)
    {
        if (enemy is Chaser)
        {
            curChasers--;
        }
        else if (enemy is Shooter)
        {
            curShooters--;
        }
        else if (enemy is Kamikaze)
        {
            curKamikazes--;
        }
        else if (enemy is Rapid)
        {
            curRapids--;
        }
    }


    //public bool TriggerStageCheck()
    //{
    //    Stage.Result result = CurStage.StageCheck();

    //    if (result == Stage.Result.USE_DEFAULT)
    //    {
    //        result = DefaultEnemyCheck();
    //    }

    //    switch (result)
    //    {
    //        case Stage.Result.IN_PROGRESS:
    //            return false;
    //        case Stage.Result.FINISHED:
    //            GoNextStage();
    //            return true;
    //        default:
    //            return false;
    //    }
    //}

    private Stage.Result DefaultEnemyCheck()
    {
        if (curChasers == 0 && curKamikazes == 0 && curRapids == 0 && curShooters == 0
            && spawnedChasers >= totalChasers && spawnedKamikazes >= totalKamikazes
            && spawnedRapids >= totalRapids && spawnedShooters >= totalShooters)
        {
            return Stage.Result.FINISHED;
        }
        return Stage.Result.IN_PROGRESS;
    }

    //private void GoNextStage()
    //{
    //    if (curStageNumber >= stageList.Count)
    //    {
    //        Debug.Log("Insert Finish Screen");
    //        return;
    //    }
    //    Debug.Log("Loading next stage");
    //    curStage = stageList[curStageNumber];
    //}

    public ValidCoord WithinBounds(Vector3 vector3)
    {
        if (vector3.x > WorldXMax) return ValidCoord.X_LARGE;
        if (vector3.x < WorldXMin) return ValidCoord.X_SMALL;
        if (vector3.y > WorldYMax) return ValidCoord.Y_LARGE;
        if (vector3.y < WorldYMin) return ValidCoord.Y_SMALL;
        return ValidCoord.VALID;
    }

}

public enum ValidCoord { VALID, X_LARGE, X_SMALL, Y_LARGE, Y_SMALL }
