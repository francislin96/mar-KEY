using System;
using System.Collections.Generic;

[Serializable]
public class Venue {
	public int id;
	public string name;
	public string image_url;
	public string location;

	public string details() {
		string str = "";
		str = str + "id: " + id;
		str = str + "\nname: " + name;
		str = str + "\nimage_url: " + image_url;
		str = str + "\nlocation: " + location;
		return str;
	}
}
