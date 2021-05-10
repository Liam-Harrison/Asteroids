using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameState
{
	public override void OnEnterState()
	{
		EntityManager.Instance.DestroyAllDebris();

		for (int i = 0; i < 10; i++)
			EntityManager.Instance.SpawnDebris();
	}

	public override void OnLeaveState()
	{

	}

	public override void OnStateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameStateManager.Instance.SetState(State.Playing);
		}
	}
}
