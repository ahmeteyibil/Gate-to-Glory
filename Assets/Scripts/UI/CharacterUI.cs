using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider healthSlider;
    [SerializeField] float healthBarAnimationDuration = 0.2f;
    // Start yerine OnEnable kullanmak daha güvenlidir
    private void OnEnable()
    {
        BaseHealthController.OnHealthChanged += UpdateHealthUI;
    }

    // --- KRÝTÝK DÜZELTME ---
    // Obje yok edilirken veya pasif olurken abonelikten çýkmalýsýn.
    private void OnDisable()
    {
        BaseHealthController.OnHealthChanged -= UpdateHealthUI;
    }
    public void UpdateHealthUI(GameObject obj, float health, float maxHealth)
    {
        if(obj == gameObject)
        {
            healthSlider.DOValue(health / maxHealth, healthBarAnimationDuration);
            healthText.text = ((int)Mathf.Round(health)).ToString();
        }
    }
}
