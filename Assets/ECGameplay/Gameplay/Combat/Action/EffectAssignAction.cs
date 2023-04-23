// namespace ECGameplay
// {
//     public class EffectAssignAction : Entity,IAction
//     {
//         public CombatEntity OwnerEntity
//         {
//             get => GetParent<CombatEntity>();
//             set { }
//         }
//         
//         public bool Enable { get; set; }
//
//         public bool TryMakeAction(out EffectAssignActionExecution action)
//         {
//             if (!Enable)
//             {
//                 action = null;
//             }
//             else
//             {
//                 action = OwnerEntity.AddChild<EffectAssignActionExecution>();
//                 action.Action = this;
//                 action.Creator = OwnerEntity;
//             }
//             return Enable;
//         }
//     }
//     
//
//     public class EffectAssignActionExecution : Entity,IActionExecution
//     {
//         public IAction Action { get; set; }
//         public EffectAssignActionExecution EffectActionExecution { get; set; }
//
//         public CombatEntity Creator { get; set; }
//         
//         public CombatEntity Target { get; set; }
//         /// 释放目标，不一定是CombatEntity，效果也可以对其他效果释放
//         public Entity AssignTarget { get; set; }
//
//         /// 这个行为来自哪个能力        
//         public IAbility OwnerAbility { get; set; }
//         /// 这个行为的能力效果
//         public AbilityEffect AbilityEffect { get; set; }
//
//
//         public void AssignEffect()
//         {
//             // 可能是对其他战斗单位释放的，也可能不是
//             if (AssignTarget is CombatEntity combatEntity)
//             {
//                 Target = combatEntity;
//             }
//             
//             foreach (var comp in AbilityEffect.Components.Values)
//             {
//                 if (comp is IEffectAssignComponent trigger)
//                 {
//                     trigger.OnApplyEffect(this);
//                 }
//             }
//
//             Creator?.TriggerActionPoint(ActionPointType.AssignEffect, this);
//             Target?.TriggerActionPoint(ActionPointType.ReceiveEffect, this);
//             
//             FinishAction();
//         }
//         
//         public void FinishAction()
//         {
//             Destroy(this);
//         }
//     }
// }