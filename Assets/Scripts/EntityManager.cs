using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
	private const float DEBRIS_SCATTER_ANGLE = 60;

	[SerializeField]
	private GameObject[] debris;

	public List<DebrisEntity> Entities { get; private set; } = new List<DebrisEntity>();

	private int index;

	public void DestroyAllDebris()
	{
		foreach (var entity in Entities.ToArray())
		{
			Destroy(entity);
			Entities.Remove(entity);
		}
	}

	public void SpawnDebris()
	{
		var go = Instantiate(debris[index++], transform);

		if (index == debris.Length)
			index = 0;

		go.transform.position = (Quaternion.AngleAxis(Random.value * DEBRIS_SCATTER_ANGLE, Random.onUnitSphere) * -PlayerEntity.Instance.transform.position).normalized * 250f;

		var up = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.forward;
		var fwd = -go.transform.position.normalized;

		go.transform.rotation = Quaternion.LookRotation(fwd, up);

		Entities.Add(go.GetComponent<DebrisEntity>());
	}
}
