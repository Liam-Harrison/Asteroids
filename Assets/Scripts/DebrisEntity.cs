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
	private GameObject smallerDebris;

	[SerializeField]
	private Vector2 speedRange = new Vector2(20, 30);

	[SerializeField]
	private Vector2 rotateRange = new Vector2(20, 30);

	private Quaternion rotation;
	private float speed;

	private void Awake()
	{
		speed = Mathf.Lerp(rotateRange.x, rotateRange.y, Random.value);
		rotation = Quaternion.AngleAxis(45f, Random.onUnitSphere);
		Velocity = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector2(0, 1) * Mathf.Lerp(speedRange.x, speedRange.y, Random.value);
	}

	protected override void Update()
	{
		if (child != null)
			child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, child.transform.rotation * rotation, speed * Time.smoothDeltaTime);

		base.Update();
	}
}
