using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public class AudioManager : MonoBehaviour
{
	private void Start()
	{
		// jalankan BGM saat game dimulai
		PlayBGM();
	}
	
	// fungsi yang disiapkan untuk perintah menjalankan bgm dari script lain
	public void PlayBGM() { }
	// fungsi yang disiapkan untuk perintah menjalankan sfx dari script lain
	public void PlaySFX() { }

    public AudioSource bgmAudioSource

...

// ini sudah di set di play saat Start()
public void PlayBGM() 
{ 
	// kita tinggal tambahkan saja fungsi untuk memainkan audio source bgm nya
	bgmAudioSource.Play();
}
}
}