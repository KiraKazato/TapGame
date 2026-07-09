using UnityEngine;

/// <summary>
/// タイトル画面の処理を管理するクラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// スタートボタンが押されたときに呼ぶ
    /// </summary>
    public void GoToNextScene()
    {
        if (SystemManager.Instance != null)
        {
            //フェードをしながらゲームに遷移させる
            SystemManager.Instance.StartFadeAndLoad("Game");
        }
    }
}
