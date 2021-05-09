using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Size
{
	Small,
	Large
}

public class DebrisEntity : Entity
{
	[SerializeField]
	private Size size;

	[SerializeField]
	private Vector2 speedRange = new Vector2(20, 30);

	[SerializeField]
	private Vector2 rotateRange = new Vector2(20, 30);

	private Quaternion rotation;

	private void Awake()
	{
		rotation = Quaternion.AngleAxis(Mathf.Lerp(rotateRange.x, rotateRange.y, Random.value), Random.onUnitSphere);
		Velocity = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector2(0, 1) * Mathf.Lerp(speedRange.x, speedRange.y, Random.value);
	}

	protected override void Update()
	{
		if (child != null)
			child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, child.transform.rotation * rotation, 180 * Time.smoothDeltaTime);

		base.Update();
	}
}
