

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level/Level", order = 0)]
public class EnemyLevelSpawnData : ScriptableObject
{
    public List<EnmeySectionSpawnData> enmeySectionSpawnDatas;
    public EnmeySectionSpawnData EnmeySectionSpawnData(int sectionId) => enmeySectionSpawnDatas[sectionId];
}

