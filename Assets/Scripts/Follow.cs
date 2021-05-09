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

	private void Update()
	{
		var offset = target.TransformDirection(Quaternion.Euler(rotationEuler) * Vector3.forward);

		var pos = target.position;
		pos += offset * -distance;

		transform.position = Vector3.SmoothDamp(transform.position, pos, ref vel, time);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((target.position - transform.position).normalized, target.transform.up), 90 * Time.smoothDeltaTime);
	}
}
