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

	public Vector2 Velocity { get; set; }

	public float Spawned { get; private set; }

	protected AudioSource source;

	protected virtual void Awake()
	{
		Spawned = Time.time;

		source = GetComponent<AudioSource>();
		if (source == null) source = gameObject.AddComponent<AudioSource>();
	}

	protected virtual void Update()
	{
		transform.RotateAround(Vector3.zero, transform.up, Velocity.x * Time.smoothDeltaTime);
		transform.RotateAround(Vector3.zero, transform.right, Velocity.y * Time.smoothDeltaTime);
	}

	public virtual void OnEntityDestroyed()
	{
		if (deathNoise != null)
		{
			var go = new GameObject();
			go.transform.position = transform.position;
			go.transform.rotation = transform.rotation;
			var audio = go.AddComponent<AudioSource>();
			audio.PlayOneShot(deathNoise);
			audio.volume = 0.2f;
			Destroy(go, deathNoise.length);
		}

		if (deathParticle != null)
		{
			var go = Instantiate(deathParticle);
			go.transform.position = transform.position;
			go.transform.rotation = transform.rotation;
			Destroy(go, 1f);
		}
	}
}
