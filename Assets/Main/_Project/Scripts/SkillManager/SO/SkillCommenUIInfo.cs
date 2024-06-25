
using System;
using System.Collections.Generic;
using _Project.Scripts.SkillManager;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillCommenUIInfo", menuName = "ScriptableObjects/SkillSystem/SkillCommenUIInfo", order = 1)]
public class SkillCommenUIInfo : ScriptableObject
{
    public List<SkillRarityIconHolder> skillRarityIconHolders;
}
[Serializable]
public struct SkillRarityIconHolder
{
    public SkillRarity rarity;
    public Sprite rarityBgImage;
}