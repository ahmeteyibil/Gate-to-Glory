using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    int selectedLevelID = 1;
    LevelData levelData;
    public void PlayButtonHandler()
    {
        levelData = LevelDataManager.Instance.LoadLevelData(selectedLevelID);
        // Play buton level 1'i açan buton gibi ayarlandý geçci oolarak.
        Debug.Log("level name: " + levelData.levelName);
        LevelDataHolder.SetLevelData(levelData);
        SceneManager.LoadSceneAsync(SceneName.SN_PLAY);
    }
    public void CharacterButtonHandler()
    {
        SceneManager.LoadSceneAsync(SceneName.SN_CHOOSE_CHARACTER);
    }
    public void SettingsButtonHandler()
    {

    }
}
