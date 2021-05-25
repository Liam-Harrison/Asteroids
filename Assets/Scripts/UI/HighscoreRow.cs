using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighscoreRow : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI index;

	[SerializeField]
	private TMP_InputField username;

	[SerializeField]
	private TextMeshProUGUI score;

	public TextMeshProUGUI Index { get => index; }

	public TMP_InputField Username { get => username; }

	public TextMeshProUGUI Score { get => score; }

	public bool UserInputting { get; private set; } = false;

	public event Action OnUserInputted;

	public void ReadUserHighscore()
	{
		Username.interactable = true;
		EventSystem.current.SetSelectedGameObject(Username.gameObject, null);
		Username.ActivateInputField();

		Username.text = "";
		UserInputting = true;
	}

	public void StopUserInput()
	{
		Username.DeactivateInputField();
		Username.interactable = false;
		UserInputting = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) && Username.text != "")
		{
			StopUserInput();
			OnUserInputted?.Invoke();
		}
	}
}
