using DilmerGames.Core.Singletons;

public class ApplicationState : Singleton<ApplicationState>
{
    public State CurrentState { get; set; } = State.Idle;

}