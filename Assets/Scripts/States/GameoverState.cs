using UnityEngine;

public class GameoverState : GameState
{
	public override void OnEnterState()
	{
		UIManager.Instance.FinalScore.text = $"Final Score:{GameStateManager.Instance.Points:000000}";
		PlayerEntity.Instance.HidePlayer();
	}

	public override void OnLeaveState()
	{

	}

	public override void OnStateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameStateManager.Instance.SetState(State.Menu);
		}
	}
}
