using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
	// gesture types
    public bool gesture_input = false;
    public bool gesture_affirmative = false;
    public bool gesture_negative = false;

    // audio: You are approaching the bardot, The Cure is playing here, would you like to sample the artist?
    public AudioSource guideVenueAudio;

    // audio: New Order is also playing, would you like to sample them?
    public AudioSource guideNextArtistAudio;

    // audio: Would you like to listen to more music by New Order?
    public AudioSource guideNextSongAudio;

    // audio: acutal audio Clip1
    public AudioSource streamSongAudio;

    // audio: actual audio Clip2
    public AudioSource streamSongAudio2;
    
    public bool inGuideVenue = false;
    public bool inGuideNextArtist = false;
    public bool inGuideSong = false;
    public bool inStreamSong = false;
    public bool inStreamSong2 = false;


    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }



    //public void Set_gesture_input_true(bool _bool)
    public void Set_gesture_input_true(bool _bool)
    {
        //gesture_input = _bool;
        gesture_input = _bool;
        gesture_affirmative = false;
        gesture_negative = false;
        Debug.Log("input true");
        }

    public void Set_gesture_affirmative_true(bool _bool)
    {
        gesture_input = false;
        gesture_affirmative = _bool;
        gesture_negative = false;
        Debug.Log("affirmative true");


    }
    public void Set_gesture_negative_true(bool _bool)
    {
        gesture_input = false;
        gesture_affirmative = false;
        gesture_negative = _bool;
        Debug.Log("negative true");

    }

    // Start is called before the first frame update
    void Start()
    {
        // // audio: You are approaching the bardot, The Cure is playing here, would you like to sample the artist?
        // Debug.Log("before guideVenue");

        // guideVenueAudio.Play(0);
        // Debug.Log("after guideVenue");


    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("begin update");

        // // no
        // if (inGuideNextArtist == false && gesture_input == true || Input.GetKey("n"))
        // {
        //     gesture_input = false;
        //     inGuideNextArtist = true;

        //     // audio: New Order is also playing, would you like to sample them?
        //     guideVenueAudio.Pause();
        //     guideNextArtistAudio.Play(0);
        //     Debug.Log("in GuideNextArtist");

        //     // yes
        //     if (gesture_affirmative == true || Input.GetKey("up"))
        //     {
        //         // audio: acutal audio Clip1
        //         guideNextArtistAudio.Pause();
        //         streamSongAudio.Play(0);
        //         Debug.Log("in stream song for artist");


        //         // skip
        //         if (gesture_input == true || Input.GetKey("n"))
        //         {

        //             //// audio: Would you like to listen to more music by New Order?
        //             streamSongAudio.Pause();
        //             streamSongAudio2.Play(0);
        //             Debug.Log("in stream song for artist 2");

        //         }
        //     }
        //     Debug.Log("left GuideNextArtist Loop");
        //     inGuideNextArtist = false;
        // }

    }
}
