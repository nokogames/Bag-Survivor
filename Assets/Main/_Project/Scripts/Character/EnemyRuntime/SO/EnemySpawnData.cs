

using System.Collections.Generic;
using _Project.Scripts.Character.EnemyRuntime;
using UnityEngine;
using static _Project.Scripts.Character.EnemyRuntime.EnemyManager;

[CreateAssetMenu(fileName = "SpawnData", menuName = "ScriptableObjects/EnemySpawnData", order = 0)]
public class EnemySpawnData : ScriptableObject
{
    public List<EnemyLevelSpawnData> enemyLevelSpawnDatas;
    public EnemyLevelSpawnData EnemyLevelSpawnData(int lvl) => enemyLevelSpawnDatas[lvl];
}


[System.Serializable]
public class WaveInfo
{
    public List<SpawnEnemyInfo> spawnEnemyInfos;
    public float timeOffset;
    public bool mustClearNextWave;
}

[System.Serializable]
public class SpawnEnemyInfo
{
    public EnemyType enemyType;
    public int Count;
}