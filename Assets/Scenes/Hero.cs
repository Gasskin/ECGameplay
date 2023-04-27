using System;
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

    private Vector3 targetPos;

    private float Speed => combatEntity.GetComponent<AttributeComponent>().MoveSpeed.Value;

    void Start()
    {
        combatEntity = MasterEntity.Instance.AddChild<CombatEntity>();
        combatEntity.ListenActionPoint(ActionPointType.AfterGiveAttack, OnAfterGiveAttackEffect);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (RayCastUtil.CastMapPoint(out targetPos))
            {
            }
        }

        if (targetPos != Vector3.zero)
        {
            var curPos = transform.position;
            var dir = (targetPos - curPos).normalized;
            var addPos = dir * Speed * Time.deltaTime;
            
            var posAfterAdd = curPos + addPos;
            var dirAfterAdd = (targetPos - posAfterAdd).normalized;

            if (Vector3.Dot(dir, dirAfterAdd) < 0) 
            {
                transform.position = targetPos;
                targetPos = Vector3.zero;
            }
            else
            {
                transform.position += addPos;
            }
        }
    }

    public void Attack()
    {
        if (combatEntity.AttackAction.TryMakeAction(out var actionExecution))
        {
            actionExecution.Target = monsterMono.combatEntity;
            actionExecution.ApplyAttack();
        }
    }


    private void OnAfterGiveAttackEffect(Entity actionExecution)
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