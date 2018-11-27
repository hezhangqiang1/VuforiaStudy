using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public AudioClip clip;

    void Start () {
        Invoke("PlayMusic", 5f);
    }
	
	
	void Update () {
        //if (transform.localPosition.y >= 0)
        //{
        //    return;
        //}
        //transform.Translate(new Vector3(0, 1f, 0) * Time.deltaTime);
       
	}
    
    private void PlayMusic()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
