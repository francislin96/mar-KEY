using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Linq;

public class songs {
	public static string [] artist_15316301 = {"Dollar To A Diamond", "Getty Woah", "Selenas"};
	public static string [] artist_4671201 = {"On A Prayer", "Cold", "Drown"};
	public static string [] artist_13771893 = {"Age of Consent", "Blue Monday", "Close To Me"};
}

public class MusicSceneManager : MonoBehaviour
{
	// api manager
	public API_Manager api_Manager;
	public int artist_index = 0;
	public Text Artist_Name;
	Artist current_artist;

	// artist info
	public List<Artist> artists;
	public RawImage Artist_Image;
	public Texture emptyUserTexture;

	// event info
	public Text Event_Date;
	public Text Event_Time;

	// venue info
	public Text Venue_Name;
	public Text Venue_Location;

	// song info
	public Text Song_Name;

	// gesture manager
	public GestureManager gestureManager;

	// app states
	public bool isUserApproachingVenue = false;
	public bool isUserListeningToSongs = false;

	// Audio Gameobject
	public AudioSource audio;
	string [] song_array;
	int song_index = 0;

    // Start is called before the first frame update
    void Start()
    {
    	// find gestureManager
    	gestureManager = GameObject.Find("GestureManager").GetComponent<GestureManager>();


    }

    // Update is called once per frame
    void Update()
    {
    	if ("artist_" + current_artist.id.ToString() == "artist_15316301") {
    		song_array = songs.artist_15316301;
    	}
    	else if ("artist_" + current_artist.id.ToString() == "artist_4671201") {
    		song_array = songs.artist_4671201;
    	} 
    	else if ("artist_" + current_artist.id.ToString() == "artist_13771893") {
    		song_array = songs.artist_13771893;
    	}
        if (isUserApproachingVenue) {
        	if (gestureManager.gesture_affirmative) {
        		isUserListeningToSongs = false;
				gestureManager.gesture_affirmative = false;
				isUserApproachingVenue = false;
				play_Song(song_array[song_index]);
        	}
        	if (gestureManager.gesture_negative) {
        		isUserListeningToSongs = false;
        		get_Next_Artist();
        		gestureManager.gesture_negative = false;
        		isUserApproachingVenue = true;
        		if (current_artist.id == 15316301) {
        			play_Recording("guideNextArtistAudio_MayaB");
        		} 
        		else if (current_artist.id == 4671201) {
        			play_Recording("guideNextArtistAudio_BoyinSpace");
        		}
        		else if (current_artist.id == 13771893) {
        			play_Recording("guideNextArtistAudio_NoArtist");
        		}
        		// play_Recording("guideNextArtistAudio");
        	}
        	
        }
        if (audio.isPlaying) {
        	if (gestureManager.gesture_input) {
        		isUserListeningToSongs = false;
        		song_index = song_index + 1;
        		if (song_index >= song_array.Length) {
        			song_index = 0;
        		}
        		gestureManager.gesture_input = false;
        		play_Song(song_array[song_index]);
        	}
        	else if (gestureManager.gesture_negative) {
        		isUserApproachingVenue = true;
        		isUserListeningToSongs = false;
        		get_Next_Artist();
        		gestureManager.gesture_negative = false;
        		if (current_artist.id == 15316301) {
        			play_Recording("guideNextArtistAudio_MayaB");
        		} 
        		else if (current_artist.id == 4671201) {
        			play_Recording("guideNextArtistAudio_BoyinSpace");
        		}
        		else if (current_artist.id == 13771893) {
        			play_Recording("guideNextArtistAudio_NoArtist");
        		}

        	}
        }
        if (!audio.isPlaying && isUserListeningToSongs) {
        	song_index = song_index + 1;
    		if (song_index >= song_array.Length) {
    			song_index = 0;
    		}
    		play_Song(song_array[song_index]);
        }
    }

    public void setup_MusicSceneManager() {
    	// find artists
    	artists = api_Manager.getUniqueArtists(api_Manager.queryResult._embedded.artists);

    	var no_artist = artists.SingleOrDefault(x => x.id == 13771893);
		if (no_artist != null)
    		artists.Remove(no_artist);

        display_Artist();
        display_Event();
        display_Venue();

        isUserApproachingVenue = true;
        play_Recording("guideVenueAudio_MayaB");
    }

	// display artist
	public void display_Artist() {
		// get current artist
    	current_artist = artists[artist_index];

    	// display artist image
    	StartCoroutine(DisplayArtistImage(current_artist.image_url));

    	// display artist name
    	Artist_Name.text = current_artist.name;
	}

	IEnumerator DisplayArtistImage(string MediaUrl)
	{   
	    UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
	    yield return request.SendWebRequest();
	    if(request.isNetworkError || request.isHttpError) {
	        Debug.Log(request.error);
	        Artist_Image.texture = (Texture)emptyUserTexture;
	    }
	    else
	        Artist_Image.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
	}

	// display event
	public void display_Event() {
		// get event
		Event event_info = api_Manager.getEventOfArtist(api_Manager.queryResult, current_artist.id);

		// display datetime
		string dt_string = System.DateTime.ParseExact("2019-11-18T19:30:00", "yyyy-MM-dd\\THH:mm:ss", null).ToString("ddd, dd MMM yyyy, hh:mm tt");
		Event_Date.text = System.DateTime.ParseExact(event_info.starts_at, "yyyy-MM-dd\\THH:mm:ss", null).ToString("ddd, dd MMM yyyy");
		Event_Time.text = System.DateTime.ParseExact(event_info.starts_at, "yyyy-MM-dd\\THH:mm:ss", null).ToString("h:mm tt");
	}

	// display venue
	public void display_Venue() {

		// get venue
		Venue venue = api_Manager.getVenueOfArtist(api_Manager.queryResult, current_artist.id);

		// display name
		Venue_Name.text = venue.name;

		// display location
		Venue_Location.text = venue.location;
	}

	public void get_Next_Artist() {
		artist_index = artist_index + 1;
		if (artist_index >= 2) {
			artist_index = 0;
		}
		song_index = 0;
		display_Artist();
		display_Event();
		display_Venue();
	}

	public void load_Home_Scene() {
		SceneManager.LoadScene("Home");
	}

	public void play_Song(string name) {
		audio.clip = Resources.Load("Audio/"+name) as AudioClip;
 		audio.Play();
 		Song_Name.text = "Playing: " + name;
 		isUserListeningToSongs = true;
	}

	public void play_Recording(string name) {
		audio.clip = Resources.Load("Audio/"+name) as AudioClip;
 		audio.Play();
 		Song_Name.text = "---";
	}

	public void buy_Tickets() {
		// get event
		Event event_info = api_Manager.getEventOfArtist(api_Manager.queryResult, current_artist.id);
		Application.OpenURL(event_info.rsvp_url);
	}
}
