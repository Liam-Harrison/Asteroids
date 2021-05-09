using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	[SerializeField, Header("Assignments")]
	private GameObject menu;

	[SerializeField]
	private GameObject playing;

	[SerializeField]
	private GameObject gameover;

	protected override void Awake()
	{
		GameStateManager.OnStateChanged += OnGameStateChanged;

		base.Awake();
	}

	private void OnGameStateChanged(State state)
	{
		menu.SetActive(state == State.Menu);
		playing.SetActive(state == State.Playing);
		gameover.SetActive(state == State.Gameover);
	}
}
