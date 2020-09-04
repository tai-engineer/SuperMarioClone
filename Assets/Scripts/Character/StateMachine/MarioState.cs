
public abstract class MarioState
{
    public abstract void Update(MarioStateController state, MarioController player);
    public abstract void FixedUpdate(MarioStateController state, MarioController player);
    public abstract void EnterState(MarioStateController state, MarioController player);
    public abstract void ExitState(MarioStateController state, MarioController player);
}
