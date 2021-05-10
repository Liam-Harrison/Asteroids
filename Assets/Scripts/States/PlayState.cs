using UnityEngine;

public class PlayState : GameState
{
	public override void OnEnterState()
	{
		PlayerEntity.Instance.SpawnPlayer();

		GameStateManager.Instance.Points = 0;
		GameStateManager.Instance.Lives = 3;
	}

	public override void OnLeaveState()
	{

	}

	public override void OnStateUpdate()
	{
		if (GameStateManager.Instance.Lives == 0)
		{
			GameStateManager.Instance.SetState(State.Gameover);
		}
	}
}
