using UnityEngine;

public class PlayerEntity : Entity
{
	private const float FIRE_RATE = 1f / 3f;

	[SerializeField]
	private float change;

	[SerializeField]
	private float speed;

	public bool Locked { get; private set; } = true;

	public bool IsAlive { get => child.gameObject.activeSelf; }

	private new Camera camera;

	private float x, y, velx, vely;

	private Vector3 target;

	private float lastFired = 0;

	public static PlayerEntity Instance;

	private void Awake()
    {
		Instance = this;
		camera = Camera.main;
		child.gameObject.SetActive(false);
    }

	protected override void Update()
	{
		if (!Locked)
		{
			HandlePlayerLocomotion();
			HandlePlayerFiring();
		}

		base.Update();
    }

	public void SpawnPlayer()
	{
		child.rotation = Quaternion.LookRotation(transform.up, transform.position.normalized);
		child.gameObject.SetActive(true);
		Locked = false;
	}

	private void HandlePlayerLocomotion()
	{
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		var plane = new Plane(-transform.forward, transform.position);

		if (plane.Raycast(ray, out float dist))
			target = ray.GetPoint(dist);
		else
			target = Vector3.zero;

		var input = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;

		x = Mathf.SmoothDamp(x, input.x, ref velx, change);
		y = Mathf.SmoothDamp(y, input.y, ref vely, change);

		Velocity = new Vector2(x, y);

		if (child != null)
			child.rotation = Quaternion.RotateTowards(child.rotation, Quaternion.LookRotation((target - transform.position).normalized, transform.position.normalized), 720 * Time.smoothDeltaTime);
	}

	private void HandlePlayerFiring()
	{
		if (Input.GetMouseButton(0) && Time.time >= lastFired + FIRE_RATE)
		{
			lastFired = Time.time;
			EntityManager.Instance.SpawnBullet(child.position + child.forward, child.right);
		}
	}
}
