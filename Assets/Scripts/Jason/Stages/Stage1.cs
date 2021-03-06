﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    [SerializeField] protected int _maxChasers, _maxShooters, _maxKamikazes, _maxRapids;
    [SerializeField] protected int _totalChasers, _totalShooters, _totalKamikazes, _totalRapids;
    [SerializeField] protected float _maxIncreaseInterval;
    [SerializeField] protected int _maxIncreaseCount;
    [SerializeField] protected float _spawnInterval;
    [SerializeField] protected StageType _stageType = StageType.NORMAL;

    public override int MaxChasers { get => _maxChasers; }
    public override int TotalChasers { get => _totalChasers; }

    public override int MaxShooters { get => _maxShooters; }
    public override int TotalShooters { get => _totalShooters; }


    public override int MaxKamikazes { get => _maxKamikazes; }
    public override int TotalKamikazes { get => _totalKamikazes; }

    public override int MaxRapids { get => _maxRapids; }
    public override int TotalRapids { get => _totalRapids; }
    
    public override float MaxIncreaseInterval { get => _maxIncreaseInterval; }
    public override int MaxIncreaseCount { get => _maxIncreaseCount; }
    public override float SpawnInterval { get => _spawnInterval > 0f ? _spawnInterval : base.SpawnInterval; }
    public override StageType StageType { get => _stageType; }

    public override bool SpawnEnemies(EnemyManager manager)
    {
        return manager.Spawner();
    }
}
