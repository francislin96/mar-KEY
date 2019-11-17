using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class API_Manager : MonoBehaviour
{
	// MusicSceneManager
	public MusicSceneManager musicSceneManager;

    // region
	string latitude;
	string longitude;
    string radius;
    
    // period
    string starts_at;
    string ends_at;

    public QueryResult queryResult;


    // Start is called before the first frame update
    void Start()
    {
        // region
        latitude = PlayerPrefs.GetString("latitude");
        longitude = PlayerPrefs.GetString("longitude");
        radius = PlayerPrefs.GetString("radius");

        // period
        starts_at = PlayerPrefs.GetString("starts_at");
        ends_at = PlayerPrefs.GetString("ends_at");

        queryResult = getQueryResults(_latitude:latitude, _longitude:longitude, _radius:radius);

        // var foundItem = queryResult._embedded.artists.SingleOrDefault(item => item.id == queryResult.events[0].artist_id);
        
        // List<Artist> artists = getUniqueArtists(queryResult._embedded.artists);

        // Debug.Log(getVenueOfArtist(queryResult, artists[0].id).details());

        musicSceneManager.setup_MusicSceneManager();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string build_API_URL() {

        // use bandsintown API
        string url = "https://search.bandsintown.com/search?query={";

        // if period, add period portion to api
        if (starts_at != "" && ends_at != "") {
            url = url + "%22period%22%3A%7B%22starts_at%22%3A%22" + starts_at.Replace(":", "%3A") + "%22%2C%22ends_at%22%3A%22" + ends_at.Replace(":", "%3A") + "%22%7D%2C";
        }

        // if region, add region portion to api
        if (latitude != "" && latitude != "" && radius != "") {
            url = url + "%22region%22%3A%7B%22latitude%22%3A" + latitude + "%2C%20%22longitude%22%3A" + longitude + "%2C%22radius%22%3A" + radius + "%7D%2C";
        }

        // url search for events, order by date
        url = url + "%22entities%22%3A%5B%7B%22order%22%3A%22start%20date%22%2C%22type%22%3A%22event%22%7D%5D";

        //finish bandsintown API
        url = url + "}";

        return url;
    }

    public QueryResult getQueryResults(string _latitude, string _longitude, string _radius) {

        string url_string = build_API_URL();
        
        // get QueryResult
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url_string);
		request.Headers.Add("x-api-key", "nTG4tbSXpIaniCHlJ62q06GzIpROk6qh56EiK7N1");
		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		string jsonResponse = reader.ReadToEnd();
        QueryResult queryResult = JsonUtility.FromJson<QueryResult>(jsonResponse);

        return queryResult;

    }

    public List<Artist> getUniqueArtists(List<Artist> artists) {
        return artists.GroupBy(artist => artist.id).Select(g => g.First()).ToList();
    }

    public Event getEventOfArtist(QueryResult queryResult, int artist_id) {
    	return queryResult.events.Where(e => e.artist_id == artist_id).FirstOrDefault();
    }

    public Venue getVenueOfArtist(QueryResult queryResult, int artist_id) {
        Event event_info = getEventOfArtist(queryResult, artist_id);
        Venue venue = queryResult._embedded.venues.Where(v => v.id == event_info.venue_id).FirstOrDefault();
        return venue;
    }



}



