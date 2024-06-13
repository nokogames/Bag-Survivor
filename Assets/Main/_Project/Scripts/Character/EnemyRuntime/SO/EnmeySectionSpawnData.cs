using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Section", menuName = "ScriptableObjects/Level/Section", order = 0)]
public class EnmeySectionSpawnData : ScriptableObject
{
    public List<WaveInfo> waveInfos;
}