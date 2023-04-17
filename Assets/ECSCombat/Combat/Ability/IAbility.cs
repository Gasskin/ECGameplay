using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility 
{
    /// 创建能力执行体
    public BaseAbilityExecution CreateExecution(BaseActionExecution baseActionExecution);
}
