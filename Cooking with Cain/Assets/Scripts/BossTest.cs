using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : Entity
{
    public EnemyLoader loader;
    public Enemy pea;

    StatusInstance defense = null;
    int cooldown = 0;

    void Start()
    {
        TooltipTextWithIngredients tooltip = gameObject.AddComponent<TooltipTextWithIngredients>();
        tooltip.text = "Ingredients:";
        List<Ingredient> ingredients = new List<Ingredient>();
        ingredients.AddRange(GetComponent<EnemyAction>().ingredients);
        tooltip.sprites = ingredients.ConvertAll(ingredient => ingredient.sprite);
    }

    public override void UpdateStartPlayerTurn()
    {
        if (manager.GetEnemyRemaining() > 1)
        {
            defense.duration = 100;
        }
        else
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                defense = AddStatus(StatusInstance.Status.defup, 0.5f, 100);
                defense.customMessage = "Damage taken reduced by half until all peas are defeated";

                manager.AddEnemyToQueue(loader.GenerateEnemy(pea));
                manager.AddEnemyToQueue(loader.GenerateEnemy(pea));

                cooldown = 3;
            }
        }
    }

    public override bool UpdateStart()
    {
        if (manager.GetEnemyRemaining() <= 1)
        {
            if (defense != null)
            {
                defense.duration = 0;
                defense = null;
            }
        }

        return base.UpdateStart();
    }
}
