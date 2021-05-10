using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
	private const float DEBRIS_SCATTER_ANGLE = 60;

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

	public void DestroyAllDebris()
	{
		foreach (var entity in Entities.ToArray())
		{
			Destroy(entity);
			Entities.Remove(entity);
		}
	}

	public void DestroyAllBullets()
	{
		foreach (var bullet in Bullets.ToArray())
		{
			Destroy(bullet);
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
