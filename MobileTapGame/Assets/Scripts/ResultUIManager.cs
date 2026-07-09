using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;//インスペクターから入れる

    /// <summary>
    /// 点数を入れてもらいtextに表示する関数
    /// </summary>
    public void ResultScoreText(int finalScore)
    {
        if (scoreText != null)
        {
            //textに文字列を代入
            scoreText.text = finalScore.ToString() + "点";
        }
    }
}
