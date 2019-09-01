using System.Threading.Tasks;

public abstract class Behaviour
{
	public readonly string name;
	protected readonly BehaviourHandler handler;

	public Behaviour(string name, BehaviourHandler handler)
	{
		this.name = name;
		this.handler = handler;
	}

	public void Start()
	{
		OnStart();
	}

	protected abstract void OnStart();

	public void Update()
	{
		OnUpdate();
	}

	protected abstract void OnUpdate();

	public void End()
	{
		OnEnd();
	}

	protected abstract void OnEnd();

	protected async void ShiftBehaviourAfterWait(string name, float duration)
	{
		await Task.Delay(System.TimeSpan.FromSeconds(duration));

		handler.ShiftBehaviour(name);
	}
}
