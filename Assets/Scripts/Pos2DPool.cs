using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos2DPool : MonoBehaviour
{
    private Queue<Pos2D> poolQueue = new Queue<Pos2D>();
    public Pos2D pos2DPrefab; // プレハブを直接設定

    public void InitializePool(int initialPoolSize)
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Pos2D newObj = CreateNewObject();
            poolQueue.Enqueue(newObj);
        }
    }

    public Pos2DPool(Pos2D prefab, int initialPoolSize)
    {
        this.pos2DPrefab = prefab;
        for(int i = 0; i < initialPoolSize; i++)
        {
            Pos2D newObj = CreateNewObject();
            poolQueue.Enqueue(newObj);
        }
    }

    public Pos2D GetObjectFromPool()
    {
        if(poolQueue.Count > 0)
        {
            Pos2D obj = poolQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Pos2D newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    private Pos2D CreateNewObject()
    {
        Pos2D newObj = pos2DPrefab; // プレハブのPos2Dコンポーネントを使う
        newObj.gameObject.SetActive(false);
        return newObj;
    }
}
