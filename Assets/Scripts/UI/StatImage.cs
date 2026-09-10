using UnityEngine;
using UnityEngine.EventSystems;

public class StatImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] StatType statType;
    public StatType StatType => statType;

    public void OnPointerClick(PointerEventData eventData)
    {
        StatInfoPopup.Instance.ShowTooltip(StatType, transform.position);
    }
}
