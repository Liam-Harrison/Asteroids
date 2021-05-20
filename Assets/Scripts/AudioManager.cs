using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
	[SerializeField]
	AudioClip[] music;

	public AudioSource AudioSource { get; private set; }

	protected override void Awake()
	{
		AudioSource = GetComponent<AudioSource>();

		AudioSource.PlayOneShot(music[0]);

		base.Awake();
	}
}
