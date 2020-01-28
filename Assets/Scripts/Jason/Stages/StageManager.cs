using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager _current;
    public static StageManager current
    {
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

    public List<Stage> stageList = new List<Stage>();
    [SerializeField] private EnemyManager enemyManager;

    private Stage _curStage;
    public Stage CurStage
    {
        set
        {
            if (current.stageList.Count == 0)
            {
                _curStage = null;
            }
            else
            {
                _curStage = value;
                curStageNumber = current.stageList.IndexOf(value) + 1;
                enemyManager.LoadStage(value);
                value.StartLevel(enemyManager);
            }
        }
        get { return _curStage; }
    }
    [SerializeField] private static int curStageNumber;

    void Awake()
    {
        current = this;
        CurStage = stageList[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GoNextStage()
    {
        if (curStageNumber >= stageList.Count)
        {
            Debug.Log("Insert Finish Screen");
            return;
        }
        Debug.Log("Loading next stage");
        CurStage = stageList[curStageNumber];
    }
}
