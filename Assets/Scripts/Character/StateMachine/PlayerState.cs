
public abstract class PlayerState
{
    public abstract void Update(PlayerStateController state, PlayerController player);
    public abstract void FixedUpdate(PlayerStateController state, PlayerController player);
    public abstract void EnterState(PlayerStateController state, PlayerController player);
    public abstract void ExitState(PlayerStateController state, PlayerController player);
}
