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
        protected GameData _gameData;
        public virtual void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            _gameData = gameData;

        }
        public virtual void Upgraded(SavedPlayerData savedPlayerData)
        {
           data.Level=Lvl;
        }
        public PlayerSavedUpgradeInfos data;
        public void SetupData<T>()
        {
            data = _gameData.GetSavedUpgrade<T>();
        }
    }
}

