using UnityEngine;

/// <summary>
/// リザルト画面の処理とスコア表示を管理するクラス
/// </summary>
public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private ResultUIManager resultUIManager;//インスペクターから入れる
    //Sound
    [SerializeField]
    private AudioClip buttonSE;//インスペクターから入れる
    [SerializeField]
    private AudioSource audioSource;//インスペクターから入れる

    private bool isPressed = false;

    /// <summary>
    /// シーン開始時にシステムマネージャーから最終スコアを取得
    /// </summary>
    void Start()
    {
        if (SystemManager.Instance != null)
        {
            //SystemManagerから保存されている最終スコアを取得する
            int finalScore = SystemManager.Instance.FinalScore;
            //UIにスコアを渡す
            resultUIManager.ResultScoreText(finalScore);
        }
    }

    /// <summary>
    /// ボタンが押されたら呼ぶ関数
    /// </summary>
    public void GoToNextScene()
    {
        //連続押し回避
        if (isPressed) return;

        if (audioSource != null && buttonSE != null)
        {
            //音を鳴らす
            audioSource.PlayOneShot(buttonSE);
        }

        if (SystemManager.Instance != null)
        {
            //フェードをしながらタイトルに遷移させる
            SystemManager.Instance.StartFadeAndLoad("Title", 1.0f);
        }
    }
}
