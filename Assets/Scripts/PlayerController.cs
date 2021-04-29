using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private float change;

	private new Camera camera;

	private float x, y, velx, vely;
	private Vector3 pos;

	private void Awake()
    {
		camera = Camera.main;
    }

	private void Update()
	{
		pos = camera.ScreenToWorldPoint(Input.mousePosition);

		var input = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed * Time.smoothDeltaTime;

		x = Mathf.SmoothDamp(x, x + input.y, ref velx, change);
		y = Mathf.SmoothDamp(y, y + input.x, ref vely, change);

        transform.RotateAround(Vector3.zero, transform.up, input.x);
        transform.RotateAround(Vector3.zero, transform.right, input.y);
    }

	private void OnDrawGizmos()
    {
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, pos);
    }
}
