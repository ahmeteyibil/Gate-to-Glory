using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] float panelFadeDuration = 0.2f, menuButtonFadeDuration = 0.3f, levelStatsFadeDuration = 0.3f;
    [SerializeField] CanvasGroup levelStatsCG, menuButtonCG;
    [SerializeField] TMP_Text panelHeaderText,totalDamageText, killCountText, wavesCompletedText;
    CanvasGroup panelCG;
    private void Awake()
    {
        panelCG = GetComponent<CanvasGroup>();
    }
    public void MenuButtonHandler()
    {
        SceneManager.LoadSceneAsync(SceneName.SN_MENU);
        GameManager.Instance.ResumeGame();
    }
    public void PanelOpenSequence()
    {
        panelCG.interactable = true;
        panelCG.blocksRaycasts = true;
        panelHeaderText.text = LevelStatisticsManager.Instance.LevelWon ? "WIN" : "DEFEAT";

        // Sequence oluþtur ve zamaný durdurduðumuz için baðýmsýz yap
        Sequence sequence = DOTween.Sequence().SetUpdate(true);

        // 1. ADIM: Paneli göster (Süre > 0 olmalý!)
        sequence.Append(panelCG.DOFade(1f, panelFadeDuration));

        // 2. ADIM: Tam bu saniyede metinleri güncelle (Ýþlem anlýk olduðu için Callback kullanýyoruz)
        sequence.AppendCallback(() => {
            totalDamageText.text = $"{LevelStatisticsManager.Instance.TotalDamage}";
            killCountText.text = $"{LevelStatisticsManager.Instance.KillCount}";
            wavesCompletedText.text = $"{LevelStatisticsManager.Instance.WavesCompleted}";
            Debug.Log("Metinler þimdi güncellendi!");
        });

        // 3. ADIM: Ýstatistik panelini göster
        sequence.Append(levelStatsCG.DOFade(1f, levelStatsFadeDuration));

        // 4. ADIM: Menü butonunu göster
        sequence.Append(menuButtonCG.DOFade(1f, menuButtonFadeDuration));
        menuButtonCG.interactable = true;
    }
}
