using System;
using System.Collections.Generic;

[Serializable]
public class Event {
	public int id;
	public string image_url;
	public int artist_id;
	public int venue_id;
	public int rsvp_count;
	public string starts_at;
	public bool ticket_available;
	public int[] lineup;
	public string rsvp_url;
	public string event_url;

	public string details() {
		string str = "";
		str = str + "id: " + id;
		str = str + "\nimage_url: " + image_url;
		str = str + "\nartist_id: " + artist_id;
		str = str + "\nvenue_id: " + venue_id;
		str = str + "\nrsvp_count: " + rsvp_count;
		str = str + "\nstarts_at: " + starts_at;
		str = str + "\nticket_available: " + ticket_available;
		str = str + "\nrsvp_url: " + rsvp_url;
		str = str + "\nevent_url: " + event_url;
		return str;
	}
}
