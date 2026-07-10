using UnityEngine;


/// <summary>
/// タイトル画面の処理を管理するクラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    //Sound
    [SerializeField]
    private AudioClip buttonSE;//インスペクターから入れる
    [SerializeField]
    private AudioSource audioSource;//インスペクターから入れる

    private bool isPressed = false;
    /// <summary>
    /// スタートボタンが押されたときに呼ぶ
    /// </summary>
    public void GoToNextScene()
    {
        //連続押しを回避
        if (isPressed) return;

        
        if (audioSource != null && buttonSE != null)
        {
            //音を鳴らす
            audioSource.PlayOneShot(buttonSE);
        }

        if (SystemManager.Instance != null)
        {
            //フェードをしながらゲームに遷移させる
            SystemManager.Instance.StartFadeAndLoad("Game",1.0f);
            isPressed = true;
        }

    }
}
