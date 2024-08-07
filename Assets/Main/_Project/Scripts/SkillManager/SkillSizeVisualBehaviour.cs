using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSizeVisualBehaviour : MonoBehaviour
{
    [SerializeField] private List<SkillColumnData> skillColumnDatas;
    private void Awake()
    {
        Clear();
    }
    public void Initialize(Vector2Int size)
    {
        skillColumnDatas.ForEach(e => e.Close());
        for (int i = 0; i < size.y; i++)
        {
            var crrCol = skillColumnDatas[i];
            for (int j = 0; j < size.x; j++) crrCol.SetActive(j);
        }
    }

    internal void Clear()
    {
        skillColumnDatas.ForEach(skillColumnData => skillColumnData.Close());
    }
}

[Serializable]
public class SkillColumnData
{
    public List<GameObject> rowElements;
    public void Close() => rowElements.ForEach(e => e.SetActive(false));
    public void SetActive(int index) => rowElements[index].SetActive(true);
}
