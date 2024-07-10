using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO
{
    //  [CreateAssetMenu(fileName = "PlayerUpgradeItem", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem", order = 0)]
    public class PlayerMainMenuUpgradeInfo : ScriptableObject
    {
        public virtual float Value { get; }
        public virtual float NextValue { get; }
        public virtual int Price { get; }
        public Sprite BgSprite;
        public String UpgradeInfo;
        public int Lvl = 0;
        public bool IsCompleted = false;

        // public virtual void Initialize(GameData gameData)
        // {

        // }
        public virtual void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {

        }
        public virtual void Upgraded(SavedPlayerData savedPlayerData)
        {

        }


    }
}

