using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof( AudioSource))]
public class BGMController : MonoBehaviour {


	public static BGMController theBGMController = null;
	List<AudioClip> audioClips = new List<AudioClip>();
	//这个类是用于BGM切换的
	public AudioSource sourceBGM;

	void Start () {
		theBGMController = this;
		sourceBGM = this.GetComponent<AudioSource>();
		sourceBGM.loop = true;
		ChangeBGM("costumNormal");
		DontDestroyOnLoad(this.gameObject);
	}

	public void ChangeBGM(string BGMName)
	{
		if (!sourceBGM)
			return;

		for (int i = 0; i < audioClips.Count; i++)
		{
			if (audioClips[i].name == BGMName)
			{
				sourceBGM.clip = audioClips[i];
				sourceBGM.Play();
				return;
			}
		}

		try
		{
			AudioClip theClip = Resources.Load<AudioClip>("Music/" + BGMName);
			if (theClip == null)
				return;

			theClip.name = BGMName;
			audioClips.Add(theClip);
			sourceBGM.clip = theClip;
			sourceBGM.Play();
		}
		catch
		{
			return;
		}
	}
}