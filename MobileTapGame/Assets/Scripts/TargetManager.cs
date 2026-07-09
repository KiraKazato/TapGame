using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    //出す図形
    [SerializeField]
    private GameObject circlePrefab;//インスペクターから入れる



    /// <summary>
    /// 図形を出現させる
    /// </summary>
    public void SpawnObject(float deleteTime)
    {
        //指定の範囲にランダムに
        Vector2 position;
        position.x = Random.Range(-2.0f, 2.0f);
        position.y = Random.Range(-4.0f, 3.5f);
        //指定された場所に図形を出現させその情報を保存する
        GameObject spawnedObject = Instantiate(circlePrefab, position, Quaternion.identity);
        if (spawnedObject != null)
        {
            //出現させた図形を、設定した時間が経過したら自動的に消去する
            Destroy(spawnedObject, deleteTime);
        }    
    }
}