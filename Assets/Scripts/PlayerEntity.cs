using UnityEngine;

public class PlayerEntity : Entity
{
	[SerializeField]
	private float change;

	[SerializeField]
	private float speed;

	private new Camera camera;

	private float x, y, velx, vely;

	private Vector3 target;

	public static PlayerEntity Instance;

	private void Awake()
    {
		Instance = this;
		camera = Camera.main;
    }

	protected override void Update()
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

		base.Update();
    }
}
