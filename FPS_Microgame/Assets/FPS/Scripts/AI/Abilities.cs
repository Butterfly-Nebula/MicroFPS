using System.Collections;
using System.Collections.Generic;
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

    public bool abilityUnlock2 = false;
    public bool abilityUnlock3 = false;

    public int seconds;
    public bool levelUp;

    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;

        abilityText1.text = "";
        abilityText2.text = "";
        abilityText3.text = "";

        lockText2.enabled = false;
        lockText3.enabled = false;

        levelUp = false;

        seconds = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<ExperienceSystem>().currentLevel == 1)
        {
            Ability1Input();
            AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
            if (Input.GetKeyDown(ability2Key))
            {
                StartCoroutine(Wait(lockText2, seconds));
            }
            if (Input.GetKeyDown(ability3Key))
            {
                StartCoroutine(Wait(lockText3, seconds));
            }

        }
        else
        {
            if (this.GetComponent<ExperienceSystem>().currentLevel == 2)
            {
                Ability1Input();
                AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
                if (abilityUnlock2 == true)
                {
                    Ability2Input();
                    AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);
                    if (Input.GetKeyDown(ability3Key))
                    {
                        StartCoroutine(Wait(lockText3, seconds));
                    }
                }
                if (abilityUnlock3 == true)
                {
                    Ability3Input();
                    AbilityCooldown(ref currentAbility3Cooldown, ability3Cooldown, ref isAbility3Cooldown, abilityImage3, abilityText3);
                    if (Input.GetKeyDown(ability2Key))
                    {
                        StartCoroutine(Wait(lockText2, seconds));
                    }
                }
            }
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
        if (Input.GetKeyDown(ability2Key) && !isAbility2Cooldown)
        {
            isAbility2Cooldown = true;
            currentAbility2Cooldown = ability2Cooldown;
        }
    }

    private void Ability3Input()
    {
        if (Input.GetKeyDown(ability3Key) && !isAbility3Cooldown)
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

    public bool UnlockAbility()
    {
        if (Input.GetKeyDown(ability2Key))
        {
            Debug.Log("Key2");
            abilityImage2.fillAmount = 0;
            abilityUnlock2 = true;
            return false;
        }
        else if (Input.GetKeyDown(ability3Key))
        {
            Debug.Log("Key3");
            abilityImage3.fillAmount = 0;
            abilityUnlock3 = true;
            return false;
        }
        return true;
    }
}
