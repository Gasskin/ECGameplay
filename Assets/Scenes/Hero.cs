using ECGameplay;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public CombatEntity combatEntity;

    public Monster monsterMono;
    public GameObject lineEffectPrefab;
    public GameObject hitEffectPrefab;
    public Image healthBar;
    public Text valueText;

    void Start()
    {
        combatEntity = MasterEntity.Instance.AddChild<CombatEntity>();
        combatEntity.ListenActionPoint(ActionPointType.BeforeGiveAttackEffect, OnBeforeGiveAttackEffect);
    }

    public void Attack()
    {
        if (combatEntity.AttackAction.TryMakeAction(out var actionExecution))
        {
            actionExecution.Target = monsterMono.combatEntity;
            actionExecution.ApplyAttack();
        }
    }


    private void OnBeforeGiveAttackEffect(Entity actionExecution)
    {
        SpawnLineEffect(transform.position, monsterMono.transform.position);
        SpawnHitEffect(transform.position, monsterMono.transform.position);
    }


    private void SpawnLineEffect(Vector3 p1, Vector3 p2)
    {
        var attackEffect = Instantiate(lineEffectPrefab);
        attackEffect.transform.position = Vector3.zero;
        attackEffect.GetComponent<LineRenderer>().SetPosition(0, p1);
        attackEffect.GetComponent<LineRenderer>().SetPosition(1, p2);
        Destroy(attackEffect, 0.05f);
    }
    
    private void SpawnHitEffect(Vector3 p1, Vector3 p2)
    {
        var vec = p1 - p2;
        var hitPoint = p2 + vec.normalized * .6f;
        hitPoint += Vector3.up;
        var hitEffect = Instantiate(hitEffectPrefab);
        hitEffect.transform.position = hitPoint;
        GameObject.Destroy(hitEffect, 0.2f);
    }
}