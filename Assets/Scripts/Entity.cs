using UnityEngine;

public class Entity : MonoBehaviour
{
	[SerializeField]
	protected Transform child;

	[SerializeField]
	protected GameObject deathParticle;

	[SerializeField]
	protected AudioClip hitNoise;

	[SerializeField]
	protected AudioClip deathNoise;

	public Vector2 Velocity { get; protected set; }

	protected virtual void Update()
	{
		transform.RotateAround(Vector3.zero, transform.up, Velocity.x * Time.smoothDeltaTime);
		transform.RotateAround(Vector3.zero, transform.right, Velocity.y * Time.smoothDeltaTime);
	}

	private void OnDestroy()
	{
		if (deathNoise != null)
		{

		}

		if (deathParticle != null)
		{
			var go = Instantiate(deathParticle);
			go.transform.position = transform.position;
			go.transform.rotation = transform.rotation;
		}
	}
}
