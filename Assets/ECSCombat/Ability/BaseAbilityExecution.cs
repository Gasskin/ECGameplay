
public enum AbilityExecutionProcess
{
    None,
    Begin,
    Execute,
    End,
}

public abstract class BaseAbilityExecution
{
    private AbilityExecutionProcess process = AbilityExecutionProcess.None;
    
    public BaseActionExecution actionExecution;
    public bool IsEnd => process == AbilityExecutionProcess.End;
    
    public void Update()
    {
        switch (process)
        {
            case AbilityExecutionProcess.None:
                break;
            case AbilityExecutionProcess.Begin:
                process = AbilityExecutionProcess.Execute;
                OnExecute();
                break;
            case AbilityExecutionProcess.Execute:
                process = AbilityExecutionProcess.End;
                OnEndExecute();
                break;
            case AbilityExecutionProcess.End:
                break;
        }
    }

    /// 开始执行
    public void BeginExecute()
    {
        OnBeginExecute();
        process = AbilityExecutionProcess.Begin;
    }

    /// 开始
    public abstract void OnBeginExecute();
    
    /// 进行中 
    public abstract void OnExecute();

    /// 结束
    public abstract void OnEndExecute();
}
