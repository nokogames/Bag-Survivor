

using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Level;
using UnityEngine;
using VContainer;

namespace _Project.Scripts
{

    [System.Serializable]
    public class ParticleObjectPoolItem
    {

        public GameObject objectToPool;
        public int amountToPool;
        public bool shouldExpand = true;

        public ParticleObjectPoolItem(GameObject obj, int amt, bool exp = true)
        {
            objectToPool = obj;
            amountToPool = Mathf.Max(amt, 2);
            shouldExpand = exp;
        }
    }

    public class ParticlePool : MonoBehaviour
    {

        public static ParticlePool SharedInstance;
        public List<ParticleObjectPoolItem> itemsToPool;
        public Dictionary<GameObject, int> objectToIndex;

        public List<List<GameObject>> pooledObjectsList;
        public List<GameObject> pooledObjects;
        private List<int> positions;

        [Inject]
        public void InjectDependenciesAndInitialize(InLevelEvents events)
        {
            events.onNextSection += SetActiveFalseAll;
            events.onNextLevel += SetActiveFalseAll;
        }

        private void SetActiveFalseAll()
        {
            for (int i = 0; i < pooledObjectsList.Count; i++)
            {
                var crr = pooledObjectsList[i];
                for (int j = 0; j < crr.Count; j++)
                {
                    crr[j].SetActive(false);
                }

            }
        }

        private void FillItemTOIndex()
        {
            pooledObjectsList.ForEach(o => o.ForEach(x => x.gameObject.SetActive(false)));
        }

        void Awake()
        {
            if (SharedInstance == null)
            {

                DontDestroyOnLoad(this.gameObject);
                SharedInstance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
            objectToIndex = new();
            pooledObjectsList = new List<List<GameObject>>();
            pooledObjects = new List<GameObject>();
            positions = new List<int>();


            for (int i = 0; i < itemsToPool.Count; i++)
            {
                ObjectPoolItemToPooledObject(i);
            }


        }
        public GameObject GetPooledObject(GameObject go)
        {
            try
            {
                var index = objectToIndex[go];
                return GetPooledObject(index);
            }
            catch (System.Exception)
            {
                Debug.LogError($"{go.transform.name}");
                throw;
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
                //  DontDestroyOnLoad(obj);
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
            ParticleObjectPoolItem item = new ParticleObjectPoolItem(GO, amt, exp);
            int currLen = itemsToPool.Count;
            itemsToPool.Add(item);
            ObjectPoolItemToPooledObject(currLen);
            return currLen;
        }


        void ObjectPoolItemToPooledObject(int index)
        {
            ParticleObjectPoolItem item = itemsToPool[index];
            objectToIndex[item.objectToPool] = index;

            pooledObjects = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                pooledObjects.Add(obj);
                //   DontDestroyOnLoad(obj);
            }
            pooledObjectsList.Add(pooledObjects);
            positions.Add(0);

        }
        // public GameObject GetInstanceObj(int index){
        //     ObjectPoolItem item = itemsToPool[index];
        //     GameObject obj = (GameObject)Instantiate(item.objectToPool);
        //     return obj;
        // }
    }
}
