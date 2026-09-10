using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatInfoPopup : MonoBehaviour
{
    public static StatInfoPopup Instance;
    [SerializeField] TMP_Text statHeaderText, statDescText;
    [SerializeField] Vector3 positionOffset;
    [SerializeField] CanvasGroup cg;
    StatType currentType;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowTooltip(StatType type, Vector3 imagePosition)
    {
        if (currentType == type)
        {
            currentType = StatType.EmptyType;
            HideTooltip();
            return;
        }

        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        
        statHeaderText.text = type.ToString();
        statDescText.text = StatProcesses.GetDescriptionByType(type);
        //transform.position = imagePosition + positionOffset;
        currentType = type;
    }
    void HideTooltip()
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
