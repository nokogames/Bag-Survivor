

using System.Collections.Generic;
using _Project.Scripts.Character.EnemyRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "ScriptableObjects/EnemySpawnData", order = 0)]
public class EnemySpawnData : ScriptableObject
{
    public List<EnemyLevelSpawnData> enemyLevelSpawnDatas;
}


[System.Serializable]
public class WaveInfo
{
    public List<SpawnEnemyInfo> spawnEnemyInfos;
}

[System.Serializable]
public class SpawnEnemyInfo
{
    public EnemyType enemyType;
    public int Count;
}