using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EntityManager))]
public class HealthDisplayManager : MonoBehaviour
{
    public HealthFields playerHealthBar;
    public HealthFields[] enemyHealthBars = new HealthFields[3];

    float[] renderHealth = new float[] { 0, 0, 0, 0 };
    float[] lastMaxHealth = new float[] { 0, 0, 0, 0 };
    float[] colorText = new float[] { 0, 0, 0, 0 };

    EntityManager manager;

    void Awake()
    {
        manager = GetComponent<EntityManager>();
    }
    
    void Update()
    {
        Entity player = manager.GetPlayer();
        Entity[] enemies = manager.GetEnemies();

        if (player == null)
        {
            renderHealth[3] /= 5;

            playerHealthBar.healthText.text = string.Format("{0}/{1}", Mathf.RoundToInt(renderHealth[3] * lastMaxHealth[3]), Mathf.RoundToInt(lastMaxHealth[3]));
            
            colorText[3] -= 0.1f;
            if (colorText[3] < 0)
                colorText[3] = 0;
        }
        else
        {
            playerHealthBar.nameText.text = player.entityName;

            renderHealth[3] += (player.stats.health / player.stats.maxHealth - renderHealth[3]) / 5;
            
            playerHealthBar.healthText.text = string.Format("{0}/{1}", Mathf.RoundToInt(renderHealth[3] * player.stats.maxHealth), Mathf.RoundToInt(player.stats.maxHealth));
            lastMaxHealth[3] = player.stats.maxHealth;
            
            colorText[3] += 0.1f;
            if (colorText[3] > 1)
                colorText[3] = 1;
        }

        playerHealthBar.healthBar.fillAmount = renderHealth[3];
        playerHealthBar.healthBar.color = Color.HSVToRGB(renderHealth[3] / 3f, 1, 1);
        playerHealthBar.SetTextColor(new Color(0, 0, 0, colorText[3]));

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                renderHealth[i] /= 5;

                enemyHealthBars[i].healthText.text = string.Format("{0}/{1}", Mathf.RoundToInt(renderHealth[i] * lastMaxHealth[i]), Mathf.RoundToInt(lastMaxHealth[i]));

                colorText[i] -= 0.1f;
                if (colorText[i] < 0)
                    colorText[i] = 0;
            }
            else
            {
                enemyHealthBars[i].nameText.text = enemies[i].entityName;

                renderHealth[i] += (enemies[i].stats.health / enemies[i].stats.maxHealth - renderHealth[i]) / 5;
                
                enemyHealthBars[i].healthText.text = string.Format("{0}/{1}", Mathf.RoundToInt(renderHealth[i] * enemies[i].stats.maxHealth), Mathf.RoundToInt(enemies[i].stats.maxHealth));
                lastMaxHealth[i] = enemies[i].stats.maxHealth;

                colorText[i] += 0.1f;
                if (colorText[i] > 1)
                    colorText[i] = 1;
            }

            enemyHealthBars[i].healthBar.fillAmount = renderHealth[i];
            enemyHealthBars[i].healthBar.color = Color.HSVToRGB(renderHealth[i] / 3f, 1, 1);
            enemyHealthBars[i].SetTextColor(new Color(0, 0, 0, colorText[i]));
        }
    }
}

[System.Serializable]
public class HealthFields
{
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI nameText;

    public void SetTextColor(Color color)
    {
        healthText.color = color;
        nameText.color = color;
    }
}