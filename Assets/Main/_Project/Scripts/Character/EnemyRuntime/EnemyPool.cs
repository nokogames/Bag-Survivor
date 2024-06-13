




using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Character.EnemyRuntime
{

    [System.Serializable]
    public class EnemyPoolItem
    {
        public EnemyType enemyType;
        public GameObject objectToPool;
        public int amountToPool;
        public bool shouldExpand = true;

        public EnemyPoolItem(GameObject obj, int amt, bool exp = true)
        {
            objectToPool = obj;
            amountToPool = Mathf.Max(amt, 2);
            shouldExpand = exp;
        }
    }

    public class EnemyPool : MonoBehaviour
    {
        public static EnemyPool SharedInstance;
        public List<EnemyPoolItem> itemsToPool;


        public List<List<GameObject>> pooledObjectsList;
        public List<GameObject> pooledObjects;
        private List<int> positions;
        private void OnEnable()
        {
            // GameEvents.OnCompletedLevelChanging += OnLevelCompleted;
        }
        private void OnDisable()
        {
            //  GameEvents.OnCompletedLevelChanging -= OnLevelCompleted;

        }

        private void OnLevelCompleted()
        {
            pooledObjectsList.ForEach(o => o.ForEach(x => x.gameObject.SetActive(false)));
        }

        void Awake()
        {

            SharedInstance = this;

            pooledObjectsList = new List<List<GameObject>>();
            pooledObjects = new List<GameObject>();
            positions = new List<int>();


            for (int i = 0; i < itemsToPool.Count; i++)
            {
                ObjectPoolItemToPooledObject(i);
            }

        }


        public GameObject GetPooledObject(int index)
        {

            int curSize = pooledObjectsList[index].Count;
            for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
            {

                if (!pooledObjectsList[index][i % curSize].activeSelf)
                {
                    positions[index] = i % curSize;
                    return pooledObjectsList[index][i % curSize];
                }
            }

            if (itemsToPool[index].shouldExpand)
            {

                GameObject obj = (GameObject)Instantiate(itemsToPool[index].objectToPool);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                pooledObjectsList[index].Add(obj);
                return obj;

            }
            return null;
        }

        public List<GameObject> GetAllPooledObjects(int index)
        {
            return pooledObjectsList[index];
        }


        public int AddObject(GameObject GO, int amt = 3, bool exp = true)
        {
            EnemyPoolItem item = new EnemyPoolItem(GO, amt, exp);
            int currLen = itemsToPool.Count;
            itemsToPool.Add(item);
            ObjectPoolItemToPooledObject(currLen);
            return currLen;
        }


        void ObjectPoolItemToPooledObject(int index)
        {
            EnemyPoolItem item = itemsToPool[index];

            pooledObjects = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                pooledObjects.Add(obj);
            }
            pooledObjectsList.Add(pooledObjects);
            positions.Add(0);

        }
        public GameObject GetPooledObject(EnemyType enemyType)
        {


            int index = itemsToPool.FindIndex(item => item.enemyType == enemyType);
            if (index == -1) return null;

            return GetPooledObject(index);
        }
        // public GameObject GetInstanceObj(int index){
        //     ObjectPoolItem item = itemsToPool[index];
        //     GameObject obj = (GameObject)Instantiate(item.objectToPool);
        //     return obj;
        // }
    }
}
