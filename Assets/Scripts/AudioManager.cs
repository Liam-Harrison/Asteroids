using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
	[SerializeField]
	AudioClip[] music;

	public AudioSource AudioSource { get; private set; }

	private int nextClip = 1;

	protected override void Awake()
	{
		AudioSource = GetComponent<AudioSource>();

		AudioSource.clip = music[0];
		AudioSource.Play();

		base.Awake();
	}

	private void LateUpdate()
	{
		if (!AudioSource.isPlaying)
		{
			if (nextClip >= music.Length)
				nextClip = 0;

			AudioSource.clip = music[nextClip++];
			AudioSource.Play();
		}
	}
}
