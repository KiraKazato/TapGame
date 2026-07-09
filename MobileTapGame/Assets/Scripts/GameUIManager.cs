using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//TextMeshProを使うのに必要
using UnityEngine.UI;
using System;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI scoreText;//インスペクターから入れる

    //枠画像を入れる
    [SerializeField]
    private Image TimeGaugeFrame;//インスペクターから入れる

    //グラデーション用
    [SerializeField]
    private Gradient gradient;//インスペクターから入れる

    /// <summary>
    /// 現在のタイムをtextに表示する
    /// </summary>
    public void UpdateTimeUI(float currentTime,float maxTime)
    {
        //タイムを1.0~0.0にする
        float rate = currentTime / maxTime;
        if(rate < 0 )
        {
            rate = 0; 
        }
        //画像のfillamountに適用
        TimeGaugeFrame.fillAmount = rate;
        //画像の色にも適用(グラデーションかけれる)
        TimeGaugeFrame.color = gradient.Evaluate(rate);
    }

    /// <summary>
    /// 現在のスコアをtextに表示する
    /// </summary>
    public void UpdateScoreText(int currentScore)
    {
        if(scoreText != null) 
        {
            //textに文字列を代入
            scoreText.text = "スコア : "+currentScore.ToString();
        }
    }
}
