public abstract class BaseState
{
    public NPCs npc;
    public StateMachine stateMachine;

    public abstract void Enter();

    public abstract void Perform();

    public abstract void Exit();

}