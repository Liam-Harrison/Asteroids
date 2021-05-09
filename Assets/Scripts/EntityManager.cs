using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
	[SerializeField]
	private GameObject[] debris;

	public List<DebrisEntity> Entities { get; private set; } = new List<DebrisEntity>();

	private int index;

	private void Start()
	{
		SpawnDebris();
	}

	private void SpawnDebris()
	{
		var go = Instantiate(debris[index], transform);
		index = (int)Mathf.Repeat(++index, debris.Length - 1);

		go.transform.position = -PlayerEntity.Instance.transform.position.normalized * 250f;

		var up = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.forward;
		var fwd = -go.transform.position.normalized;

		go.transform.rotation = Quaternion.LookRotation(fwd, up);

		Entities.Add(go.GetComponent<DebrisEntity>());
	}
}
