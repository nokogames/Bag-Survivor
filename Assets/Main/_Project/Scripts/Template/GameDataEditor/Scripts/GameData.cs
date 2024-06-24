using UnityEngine;

namespace Pack.GameData
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public int CurrentLvl;
        public int CurrentSection;
        public int Money;
        public int Xp;
        
    }

}