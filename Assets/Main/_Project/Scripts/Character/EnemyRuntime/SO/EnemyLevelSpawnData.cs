

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level/Level", order = 0)]
public class EnemyLevelSpawnData : ScriptableObject
{
    public List<EnmeySectionSpawnData> enmeySectionSpawnDatas;
    public EnmeySectionSpawnData EnmeySectionSpawnData(int sectionId) => enmeySectionSpawnDatas[sectionId];
    public bool IsCompletedSections(int section) => section >= enmeySectionSpawnDatas.Count;
    public int EnemyCount;
    public int TotalCoinCount = 100;
    internal int AllEnemyCount()
    {
        if (EnemyCount != 0) return EnemyCount;

        enmeySectionSpawnDatas.ForEach(s =>
        {
            s.waveInfos.ForEach(w =>
            {
                w.spawnEnemyInfos.ForEach(e =>
                {
                    EnemyCount += e.Count;
                });
            });
        });
        return EnemyCount;
    }

    private void OnValidate()
    {
        EnemyCount = 0;
        enmeySectionSpawnDatas.ForEach(s =>
        {
            s.waveInfos.ForEach(w =>
            {
                w.spawnEnemyInfos.ForEach(e =>
                {
                    EnemyCount += e.Count;
                });
            });
        });
    }
}

