using UnityEngine;

public class Bullet : Entity
{
	private const float DEBRIS_INVINCIBLE_TIME = 0.15f;

	[SerializeField]
	private float speed = 20;

	public Quaternion start;

	protected override void Awake()
	{
		Velocity = new Vector2(0, speed);
		base.Awake();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Debris" || gameObject == null)
		{
			var debris = collision.gameObject.GetComponentInParent<DebrisEntity>();

			if (debris == null)
				return;

			if (Time.time <= debris.Spawned + DEBRIS_INVINCIBLE_TIME)
				return;

			if (debris.Size == Size.Large)
				GameStateManager.Instance.Points += 20;

			if (debris.Size == Size.Medium)
				GameStateManager.Instance.Points += 50;

			if (debris.Size == Size.Small)
				GameStateManager.Instance.Points += 100;

			if (debris.SmallerDebris != null)
			{
				EntityManager.Instance.SpawnDebris(debris.SmallerDebris, debris.transform.position.normalized * 250f);
				EntityManager.Instance.SpawnDebris(debris.SmallerDebris, debris.transform.position.normalized * 250f);
			}

			debris.OnEntityDestroyed();
			Destroy(debris.gameObject);
			Destroy(gameObject);
		}
	}

	protected override void Update()
	{
		if (this != null && Mathf.Abs(180 - Quaternion.Angle(PlayerEntity.Instance.transform.rotation, transform.rotation)) < 2f && Time.time >= Spawned + 1f)
		{
			Destroy(gameObject);
		}

		base.Update();
	}

	private void OnDestroy()
	{
		if (EntityManager.Instance.Bullets.Contains(this))
			EntityManager.Instance.Bullets.Remove(this);
	}
}
