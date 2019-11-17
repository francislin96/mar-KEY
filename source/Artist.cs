using System;
using System.Collections.Generic;

[Serializable]
public class Artist {
	public int id;
	public string name;
	public string image_url;
	public string track_count;
	public bool on_tour;
	public string artist_url;
	public string track_url;

	public string details() {
		string str = "";
		str = str + "id: " + id;
		str = str + "\nname: " + name;
		str = str + "\nimage_url: " + image_url;
		str = str + "\ntrack_count: " + track_count;
		str = str + "\non_tour: " + on_tour;
		str = str + "\nartist_url: " + artist_url;
		str = str + "\ntrack_url: " + track_url;
		return str;
	}
}