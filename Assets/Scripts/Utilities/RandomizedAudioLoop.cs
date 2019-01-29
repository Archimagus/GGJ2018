using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizedAudioLoop : MonoBehaviour
{
	public bool playOnAwake = false;
	public bool loop = true;
	[Range(-0.5f, 0.0f)]
	public float maxPitchUp = 0;
	[Range(0.0f, 0.5f)]
	public float maxPitchDown = 0;
	[Range(1.0f, 2.0f)]
	public float maxVolumeUp = 1;
	[Range(0.0f, 1.0f)]
	public float maxVolumeDOwn = 1;

	public float minRepeadDelay = 0.0f;
	public float maxRepeadDelay = 0.0f;

	public SoundType SoundType = SoundType.SoundEffect;

	public AudioClip[] Clips;


	AudioSource source;
	bool playing = false;
	float defaultPitch;
	void Start()
	{
		var go = new GameObject();
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		source = go.AddComponent<AudioSource>();
		source.loop = false;
		source.rolloffMode = AudioRolloffMode.Linear;
		defaultPitch = source.pitch;
		switch (SoundType)
		{
			case SoundType.SoundEffect:
				source.outputAudioMixerGroup = AudioManager.EffectsMixerGroup;
				break;
			case SoundType.Ambience:
				source.outputAudioMixerGroup = AudioManager.AmbienceMixerGroup;
				break;
			case SoundType.Music:
				source.outputAudioMixerGroup = AudioManager.MusicMixerGroup;
				break;
			case SoundType.Interface:
				source.outputAudioMixerGroup = AudioManager.InterfaceMixerGroup;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		if (playOnAwake)
			Play();
	}

	public void Play()
	{
		playing = true;
		StartCoroutine(LoopClips());
	}

	public void Stop()
	{
		playing = false;
		source.Stop();
	}

	IEnumerator LoopClips()
	{
		while (playing && Clips.Length > 0)
		{
			var clip = Clips[Random.Range(0, Clips.Length)];
			source.clip = clip;
			source.pitch = defaultPitch + Random.Range(maxPitchDown, maxPitchUp);
			source.volume = AudioManager.GetVolume(SoundType) * Random.Range(maxVolumeDOwn, maxVolumeUp);
			source.Play();
			if (!loop)
				break;
			yield return new WaitForSeconds(clip.length + Random.Range(minRepeadDelay, maxRepeadDelay));
		}
	}
}
