using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GateSetManager : MonoBehaviour
{
    [SerializeField] RectTransform gatesParent;
    [SerializeField] GameObject gatePrefab;
    public bool Interacted { get; set; }
    List<Gate> gates;
    List<GameObject> gateObjects = new List<GameObject>();
    private void OnEnable()
    {
        GateManager.OnPassedGate += GatePassHandler;
    }
    private void OnDisable()
    {
        GateManager.OnPassedGate -= GatePassHandler;
    }
    public void InitiazlieGates(List<Gate> _gates)
    {
        gates = _gates;
        CreateGates();
    }
    void CreateGates()
    {
        foreach(var gate in gates)
        {
            var newGate = Instantiate(gatePrefab, gatesParent);
            newGate.GetComponent<GateManager>().InitializeGate(gate);
            gateObjects.Add(newGate);
        }
    }
    void GatePassHandler(GameObject passedGateObject)
    {
        if (!gateObjects.Contains(passedGateObject)) return;
        foreach(var gateObject in gateObjects)
        {
            gateObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}

