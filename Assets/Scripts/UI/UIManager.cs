using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;
public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text coinCountText, timeCounterText;
    [SerializeField] Sprite fullHeart, emptyHeart, halfHeart;
    [SerializeField] Slider enemyDistanceBar;
    [SerializeField] float markerXOffset = -2f;
    [SerializeField] GameObject sliderDividerPrefab;
    [SerializeField] PausePanelUI pausePanelUI;
    public static UIManager Instance;
    private void OnEnable()
    {
        CurrencyManager.OnCoinChanged += UpdateCoinCountText;
    }
    private void OnDisable()
    {
        CurrencyManager.OnCoinChanged -= UpdateCoinCountText;
    }
    public void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Debug.Log($"Enemy distance bar width: {enemyDistanceBar.GetComponent<RectTransform>().rect.width}");
    }
    public void PauseButtonHandler()
    {
        GameManager.Instance.PauseGame();
        pausePanelUI.OpenPanel();
    }
    public void SetupEnemyDistanceBarMarkers(List<GameObject> enemies, Transform player)
    {
        RectTransform sliderRect = enemyDistanceBar.GetComponent<RectTransform>();
        float barWidth = sliderRect.rect.width;
        Debug.Log($"barWidth: {barWidth}");
        foreach (var enemy in enemies)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, player.position);
            Debug.Log($"Distance to player for {enemy.name}: {distanceToPlayer}");
            float markerXPos = ((distanceToPlayer / EnemyDistanceTracker.Instance.MaxDistance) * barWidth) - (barWidth/2) + markerXOffset; // örn 0.2 * 400 -> 80 konunmunda.
            Debug.Log($"{enemy.name} düþmaný için bar marker x pos: {markerXPos}");
            var newMarker = Instantiate(sliderDividerPrefab, enemyDistanceBar.transform);
            RectTransform markerRect = newMarker.GetComponent<RectTransform>();
            markerRect.anchoredPosition = new Vector2(markerXPos, 0f);
        }
    }
    public void UpdateEnemyDistanceBar()
    {
        enemyDistanceBar.value = EnemyDistanceTracker.Instance.CompleteRatio;
    }
    public void UpdateTimeCounterText(float time)
    {
        int minute = Convert.ToInt32(time) / 60; // 185,5 -> 185 / 60 = 3
        int second = Convert.ToInt32(time) % 60; // 255,3 -> 255 % 60 = 15;
        string timeStr = $"{minute:D2}:{second:D2}";
        timeCounterText.text = timeStr;

    }
    public void UpdateCoinCountText(int coin)
    {
        coinCountText.text = coin.ToString();
    }
}
