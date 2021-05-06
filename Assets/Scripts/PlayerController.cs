using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private float change;

	[SerializeField]
	private Transform child;

	private new Camera camera;

	private Vector3 pos;

	private float x, y, velx, vely;

	private void Awake()
    {
		camera = Camera.main;
    }

	private void Update()
	{
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		var plane = new Plane(-transform.forward, transform.position);
		if (plane.Raycast(ray, out float dist))
		{
			pos = ray.GetPoint(dist);
		}

		var input = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;

		x = Mathf.SmoothDamp(x, input.x, ref velx, change);
		y = Mathf.SmoothDamp(y, input.y, ref vely, change);

		transform.RotateAround(Vector3.zero, transform.up, x * Time.smoothDeltaTime);
		transform.RotateAround(Vector3.zero, transform.right, y * Time.smoothDeltaTime);

		child.rotation = Quaternion.LookRotation(-transform.position.normalized, (pos - transform.position).normalized);
    }
}
