using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float duration = 2f;
    private float timeTotal = 0f;
    
    public Image healthBar;
    private Image ghostHealthBar;

    private Text healthText;

    private float startGhostFill; 
    private float targetFill;

    private int currentHealth = 20;

    void Start()
    {
        healthBar = transform.GetChild(1).GetComponent<Image>();
        ghostHealthBar = transform.GetChild(0).GetComponent<Image>();
        healthText = transform.GetChild(2).GetComponent<Text>();

        if (healthBar != null) targetFill = healthBar.fillAmount;
        if (ghostHealthBar != null) startGhostFill = ghostHealthBar.fillAmount;
        UpdateHealthText();
    }

    void Update()
    {
        if (healthBar == null || ghostHealthBar == null) return;

        if (timeTotal < duration)
        {
            timeTotal += Time.deltaTime;
            var t = timeTotal / duration;
            var tSqrt = Mathf.Sqrt(t);
            
            ghostHealthBar.fillAmount = Mathf.Lerp(startGhostFill, targetFill, tSqrt);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (healthBar == null || ghostHealthBar == null) return;

        healthBar.fillAmount = Mathf.Clamp(healthBar.fillAmount - damageAmount, 0f, 1f);
        currentHealth -=1;
        
        targetFill = healthBar.fillAmount;
        startGhostFill = ghostHealthBar.fillAmount; 
        UpdateHealthText();
        
        timeTotal = 0f; 
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth + " / 20";
        }
    }
}