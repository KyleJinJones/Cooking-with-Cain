using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : Entity
{
    public Enemy pea;
    public Sprite defUpIcon;
    public Sprite reflectIcon;

    StatusInstance defense = null;
    StatusInstance reflect = null;
    int cooldown = 0;

    void Start()
    {
        TooltipText tooltip = gameObject.AddComponent<TooltipText>();
        tooltip.text = "Buffel Sprout";
    }

    public override void UpdateStartPlayerTurn()
    {
        if (manager.GetEnemyRemaining() > 1)
        {
            defense = AddStatus(StatusInstance.Status.defup, 0.8f, 100);
            defense.customSprite = defUpIcon;
            defense.customMessage = "Damage taken reduced by 80% until all peas are defeated";

            reflect = AddStatus(StatusInstance.Status.reflect, 0.3f, 100);
            reflect.customSprite = reflectIcon;
            reflect.customMessage = "30% of damage dealt will be reflected back until all peas are defeated";
        }
        else
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                defense = AddStatus(StatusInstance.Status.defup, 0.8f, 100);
                defense.customSprite = defUpIcon;
                defense.customMessage = "Damage taken reduced by 80% until all peas are defeated";

                reflect = AddStatus(StatusInstance.Status.reflect, 0.3f, 100);
                reflect.customSprite = reflectIcon;
                reflect.customMessage = "30% of damage dealt will be reflected back until all peas are defeated";

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

                reflect.duration = 0;
                reflect = null;
            }
        }

        return base.UpdateStart();
    }
}
