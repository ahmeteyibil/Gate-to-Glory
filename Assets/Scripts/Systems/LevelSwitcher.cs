using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    public static LevelSwitcher Instance;
    [SerializeField] MenuUI menuUI;
    public LevelData ChoosedLevel => levelData;
    int selectedLevelID = 1;
    int maxLevelCount;
    LevelData levelData;
    private void Awake()
    {
        Instance = this;
        maxLevelCount = LevelDataManager.Instance.GetLevelCount();
        selectedLevelID = PlayerPrefs.GetInt(PrefKeys.LAST_REACHED_LEVEL_ID, 1); 
    }
    private void Start()
    {
        levelData = LevelDataManager.Instance.LoadLevelData(selectedLevelID);
        menuUI.EditLevelText(levelData.levelID, levelData.levelName);
    }
    public void SwitchToNextLevel()
    {
        if (selectedLevelID >= maxLevelCount) return;
        selectedLevelID++;
        levelData = LevelDataManager.Instance.LoadLevelData(selectedLevelID);
        menuUI.EditLevelText(levelData.levelID, levelData.levelName);
    }
    public void SwitchToPreLevel()
    {
        if (selectedLevelID <= 1) return;
        selectedLevelID--;
        levelData = LevelDataManager.Instance.LoadLevelData(selectedLevelID);
        menuUI.EditLevelText(levelData.levelID, levelData.levelName);
    }
}
