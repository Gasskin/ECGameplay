using System.Collections.Generic;
using ECGameplay;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public CombatEntity combatEntity;

    public Hero heroMono;
    public Image healthBar;
    public Text valueText;

    private void Start()
    {
        combatEntity = MasterEntity.Instance.AddChild<CombatEntity>();
        combatEntity.ListenActionPoint(ActionPointType.AfterReceiveDamage, OnReceiveDamage);
    }

    private void OnReceiveDamage(Entity actionExecution)
    {
        var damageActionExecution = actionExecution.As<DamageActionExecution>();
        if (damageActionExecution == null) 
            return;
        var attr = combatEntity.GetComponent<AttributeComponent>();
        var hpPct = attr.HealthPoint.Value / attr.HealthPointMax.Value;
        healthBar.fillAmount = hpPct;
        var damageText = Instantiate(valueText, valueText.transform.parent, false);
        damageText.gameObject.SetActive(true);
        damageText.text = $"-{damageActionExecution.Damage}";
        damageText.color = Color.red;
        Destroy(damageText.gameObject, 0.2f);
    }
}