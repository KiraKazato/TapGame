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
        // ターゲットマネージャーが誕生した瞬間に、出現範囲を計算する
        if (gaugeRect != null && scoreTextRect != null && Camera.main != null)
        {
            // ゲージ枠の四隅の画面上の座標を取得する
            // corners[0]=左下, corners[1]=左上, corners[2]=右上, corners[3]=右下
            Vector3[] gaugeCorners = new Vector3[4];
            gaugeRect.GetWorldCorners(gaugeCorners);
            // スコアテキスト枠の四隅の画面上の座標を取得する
            Vector3[] scoreCorners = new Vector3[4];
            scoreTextRect.GetWorldCorners(scoreCorners);
            // カメラからの奥行き距離を取得する（スクリーン座標→ワールド座標の変換に必要）
            float zDistance = Mathf.Abs(Camera.main.transform.position.z);
            // 画面上の座標を、ゲーム空間の座標（ワールド座標）に変換する
            Vector3 gaugeBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(gaugeCorners[0].x, gaugeCorners[0].y, zDistance));
            Vector3 gaugeTopRight = Camera.main.ScreenToWorldPoint(new Vector3(gaugeCorners[2].x, gaugeCorners[2].y, zDistance));
            Vector3 scoreBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(scoreCorners[0].x, scoreCorners[0].y, zDistance));
            // ターゲットの円がゲージ枠からはみ出さないための余白
            float padding = 1.0f;
            // 変換した座標を元に、ターゲットの出現範囲を上書きする
            minX = gaugeBottomLeft.x + padding; // 左端
            maxX = gaugeTopRight.x - padding;   // 右端
            minY = gaugeBottomLeft.y + padding; // 下端
            maxY = scoreBottomLeft.y - padding; // 上端（スコアテキストの下端に合わせる）
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