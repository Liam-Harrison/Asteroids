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

	[SerializeField]
	private bool gradient = true;

	private TextMeshProUGUI text;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		if (gradient)
		{
			var color = text.colorGradient;

			var t = Mathf.Sin(Time.time / time) / 2 + 0.5f;
			color.bottomLeft = Color.Lerp(a, b, t);

			text.colorGradient = color;
		}
		else
		{
			var t = Mathf.Sin(Time.time / time) / 2 + 0.5f;
			var alpha = Mathf.Lerp(a.a, b.a, t);
			var color = Color.Lerp(a, b, t);
			color.a = alpha;
			text.color = color;
		}
	}
}
