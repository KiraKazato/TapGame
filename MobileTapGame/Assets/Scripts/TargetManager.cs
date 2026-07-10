using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    //出す図形
    [SerializeField]
    private GameObject circlePrefab;//インスペクターから入れる


    //最終的に数値を入れる変数
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    //UI
    [SerializeField]
    private RectTransform gaugeRect;//インスペクターから入れる
    [SerializeField]
    private RectTransform scoreTextRect;//インスペクターから入れる

   

    void Start()
    {
        //ターゲットマネージャーが誕生した瞬間に、出現範囲を計算
        if (gaugeRect != null && scoreTextRect != null && Camera.main != null)
        {
            Vector3[] gaugeCorners = new Vector3[4];
            gaugeRect.GetWorldCorners(gaugeCorners);

            Vector3[] scoreCorners = new Vector3[4];
            scoreTextRect.GetWorldCorners(scoreCorners);

            float zDistance = Mathf.Abs(Camera.main.transform.position.z);

            Vector3 gaugeBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(gaugeCorners[0].x, gaugeCorners[0].y, zDistance));
            Vector3 gaugeTopRight = Camera.main.ScreenToWorldPoint(new Vector3(gaugeCorners[2].x, gaugeCorners[2].y, zDistance));
            Vector3 scoreBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(scoreCorners[0].x, scoreCorners[0].y, zDistance));

            float padding = 0.5f;

            // 自分自身の持つ最小値・最大値を上書きする
            minX = gaugeBottomLeft.x + padding;
            maxX = gaugeTopRight.x - padding;
            minY = gaugeBottomLeft.y + padding;
            maxY = scoreBottomLeft.y - padding;
        }
    }

    /// <summary>
    /// 図形を出現させる
    /// </summary>
    public void SpawnObject(float deleteTime)
    {
        //指定の範囲にランダムに
        Vector2 position;
        position.x = Random.Range(minX, maxX);
        position.y = Random.Range(minY, maxY);
        //指定された場所に図形を出現させその情報を保存する
        GameObject spawnedObject = Instantiate(circlePrefab, position, Quaternion.identity);
        if (spawnedObject != null)
        {
            //出現させた図形を、設定した時間が経過したら自動的に消去する
            Destroy(spawnedObject, deleteTime);
        }

    }
}