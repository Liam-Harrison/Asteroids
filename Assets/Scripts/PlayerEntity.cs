using System.Collections;
using UnityEngine;

public class PlayerEntity : Entity
{
	private const float FIRE_RATE = 1f / 3f;

	private const float INVUNERABLE_TIME = 3;

	[SerializeField]
	private AudioClip[] fireClips;

	[SerializeField]
	private float change;

	[SerializeField]
	private float speed;

	[SerializeField]
	private GameObject invincibleEffect;

	[SerializeField]
	private GameObject invincibleText;

	public bool Locked { get; private set; } = true;

	public bool IsAlive { get => child.gameObject.activeSelf; }

	public static PlayerEntity Instance;

	private Vector3 target;
	private new Camera camera;

	private float x, y, velx, vely;
	private float lastFired = 0;
	private float lastHit = 0;

	protected override void Awake()
	{
		Instance = this;
		camera = Camera.main;
		child.gameObject.SetActive(false);
		Velocity = (Random.onUnitSphere * Vector2.up).normalized * 2;

		base.Awake();
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
		lastHit = Time.time;
	}

	public void HidePlayer()
	{
		Locked = true;
		child.gameObject.SetActive(false);
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

	private int nextClip = 0;

	private void HandlePlayerFiring()
	{
		if (Input.GetMouseButton(0) && Time.time >= lastFired + FIRE_RATE)
		{
			lastFired = Time.time;
			EntityManager.Instance.SpawnBullet(child.position + (child.forward * 10), child.right);

			if (nextClip >= fireClips.Length)
				nextClip = 0;

			source.PlayOneShot(fireClips[nextClip++]);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Debris" && Time.time >= lastHit + INVUNERABLE_TIME)
		{
			lastHit = Time.time;
			GameStateManager.Instance.Lives = Mathf.Max(GameStateManager.Instance.Lives - 1, 0);
			if (GameStateManager.Instance.Lives > 0)
				StartCoroutine(PlayInvincibleEffect());

			if (hitNoise != null)
				source.PlayOneShot(hitNoise);
		}
	}

	private IEnumerator PlayInvincibleEffect()
	{
		invincibleEffect.SetActive(true);
		invincibleText.SetActive(true);

		float started = Time.time;

		while (Time.time <= started + INVUNERABLE_TIME)
			yield return null;

		invincibleEffect.SetActive(false);
		invincibleText.SetActive(false);
		yield break;
	}
}
