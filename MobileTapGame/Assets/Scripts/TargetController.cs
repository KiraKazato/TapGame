using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    private GameObject burstEffectPrefab;//インスペクターから入れる
    private GameManager gameManager;
    [SerializeField]
    private int targetScore = 100;
    void Start()
    {
        //インスペクターから入れないで探す方法
        gameManager = FindObjectOfType<GameManager>();
    }

    //マウスがTargetの上で押されたか
    private void OnMouseDown()
    {
        if (gameManager != null)
        {
            //スコア加算
            gameManager.AddScore(targetScore);
        }
        //このスクリプトを入れてる的があった場所にエフェクトを生成する
        Instantiate(burstEffectPrefab, transform.position, Quaternion.identity);
        //消す
        Destroy(this.gameObject);
    }
}
