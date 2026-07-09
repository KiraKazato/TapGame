using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム本編の進行、タイマー、スコア、オブジェクトの出現を管理するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    private bool isGameStarted = false;
    private bool isGameOver = false;

    private float timeLimit = 30.0f;
    private float currentTime;
    private int currentScore = 0;

    private float spawnTimer = 0.0f;

    //出す頻度
    [SerializeField]
    private float SpawnInterval = 0.5f;//インスペクターから入れる
    //出してから消すまでの秒数
    [SerializeField]
    private float deleteTime = 1.0f;//インスペクターから入れる

    //クラスの参照
    [SerializeField] 
    private GameUIManager gameUIManager;//インスペクターから入れる
    [SerializeField] 
    private TargetManager targetManager;//インスペクターから入れる

    //ゲームUIの取得
    [SerializeField]
    private RectTransform GaugeFrameRect;

    /// <summary>
    /// ゲーム開始前の初期化を行う
    /// </summary>
    void Start()
    {
        currentTime = timeLimit;

        // UIの初期表示を更新する
        if (gameUIManager != null)
        {
            gameUIManager.UpdateScoreText(currentScore);
            gameUIManager.UpdateTimeUI(currentTime, timeLimit);
        }
    }

    /// <summary>
    /// 毎フレーム呼ばれ、ゲームの進行とタイマー処理を行う
    /// </summary>
    void Update()
    {
        // SystemManagerがフェード処理中の場合は、ゲームを開始・進行させない
        if (SystemManager.Instance != null && SystemManager.Instance.IsFading())
        {
            return;
        }

        // フェードが終わり、まだゲームが開始していなければ開始する
        if (!isGameStarted && !isGameOver)
        {
            StartGame();
        }

        // ゲームが開始していない、または終了している場合は何もしない
        if (!isGameStarted || isGameOver)
        {
            return;
        }

        // 制限時間内の処理
        if (currentTime > 0.0f)
        {
            spawnTimer += Time.deltaTime;
            //現在の出る時間
            float currentInterval = SpawnInterval;
            //現在の消す時間
            float currentDeleteTime = deleteTime;

            //10秒以下になったら出る時間を早く
            if(currentTime <= 10.0f)
            {
                currentInterval = SpawnInterval / 2.0f;
                currentDeleteTime = deleteTime / 2.0f;
            }

            // 一定間隔で的（ターゲット）を出現させる
            if (spawnTimer >= currentInterval)
            {
                targetManager.SpawnObject(currentDeleteTime);
                spawnTimer = 0.0f;
            }

            // 残り時間を減らし、UIを更新する
            currentTime -= Time.deltaTime;

            if (gameUIManager != null)
            {
                gameUIManager.UpdateTimeUI(currentTime, timeLimit);
            }
        }
        else
        {
            // 時間切れになったらゲーム終了処理へ移行する
            currentTime = 0.0f;
            isGameOver = true;

            // システムマネージャーに最終スコアを渡し、リザルト画面へフェード遷移する
            if (SystemManager.Instance != null)
            {                 
                SystemManager.Instance.FinalScore = currentScore;
                SystemManager.Instance.StartFadeAndLoad("Result");
            }
        }
    }

    /// <summary>
    /// ゲームの進行フラグをオンにする
    /// </summary>
    private void StartGame()
    {
        isGameStarted = true;
    }

    /// <summary>
    /// スコアを加算し、UIを更新する
    /// </summary>
    /// <param name="point">加算する点数</param>
    public void AddScore(int point)
    {
        //ゲーム中以外はスコアを加算しない
        if (currentTime <= 0 || !isGameStarted)
        {
            return;
        }
        // タップされたオブジェクトの点数を追加し、画面表示を更新する
        currentScore += point;
        if (gameUIManager != null)
        {
            //UIにスコアを渡す
            gameUIManager.UpdateScoreText(currentScore);
        }
    }
}