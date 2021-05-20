using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
	private const float DEBRIS_SCATTER_ANGLE = 60;

	private const float MIN_ENTITIES = 5;

	private const float MAX_ENTITIES = 20;

	private const float ENTITY_RESPAWN_TIME = 2;

	[SerializeField]
	private GameObject[] debris;

	[SerializeField]
	private GameObject bullet;

	public List<DebrisEntity> Entities { get; private set; } = new List<DebrisEntity>();

	public List<Bullet> Bullets { get; private set; } = new List<Bullet>();

	private int index;

	public void DestroyAllEntities()
	{
		DestroyAllDebris();
		DestroyAllBullets();
	}

	private float lastSpawnTime;

	private void Update()
	{
		if (Entities.Count < MIN_ENTITIES)
		{
			for (int i = Entities.Count; i < MIN_ENTITIES; i++)
			{
				SpawnDebris();
			}
		}
		else if (Time.time - lastSpawnTime > ENTITY_RESPAWN_TIME && Entities.Count < MAX_ENTITIES)
		{
			lastSpawnTime = Time.time;
			SpawnDebris();
		}
	}

	public void DestroyAllDebris()
	{
		foreach (var entity in Entities.ToArray())
		{
			Destroy(entity.gameObject);
			Entities.Remove(entity);
		}
	}

	public void DestroyAllBullets()
	{
		foreach (var bullet in Bullets.ToArray())
		{
			Destroy(bullet.gameObject);
			Bullets.Remove(bullet);
		}
	}

	public void SpawnDebris()
	{
		SpawnDebris(debris[index++], (Quaternion.AngleAxis(Random.value * DEBRIS_SCATTER_ANGLE, Random.onUnitSphere) * -PlayerEntity.Instance.transform.position).normalized * 250f);

		if (index == debris.Length)
			index = 0;
	}

	public void SpawnDebris(GameObject debris, Vector3 position)
	{
		var go = Instantiate(debris, transform);

		go.transform.position = position;

		var up = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.forward;
		var fwd = -go.transform.position.normalized;

		go.transform.rotation = Quaternion.LookRotation(fwd, up);

		Entities.Add(go.GetComponent<DebrisEntity>());
	}

	public void SpawnBullet(Vector3 position, Vector3 right)
	{
		var go = Instantiate(bullet, transform);

		go.transform.position = position.normalized * 250f;
		go.transform.rotation = Quaternion.LookRotation(-go.transform.position.normalized, Vector3.Cross(-go.transform.position.normalized, right));

		var b = go.GetComponent<Bullet>();
		b.start = go.transform.rotation;
		Bullets.Add(b);
	}
}
