using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	Menu,
	Playing,
	Gameover
}

public class GameStateManager : Singleton<GameStateManager>
{
	public State State { get; private set; } = State.Menu;

	public static event Action<State> OnStateChanged = null;

	public int Points { get; set; }

	public int Lives { get; set; }

	private GameState gameState = null;

	private void Start()
	{
		SetState(State.Menu);
	}

	public void SetState(State state)
	{
		if (State != state)
		{
			State = state;
			OnStateChanged?.Invoke(State);
		}

		if (gameState != null)
			gameState.OnLeaveState();

		switch (State)
		{
			case State.Menu:
				gameState = Activator.CreateInstance<MenuState>();
				break;
			case State.Playing:
				gameState = Activator.CreateInstance<PlayState>();
				break;
			case State.Gameover:
				gameState = Activator.CreateInstance<GameoverState>();
				break;
		}

		gameState.OnEnterState();
	}

	private void Update()
	{
		if (gameState != null)
			gameState.OnStateUpdate();
	}
}
