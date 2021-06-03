using UnityEngine;

public enum Size
{
	Small,
	Medium,
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

	public Size Size { get => size; }

	public GameObject SmallerDebris { get => smallerDebris; }

	private Quaternion rotation;
	private float speed;

	protected override void Awake()
	{
		speed = Mathf.Lerp(rotateRange.x, rotateRange.y, Random.value);
		rotation = Quaternion.AngleAxis(45f, Random.onUnitSphere);
		Velocity = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector2(0, 1) * Mathf.Lerp(speedRange.x, speedRange.y, Random.value);

		base.Awake();
	}

	protected override void Update()
	{
		if (child != null)
			child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, child.transform.rotation * rotation, speed * Time.smoothDeltaTime);

		base.Update();
	}

	private void OnDestroy()
	{
		if (EntityManager.Instance.Entities.Contains(this))
			EntityManager.Instance.Entities.Remove(this);
	}
}
