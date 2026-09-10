using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateManager : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] Image backgroundImage;
    GateSetManager gateSetManager;
    public static event Action<GameObject> OnPassedGate;
    Collider2D gateCollider;
    Gate gateData;
    private void Awake()
    {
        gateCollider = GetComponent<Collider2D>();
        gateSetManager = GetComponentInParent<GateSetManager>();
    }
    public void InitializeGate(Gate gateData)
    {
        this.gateData = gateData;
        label.text = gateData.label;
        backgroundImage.color = GetColorByGateType(gateData);
    }
    Color GetColorByGateType(Gate gateData)
    {
        if (gateData.isHidden) return Color.yellow;
        bool isBad = (gateData.type == OperationType.Additive && gateData.value < 0f) || (gateData.type == OperationType.Multiplier && gateData.value < 1f);
        if (isBad) return Color.red;
        switch (gateData.type)
        {
            case OperationType.Additive:
                return Color.green;
            case OperationType.Multiplier:
                return Color.blue;
            case OperationType.AdditivePercent:
                return Color.cyan;
            default:
                return Color.yellow;
        }
    }
    public Gate GetGateInfo()
    {
        return gateData;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.TAG_PLAYER))
        {
            if (!gateSetManager.Interacted)
            {
                InteractGate();
            }
        }
    }
    void InteractGate()
    {
        gateSetManager.Interacted = true;
        Color tempColor = backgroundImage.color;
        tempColor.a = .5f;
        backgroundImage.color = tempColor;
        BuffEvent.Trigger(gateData);
        OnPassedGate?.Invoke(gameObject);
    }
}
