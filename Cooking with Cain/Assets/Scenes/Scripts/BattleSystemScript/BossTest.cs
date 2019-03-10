﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : Entity
{
    public Enemy pea;
    public Sprite defUpIcon;

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

    public override void OnUpdate()
    {
        manager.enemyRemaining.text = "Enemy Remaining: ??";
    }

    public override void UpdateStartPlayerTurn()
    {
        if (manager.GetEnemyRemaining() > 1)
        {
            defense = AddStatus(StatusInstance.Status.defup, 0.5f, 100);
            defense.customSprite = defUpIcon;
            defense.customMessage = "Damage taken reduced by half until all peas are defeated";
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
                defense.customSprite = defUpIcon;
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
