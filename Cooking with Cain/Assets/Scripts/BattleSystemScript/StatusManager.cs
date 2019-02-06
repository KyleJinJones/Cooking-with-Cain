using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    public EntityManager manager;

    List<GameObject>[] icons = new List<GameObject>[] { new List<GameObject>(), new List<GameObject>(), new List<GameObject>(), new List<GameObject>() };

    public List<StatusIcon> statusIcons = new List<StatusIcon>();

    Vector3 playerPosition;

    void Start()
    {
        playerPosition = manager.GetPlayer().transform.localPosition;
    }

    void Update()
    {
        List<StatusInstance> statuses = manager.GetPlayer().statuses;

        for (int i = 0; i < statuses.Count; i++)
        {
            StatusInstance status = statuses[i];

            if (icons[3].Count > i)
            {
                Image image = icons[3][i].GetComponent<Image>();
                image.sprite = statusIcons.Find(matcher => matcher.status == status.status).sprite;
                image.SetNativeSize();

                TooltipText tooltip = icons[3][i].GetComponent<TooltipText>();
                tooltip.text = GetStatusTooltip(status);
            }
            else
            {
                GameObject obj = new GameObject("Status Icon");
                Image image = obj.AddComponent<Image>();
                image.sprite = statusIcons.Find(matcher => matcher.status == status.status).sprite;
                image.SetNativeSize();

                TooltipText tooltip = obj.AddComponent<TooltipText>();
                tooltip.text = GetStatusTooltip(status);

                obj.transform.SetParent(transform);
                obj.transform.localPosition = playerPosition + new Vector3(-200, -120 + i * 80);

                icons[3].Add(obj);
            }
        }

        for (int i = statuses.Count; i < icons[3].Count; i++)
        {
            Destroy(icons[3][i]);
        }

        icons[3].RemoveAll(icon => icon == null);

        for (int j = 0; j < 3; j++)
        {
            if (manager.GetEnemies()[j] != null && manager.GetEnemies()[j].stats.health > 0)
            {
                statuses = manager.GetEnemies()[j].statuses;

                for (int i = 0; i < statuses.Count; i++)
                {
                    StatusInstance status = statuses[i];

                    if (icons[j].Count > i)
                    {
                        Image image = icons[j][i].GetComponent<Image>();
                        image.sprite = statusIcons.Find(matcher => matcher.status == status.status).sprite;
                        image.SetNativeSize();

                        TooltipText tooltip = icons[j][i].GetComponent<TooltipText>();
                        tooltip.text = GetStatusTooltip(status);
                    }
                    else
                    {
                        GameObject obj = new GameObject("Status Icon");
                        Image image = obj.AddComponent<Image>();
                        image.sprite = statusIcons.Find(matcher => matcher.status == status.status).sprite;
                        image.SetNativeSize();

                        TooltipText tooltip = obj.AddComponent<TooltipText>();
                        tooltip.text = GetStatusTooltip(status);
                        
                        obj.transform.SetParent(transform);
                        obj.transform.localPosition = manager.positions[j] + new Vector3(120, -120 + i * 80);

                        icons[j].Add(obj);
                    }
                }

                for (int i = statuses.Count; i < icons[j].Count; i++)
                {
                    Destroy(icons[j][i]);
                }

                icons[j].RemoveAll(icon => icon == null);
            }
            else
            {
                foreach (GameObject obj in icons[j])
                {
                    Destroy(obj);
                }

                icons[j].Clear();
            }
        }
    }

    string GetStatusTooltip(StatusInstance status)
    {
        switch (status.status)
        {
            case StatusInstance.Status.burn:
                return string.Format("Burn\nTake {0}% damage at the start of turn\n{1} turns remaining", Mathf.RoundToInt(status.potency * 100), status.duration);
            case StatusInstance.Status.atkup:
                return string.Format("Attack Boost\nDeal {0}% more damage\n{1} turns remaining", Mathf.RoundToInt(status.potency * 100), status.duration);
            case StatusInstance.Status.atkdown:
                return string.Format("Attack Debuff\nDeal {0}% less damage\n{1} turns remaining", Mathf.RoundToInt(status.potency * 100), status.duration);
            case StatusInstance.Status.stun:
                return string.Format("Stunned\nCannot move next turn");
            case StatusInstance.Status.defup:
                return string.Format("Defense Boost\nTake {0}% less damage\n{1} turns remaining", Mathf.RoundToInt(status.potency * 100), status.duration);
            case StatusInstance.Status.defdown:
                return string.Format("Defense Debuff\nTake {0}% more damage\n{1} turns remaining", Mathf.RoundToInt(status.potency * 100), status.duration);
            case StatusInstance.Status.reflect:
                return string.Format("Reflect\nDeal {0}% of damage taken back to the attacker\n{1} turns remaining", Mathf.RoundToInt(status.potency * 100), status.duration);
            case StatusInstance.Status.cleanse:
                return string.Format("Debuff Cleanse\nImmune to debuffs\n{0} turns remaining", status.duration);
        }
        return null;
    }
}

[System.Serializable]
public class StatusIcon
{
    public StatusInstance.Status status;
    public Sprite sprite;
}