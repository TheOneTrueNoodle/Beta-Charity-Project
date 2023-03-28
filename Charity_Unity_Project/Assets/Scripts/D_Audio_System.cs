using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Audio_System : MonoBehaviour
{
    public List<AudioClip> Sounds = new List<AudioClip>();
    public List<string> SoundNames = new List<string>();


    public void PlayAudio(string name)//, bool randomness, float specificPitch)
    {
        //create a new object for the sound to play from
        GameObject soundPlayer = new GameObject();
        soundPlayer.transform.parent = transform; // parent it to this object to keep scene tidy
        soundPlayer.AddComponent<AudioSource>();//add audio source to it

        AudioSource soundPlayerSource = soundPlayer.GetComponent<AudioSource>();//reference the audio source itself 

        //*----------- the next few lines dont work rn *
        //set the pitch to the specified value
        //soundPlayerSource.pitch = specificPitch;

        //if "randomness" si true, then vary the pitch a bit
        // if (randomness)
        //   soundPlayerSource.pitch = Random.Range(specificPitch - 0.1f, specificPitch + 0.1f);
        //------------

        //loop through all of the audio names to find the "name" specified. If found then...
        bool hasFoundSound = false;

        for (int i = 0; i < SoundNames.Count; i++)
        {
            if (SoundNames[i] == name)
            {
                soundPlayerSource.clip = Sounds[i];//...assign the clip 
                hasFoundSound = true;
            }
        }

        if (!hasFoundSound)//if the sound aint there then give an error and stop the script here
        {
            Debug.LogError("The sound '" + name + "' doesn't exist bro");
            return;
        }

        //play the ting then delete the ting 2 sec later.
        soundPlayerSource.Play();
        Destroy(soundPlayer, 2f);

    }
}
