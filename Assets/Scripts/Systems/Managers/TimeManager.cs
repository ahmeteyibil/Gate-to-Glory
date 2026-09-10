using UnityEngine;

public class TimeManager : MonoBehaviour
{
    float timeCounter;
    void Update()
    {
        timeCounter += Time.deltaTime;
        UIManager.Instance.UpdateTimeCounterText(timeCounter);
    }
}
