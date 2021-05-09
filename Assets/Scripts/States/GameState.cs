using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
	public abstract void OnEnterState();

	public abstract void OnStateUpdate();

	public abstract void OnLeaveState();
}
