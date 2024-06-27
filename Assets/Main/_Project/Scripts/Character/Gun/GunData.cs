
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.UI.Controllers;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/GunData", order = 0)]
public class GunData : ScriptableObject
{
    [SerializeField]
    public PlayerUpgradeDataSO playerUpgradeDataSO;
    public float Damage => baseDamage + playerUpgradeDataSO.savedPlayerData.damage + playerUpgradeDataSO.playerUpgradedData.damage;
    public float SpawnTimeRate => Mathf.Clamp(spawnTimeRate - (playerUpgradeDataSO.savedPlayerData.damage + playerUpgradeDataSO.playerUpgradedData.damage), 0.05f, 10f);
    public int bulletPoolIndex;
    public float spawnTimeRate;
    [SerializeField] private float baseDamage = 1f;

}
