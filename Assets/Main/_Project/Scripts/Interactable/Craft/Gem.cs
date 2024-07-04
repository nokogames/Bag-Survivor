using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace _Project.Scripts.Interactable.Craft
{

    public class Gem : MonoBehaviour, ICraftable
    {
        [SerializeField] private GameObject craftParticle;
        [SerializeField] private int gemCount;
        [SerializeField] private GameObject gemPrefab;
        [SerializeField] private List<CuttableSecion> cuttableSecions;
        private float _remainPercentage = 100;

        private bool _canCraftable = true;
        public bool CanCraftable => _canCraftable;

        public Transform Transform => transform;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        public void Craft(float craftPercentage)
        {
            _remainPercentage -= craftPercentage;
            SetModelAndGemStatus();
        }
        public void CraftWithParticle(float craftPercentage)
        {
            _remainPercentage -= craftPercentage;
            SetModelAndGemStatus();
            var obj = ParticlePool.SharedInstance.GetPooledObject(craftParticle);
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }

        private int _craftLvl = 0;

        private void SetModelAndGemStatus()
        {
            if (_remainPercentage < 90)
            {
                cuttableSecions[0].SetActive(false);
                if (_craftLvl == 0) CreateGem();
            }
            if (_remainPercentage < 50)
            {
                cuttableSecions[1].SetActive(false);
                if (_craftLvl == 1) CreateGem();
            }

            if (_remainPercentage <= 0)
            {
                if (_craftLvl == 2) CreateGem();
                cuttableSecions[2].SetActive(false);
                Crafted();
            }
        }

        private void CreateGem()
        {
            _craftLvl++;
            var gem = ParticlePool.SharedInstance.GetPooledObject(gemPrefab);
            gem.transform.position = transform.position;
            gem.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            gem.transform.GetComponent<Collider>().enabled = false;
            gem.SetActive(true);
            gem.transform.DOJump(gem.transform.position + (gem.transform.forward), 1f, 1, .5f);
        }

        private void Crafted()
        {
            _canCraftable = false;
            _collider.enabled = false;
        }
    }

    [Serializable]
    public class CuttableSecion
    {
        public List<GameObject> objects;

        internal void SetActive(bool v)
        {
            objects.ForEach(x => x.SetActive(v));
        }
    }
}