using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject floor;
    public GameObject wall;

    private Array2D map;
    private const float oneTile = 2.0f;
    private const float floorSize = 10.0f / oneTile;

    private void Start()
    {
        Array2D mapData = new Array2D(10, 10);
        mapData.Set(1, 1, 1);
        Create(mapData);
    }

    /**
    * グリッド座標をワールド座標に変換
    */
    public static float ToWorldX(int xGrid)
    {
        return xGrid * oneTile;
    }

    public static float ToWorldZ(int zGrid)
    {
        return zGrid * oneTile;
    }

    /**
    * ワールド座標をグリッド座標に変換
    */
    public static int ToGridX(float xWorld)
    {
        return Mathf.FloorToInt(xWorld / oneTile);
    }

    public static int ToGridZ(float zWorld)
    {
        return Mathf.FloorToInt(zWorld / oneTile);
    }

    /**
     * マップデータの作成
     */
    public void Create(Array2D mapData)
    {
        map = mapData;
        float floorW = map.width / floorSize;
        float floorH = map.height / floorSize;
        floor.transform.localScale = new Vector3(floorW, 1, floorH);
        float floorX = (map.width - 1) / 2.0f * oneTile;
        float floorZ = (map.height - 1) / 2.0f * oneTile;
        floor.transform.position = new Vector3(floorX, 0, floorZ);
        for (int z = 0; z < map.height; z++)
        {
            for(int x = 0; x < map.width; x++)
            {
                if(map.Get(x, z) > 0)
                {
                    GameObject block = Instantiate(wall);
                    float xBlock = ToWorldX(x);
                    float zBlock = ToWorldZ(z);
                    block.transform.localScale = new Vector3(oneTile, 2, oneTile);
                    block.transform.position = new Vector3(xBlock, 1, zBlock);
                    block.transform.SetParent(floor.transform.GetChild(0));
                }
            }
        }
    }

    /**
     * 生成したマップのリセット
     */
    public void Reset()
    {
        Transform walls = floor.transform.GetChild(0);
        for(int i = 0; i < walls.childCount; i++)
        {
            Destroy(walls.GetChild(i).gameObject);
        }
    }

    /**
     * 指定の座標が壁かどうかをチェック
     */
    public bool IsCollide(int xGrid, int zGrid)
    {
        return map.Get(xGrid, zGrid) != 0;
    }
}
