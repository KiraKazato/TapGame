using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体のシステム管理とシーン遷移（フェード処理）を行うクラス
/// </summary>
public class SystemManager : MonoBehaviour
{
    //全体で共有する実体
    public static SystemManager Instance;

    //ゲーム終了時のスコアを入れる
    public int FinalScore = 0;

    //フェード関連
    [SerializeField]
    private GameObject fadePrefab;//インスペクターから入れる
    private Image fadeImage;
    private bool isFadingOut = false;
    private bool isFadingIn = false;
    private float fadeSpeed = 2.0f;
    private string targetSceneName = "";
    float alphaValue = 0.0f;

    /// <summary>
    /// シングルトンの設定とフェード画像の初期化
    /// </summary>
    void Awake()
    {
        //既に存在する場合は重複を防ぐ
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        //自分自身の設定
        Instance = this;

        //ほかのシーンに行っても消さないように設定
        DontDestroyOnLoad(gameObject);

        // フェード用の画像を生成
        GameObject fadeObject = Instantiate(fadePrefab);
        //自身の子要素に
        fadeImage = fadeObject.GetComponentInChildren<Image>();
        //シーン遷移しても消えないように
        DontDestroyOnLoad(fadeObject);
        // 初期状態は透明にする
        SetAlpha(alphaValue);
    }

    void Update()
    {
        // フェードアウト中の処理
        if (isFadingOut)
        {
            // 時間経過で不透明度を上げる
            alphaValue = fadeImage.color.a + (Time.deltaTime * fadeSpeed);
            // 完全に暗くなったらシーン遷移
            if (alphaValue >= 1.0f)
            {
                alphaValue = 1.0f;
                isFadingOut = false;
                SceneManager.LoadScene(targetSceneName);
                // シーン遷移後にフェードインを開始させる
                isFadingIn = true;
            }
            //α値の適用
            SetAlpha(alphaValue);
        }
        // フェードイン中（画面を明るくしていく）の処理
        else if (isFadingIn)
        {
            // 時間経過で不透明度を下げ、画面を明るくする
            float alphaValue = fadeImage.color.a - (Time.deltaTime * fadeSpeed);
            
            // 完全に明るくなったらフェードインを終了する
            if (alphaValue <= 0.0f)
            {
                alphaValue = 0.0f;
                isFadingIn = false;
            }
            //α値の適用
            SetAlpha(alphaValue);
        }
    }

    /// <summary>
    /// アルファ値を設定する
    /// </summary>
    /// <param name="alphaValue">設定するアルファ値（0.0〜1.0）</param>
    private void SetAlpha(float alphaValue)
    {
        Color imageColor = fadeImage.color;
        imageColor.a = alphaValue;
        fadeImage.color = imageColor;
    }

    /// <summary>
    /// フェードアウトを開始させ、指定したシーンへ遷移する
    /// </summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    public void StartFadeAndLoad(string sceneName)
    {
        targetSceneName = sceneName;
        isFadingOut = true;
    }

    /// <summary>
    /// 現在フェード処理中かどうかを判定する
    /// </summary>
    /// <returns>フェードアウトまたはフェードイン中なら true</returns>
    public bool IsFading()
    {
        return isFadingOut || isFadingIn;
    }
}