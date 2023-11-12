using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperinceSystem : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToLevelUp = 100;
    public int expIncreaseFactor = 2;

    public Slider expSlider;
    public TextMeshProUGUI levelText;

    public void Update()
    {
        UpdateUI();
    }

    public void GainExperienceFromEnemy(int amount)
    {
        GainExperience(amount);
    }

    private void GainExperience(int amount)
    {
        currentExp += amount;
        while (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExp -= expToLevelUp;
        expToLevelUp *= expIncreaseFactor;
    }

    private void UpdateUI()
    {
        // SLIDER UI
        if (expSlider != null)
        {
            expSlider.maxValue = expToLevelUp;
            expSlider.value = currentExp;
        }

        // TEXT UI
        if (levelText != null)
        {
            levelText.text = currentLevel.ToString();
        }
    }
}
