using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] TMP_Text levelNameText;

    public void PlayLevelButtonHandler()
    {
        LevelDataHolder.SetLevelData(LevelSwitcher.Instance.ChoosedLevel);
        SceneManager.LoadSceneAsync(SceneName.SN_PLAY);
    }
    public void CharacterButtonHandler()
    {
        SceneManager.LoadSceneAsync(SceneName.SN_CHOOSE_CHARACTER);
    }
    public void SettingsButtonHandler()
    {

    }
    public void PreLevelButtonHandler()
    {
        LevelSwitcher.Instance.SwitchToPreLevel();
    }
    public void NextLevelButtonHandler()
    {
        LevelSwitcher.Instance.SwitchToNextLevel();
    }
    public void EditLevelText(int levelID, string levelName)
    {
        levelNameText.text = $"Level {levelID}:\n{levelName}";
    }
}
