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

	[SerializeField]
	Vector3 rotationEuler;

	[SerializeField]
	Vector3 lookEuler;

	[SerializeField]
	float rot;

	private void Update()
	{
		var pos = target.position;

		var dif = Quaternion.AngleAxis(rot, target.right) * new Vector3(0, 0, -distance);
		pos += dif;

		transform.position = Vector3.SmoothDamp(transform.position, pos, ref vel, time);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((target.position - transform.position).normalized, target.transform.up), 90 * Time.smoothDeltaTime);
	}
}
