using System.Collections;
using System.Collections.Generic;
using Unity.FPS.AI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Game
{
    public class ExperienceSystem : MonoBehaviour
    {
        public int currentLevel = 1;
        public int currentExp = 0;
        public int expToLevelUp = 100;
        public int expIncreaseRatio = 2;

        public Slider expSlider;
        public Text levelText;

        public GameObject player;
        
        public int availableAbilityPoints = 0;

        public List<GameObject> enemies = new List<GameObject>();
        private List<EnemyController> enemyControllers = new List<EnemyController>();

        public bool leveledUp = false;

        //private bool dead = false;

        public void Start()
        {
            foreach (GameObject enemy in enemies)
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyControllers.Add(enemyController);
                }
            }
        }

        public void Update()
        {
            UpdateUI();

            GainExperience();

            /*if (dead == false)
            {
                if (turret.GetComponent<Health>().CurrentHealth == 0)
                {
                    currentLevel++;
                    dead = true;
                    Debug.Log("He is dead => can level!");
                }
            }*/

            if (leveledUp)
            {
                leveledUp = player.GetComponent<Abilities>().AbilityAvailable();
            }
        }

        private void GainExperience()
        {
            foreach (EnemyController enemyC in enemyControllers)
            {
                if (currentLevel == 1 && enemyC.gainExp == true)
                {
                    currentExp += 25;
                    enemyC.gainExp = false;
                }
                else if (currentLevel == 2 && enemyC.gainExp == true)
                {
                    currentExp += 50;
                    enemyC.gainExp = false;
                }
                else if (currentLevel == 3 && enemyC.gainExp == true)
                {
                    currentExp += 80;
                    enemyC.gainExp = false;
                }
            }

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
}
