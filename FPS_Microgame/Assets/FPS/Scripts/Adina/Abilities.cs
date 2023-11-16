using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Ability 1")]
    public Image abilityImage1;
    public Text abilityText1;
    public KeyCode ability1Key;
    public float ability1Cooldown = 5;

    [Header("Ability 2")]
    public Image abilityImage2;
    public Text abilityText2;
    public KeyCode ability2Key;
    public float ability2Cooldown = 5;

    [Header("Ability 3")]
    public Image abilityImage3;
    public Text abilityText3;
    public KeyCode ability3Key;
    public float ability3Cooldown = 5;

    private bool isAbility1Cooldown = false;
    private bool isAbility2Cooldown = false;
    private bool isAbility3Cooldown = false;

    private float currentAbility1Cooldown;
    private float currentAbility2Cooldown;
    private float currentAbility3Cooldown;

    public Text lockText2;
    public Text lockText3;

    public GameObject arrow2;
    public GameObject arrow3;

    public bool abilityUnlocked2 = false;
    public bool abilityUnlocked3 = false;

    public int seconds;
    public bool levelUp = false;

    public int availableAbilityPoints = 0;

    void Start()
    {
        abilityImage1.fillAmount = 0;

        abilityText1.text = "";
        abilityText2.text = "";
        abilityText3.text = "";

        lockText2.enabled = false;
        lockText3.enabled = false;

        seconds = 3;

        arrow2.SetActive(false); 
        arrow3.SetActive(false); 
    }

    void Update()
    {
        if (this.GetComponent<ExperienceSystem>().currentLevel == 1)
        {
            Ability1Input();
            AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
            PauseInBetween(ability2Key, lockText2);
            PauseInBetween(ability3Key, lockText3);
        }
        else
        {
            if (this.GetComponent<ExperienceSystem>().currentLevel == 2)
            {
                UnlockAbility(); 

                Ability1Input();
                AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);

                Ability2Input();
                AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);

                Ability3Input();
                AbilityCooldown(ref currentAbility3Cooldown, ability3Cooldown, ref isAbility3Cooldown, abilityImage3, abilityText3);

                if (abilityUnlocked2 == true)
                    PauseInBetween(ability3Key, lockText3);
                
                if (abilityUnlocked3 == true)
                    PauseInBetween(ability2Key, lockText2);
                
            } else 
            {
                if (this.GetComponent<ExperienceSystem>().currentLevel >= 3)
                {
                    UnlockAbility();

                    Ability1Input();
                    AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);

                    Ability2Input();
                    AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);

                    Ability3Input();
                    AbilityCooldown(ref currentAbility3Cooldown, ability3Cooldown, ref isAbility3Cooldown, abilityImage3, abilityText3);
                }
        }   }
    }

    private void PauseInBetween(KeyCode keycode, Text text)
    {
         if (Input.GetKeyDown(keycode))
        {
            StartCoroutine(Wait(text, seconds));
        }
    }

    private IEnumerator Wait(Text text, float time)
    {
        text.enabled = !text.enabled;
        yield return new WaitForSeconds(time);
        text.enabled = !text.enabled;
    }

    private void Ability1Input()
    {
        if (Input.GetKeyDown(ability1Key) && !isAbility1Cooldown)
        {
            isAbility1Cooldown = true;
            currentAbility1Cooldown = ability1Cooldown;
        }
    }

    private void Ability2Input()
    {
        if (Input.GetKeyDown(ability2Key) && !isAbility2Cooldown && abilityUnlocked2 == true)
        {
            //Debug.Log("Ability 2");
            isAbility2Cooldown = true;
            currentAbility2Cooldown = ability2Cooldown;
        }
    }

    private void Ability3Input()
    {
        if (Input.GetKeyDown(ability3Key) && !isAbility3Cooldown && abilityUnlocked3 == true)
        {
            isAbility3Cooldown = true;
            currentAbility3Cooldown = ability3Cooldown;
        }
    }

    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if (skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
                if (skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }
        }
    }

    private void UnlockAbility()
    {
        if(availableAbilityPoints > 0 && abilityUnlocked2 == false && abilityUnlocked3 == false)
        {
            arrow2.SetActive(true); 
            arrow3.SetActive(true); 
        } 
        else if(availableAbilityPoints > 0 && abilityUnlocked2 == true) 
        {
            arrow3.SetActive(true); 
        } 
        else if(availableAbilityPoints > 0 && abilityUnlocked3 == true) 
        {
            arrow2.SetActive(true); 
        }
        
        //Debug.Log($"Before Unlock - Ability 2: {abilityUnlocked2}, Ability 3: {abilityUnlocked3}");
        if (Input.GetKeyDown(KeyCode.F2) && abilityUnlocked2 == false && availableAbilityPoints > 0 && this.GetComponent<ExperienceSystem>().currentLevel <=3)
        {
            //Debug.Log("Key2");
            abilityImage2.fillAmount = 0;
            abilityUnlocked2 = true; 
            arrow2.SetActive(false); 
            arrow3.SetActive(false); 
            availableAbilityPoints --;
        }
        else if (Input.GetKeyDown(KeyCode.F3) && abilityUnlocked3 == false  && availableAbilityPoints > 0 && this.GetComponent<ExperienceSystem>().currentLevel <=3)
             {
                //Debug.Log("Key3");
                abilityImage3.fillAmount = 0;
                abilityUnlocked3 = true;
                arrow2.SetActive(false); 
                arrow3.SetActive(false); 
                availableAbilityPoints --;
             }
        //Debug.Log($"After Unlock - Ability 2: {abilityUnlocked2}, Ability 3: {abilityUnlocked3}");
    }

    public bool AbilityAvailable()
    {
        availableAbilityPoints++;
        return false;
    }
}
