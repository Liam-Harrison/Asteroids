using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class HighscoreUI : MonoBehaviour
{
	private const int MAX_ROWS = 6;

	[SerializeField]
	private GameObject rowPrefab;

	[SerializeField]
	private TextMeshProUGUI gameoverContinue;

	private List<HighscoreRow> rows = new List<HighscoreRow>();

	public static bool UserInputting { get; private set; } = false;

	private void OnEnable()
	{
		ParseScores();
		PlacePlayerRow();
	}

	private void PlacePlayerRow()
	{
		var score = GameStateManager.Instance.Points;

		gameoverContinue.text = "Press Space to continue";
		UserInputting = false;

		for (int i = 0; i < rows.Count; i++)
		{
			var row = rows[i];
			if (score > int.Parse(row.Score.text))
			{
				Destroy(rows[rows.Count - 1].gameObject);
				rows.RemoveAt(rows.Count - 1);

				var user = CreateRow();

				user.Index.text = $"{i + 1:0}.";
				user.Score.text = $"{score:000000}";

				user.transform.SetSiblingIndex(row.transform.GetSiblingIndex());
				user.OnUserInputted += WriteHighscores;
				user.ReadUserHighscore();
				UserInputting = true;
				gameoverContinue.text = "Press Enter to confirm name";

				break;
			}
		}
	}

	private void WriteHighscores()
	{
		var path = Path.Combine(Application.persistentDataPath, "highscores.txt");

		var highscores = new List<string>();
		for (int i = 0; i < rows.Count && i < MAX_ROWS; i++)
		{
			var row = rows[i];
			highscores.Add($"{row.Username.text},{row.Score.text}");
		}

		File.WriteAllLines(path, highscores);
		UserInputting = false;
		gameoverContinue.text = "Press Space to continue";
	}

	private void ParseScores()
	{
		ClearRows();
		var path = Path.Combine(Application.persistentDataPath, "highscores.txt");

		if (!File.Exists(path))
		{
			using var writter = File.CreateText(path);

			writter.WriteLine($"AAA,8000");
			writter.WriteLine($"BBB,6500");
			writter.WriteLine($"CCC,5000");
			writter.WriteLine($"DDD,3500");
			writter.WriteLine($"EEE,2000");
			writter.WriteLine($"FFF,500");
		}

		var lines = File.ReadAllLines(path);
		for (int i = 0; i < lines.Length && i < MAX_ROWS; i++)
		{
			var line = lines[i];

			var split = line.Split(',');

			if (split.Length != 2)
				continue;

			var name = split[0];
			var score = split[1];

			if (int.TryParse(score, out int result))
			{
				var row = CreateRow();
				row.Username.text = name;
				row.Score.text = $"{result:000000}";
			}
		}

		rows.Sort((a, b) => int.Parse(b.Score.text).CompareTo(int.Parse(a.Score.text)));

		for (int i = 0; i < rows.Count; i++)
		{
			rows[i].Index.text = $"{i + 1:0}.";
			rows[i].transform.SetAsLastSibling();
		}
	}

	private HighscoreRow CreateRow()
	{
		var row = Instantiate(rowPrefab, transform).GetComponent<HighscoreRow>();
		rows.Add(row);
		return row;
	}

	private void ClearRows()
	{
		for (int i = 0; i < rows.Count; i++)
		{
			Destroy(rows[i].gameObject);
			rows.RemoveAt(i--);
		}
	}
}
