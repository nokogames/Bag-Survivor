
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO
{

    [CreateAssetMenu(fileName = "PlayerUpgradeDataSO", menuName = "ScriptableObjects/RuntimeDatas/PlayerUpgradeDataSO", order = 0)]
    public class PlayerUpgradeDataSO : ScriptableObject
    {
        [SerializeField]
        public BarData barData;
        [SerializeField]
        public SavedPlayerData savedPlayerData;
        [SerializeField]
        public PlayerUpgradedData playerUpgradedData;

    }

    [Serializable]
    public class PlayerUpgradedData
    {
        public float damage = 0;//=
        public float range = 0;//=
        public float firerate = 0;//=
        public float botDamage = 0;
        public float botFirerate = 0;
        public float healt = 0;
        //Aditional mechanics
        public float pickUpRange = 0;
        public int botCount = 0;

        //Collectable
        public float collectedXpMultiply = 1;
        public float healtRegenRate = 0;//Can yenilemesini hizlandir

        private List<IPlayerUpgradedReciver> _recivers = new();
        public void AddReciver(IPlayerUpgradedReciver reciver)
        {
            if (!_recivers.Contains(reciver)) _recivers.Add(reciver);
        }
        public void RemoveReciver(IPlayerUpgradedReciver reciver)
        {
            if (_recivers.Contains(reciver)) _recivers.Remove(reciver);
        }

        public void Upgraded()
        {
            Debug.Log($"111  PlayerUpgradedData  {_recivers.Count} ");
            _recivers.ForEach(x => Debug.Log($"111 {x}"));
            _recivers.ForEach(x => x.OnUpgraded());
        }
        internal void Reset()
        {


            damage = 0;//=
            range = 0;//=
            firerate = 0;//=
            botDamage = 0;
            botFirerate = 0;
            //Aditional mechanics
            pickUpRange = 0;
            botCount = 0;

            //Collectable
            collectedXpMultiply = 1;
            healtRegenRate = 0;//Ca
            healt = 0;

        }
    }
    [Serializable]
    public class SavedPlayerData
    {

        public float damage = 0;//=
        public float range = 0;//=
        public float firerate = 0;//=
        public float botDamage = 0;
        public float botFirerate = 0;
        public float healtRegenAmount = 0;
        public float pickUpRange = 0;
        public int botCount = 0;
        public float healt = 0;
        public float speed=0;

        private List<IPlayerUpgradedReciver> _recivers = new();
        public void AddReciver(IPlayerUpgradedReciver reciver)
        {
            if (!_recivers.Contains(reciver)) _recivers.Add(reciver);
        }
        public void RemoveReciver(IPlayerUpgradedReciver reciver)
        {
            if (_recivers.Contains(reciver)) _recivers.Remove(reciver);
        }
        public void Upgraded()
        {
            _recivers.ForEach(x => x.OnUpgraded());
        }
    }

    [Serializable]
    public class BarData
    {




        public int StartLvl;
        public int NextLvl;
        public float BarFillAmount;
        public void Reset()
        {
            StartLvl = 0;
            NextLvl = 1;
            BarFillAmount = 0;
        }
        public void LevelUp()
        {
            StartLvl++;
            NextLvl++;
            BarFillAmount = 0;
        }
    }





    public interface IPlayerUpgradedReciver
    {
        public void OnUpgraded();


    }

}