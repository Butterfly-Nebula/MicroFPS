using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToLevelUp = 100;
    public int expIncreaseRatio = 2;

    public Slider expSlider;
    public Text levelText;

    public GameObject player;
    public GameObject turret;

    public bool leveledUp = false;

    public void Start()
    {
        //player = GameObject.FindWithTag("Player");
        //turret = GameObject.FindWithTag("EnemyTurret");
    }

    public void Update()
    {
        UpdateUI();
        if(leveledUp)
        {
            Debug.Log("Entered");
            leveledUp = player.GetComponent<Abilities>().UnlockAbility();
        }
        /*if (turret.GetComponent<Health>().turretDead == true)
        {
            LevelUp();
        }*/
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
        expToLevelUp *= expIncreaseRatio;
        leveledUp = true;
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
