using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
	[SerializeField]
	private Transform target;

	[SerializeField]
	private float distance;

	[SerializeField]
	private float time;

	private Vector3 vel;

	Vector3 offset;
	Quaternion rotation;

	private void Awake()
	{
		offset = target.position - transform.position;
		rotation = transform.rotation;
	}

	private void Update()
	{
		var pos = target.position.normalized * -distance;

		transform.position = Vector3.SmoothDamp(transform.position, pos, ref vel, time);

		transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized, rotation * target.transform.up);
	}
}
