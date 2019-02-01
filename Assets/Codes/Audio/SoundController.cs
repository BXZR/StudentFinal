using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	[RequireComponent(typeof(AudioSource))]
	public class SoundPlayer : MonoBehaviour
	{

		public static SoundPlayer soundPlayer = null;
		private List<AudioClip> clips = new List<AudioClip>();
		AudioClip theClip;
		//这个类是用于BGM切换的
		private AudioSource Source;

		void Start()
		{
			soundPlayer = this;
			Source = GetComponent<AudioSource>();
			Source.loop = false;
			// DontDestroyOnLoad(this.gameObject);
		}

		public void StopSound()
		{
			if (Source.isPlaying)
				Source.Stop();
		}



		public void PlaySound(string SoundName)
		{
			if (!Source)
				return;

			if (theClip != null && theClip.name == SoundName && Source.isPlaying)
				return;

			for (int i = 0; i < clips.Count; i++)
			{
				if (clips[i].name == SoundName)
				{
					Source.PlayOneShot(clips[i]);
					return;
				}

			}
			theClip = Resources.Load<AudioClip>("Audio/" + SoundName);
			if (theClip == null)
				return;
			theClip.name = SoundName;
			Source.PlayOneShot(theClip);
			clips.Add(theClip);
		}

	}