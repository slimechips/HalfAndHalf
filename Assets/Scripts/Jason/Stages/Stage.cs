using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Stage: MonoBehaviour
{
    public abstract int MaxChasers { get; }
    public abstract int TotalChasers { get; }
    public abstract int MaxShooters { get; }
    public abstract int TotalShooters { get; }

    public abstract int MaxKamikazes { get; }
    public abstract int TotalKamikazes { get; }

    public abstract int MaxRapids { get; }
    public abstract int TotalRapids { get; }

    public virtual float MaxIncreaseInterval { get { return 5f; } }
    public virtual int MaxIncreaseCount { get { return 0; } }

    public abstract StageType StageType { get; }

    public void StartLevel(EnemyManager manager)
    {
        SpawnEnemies(manager);
    }

    public abstract bool SpawnEnemies(EnemyManager manager);


    public virtual Result StageCheck()
    {
        return Result.USE_DEFAULT;
    }

    public enum Result { USE_DEFAULT, IN_PROGRESS, FINISHED, TLE }
}

public enum StageType { NORMAL, BOSS };