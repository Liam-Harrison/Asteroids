using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
	[SerializeField, Header("Assignments")]
	private GameObject menu;

	[SerializeField]
	private GameObject playing;

	[SerializeField]
	private GameObject gameover;

	[SerializeField, Header("Labels")]
	private TextMeshProUGUI score;

	[SerializeField]
	private TextMeshProUGUI lives;

	[SerializeField]
	private TextMeshProUGUI finalScore;

	public TextMeshProUGUI FinalScore { get => finalScore; }

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

	private void Update()
	{
		score.text = $"Score:{GameStateManager.Instance.Points:000000}";
		lives.text = $"Lives:{GameStateManager.Instance.Lives:00}";
	}
}
