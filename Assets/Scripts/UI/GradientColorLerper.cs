using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GradientColorLerper : MonoBehaviour
{
	[SerializeField]
	private Color a;

	[SerializeField]
	private Color b;

	[SerializeField]
	private float time;

	private TextMeshProUGUI text;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		var color = text.colorGradient;

		var t = Mathf.Sin(Time.time / time) / 2 + 0.5f;
		color.bottomLeft = Color.Lerp(a, b, t);

		text.colorGradient = color;
	}
}
