using System;
using System.Collections.Generic;

[Serializable]
public class QueryResult {
	public List<Event> events;
	public Embedded _embedded;
}

[Serializable]
public class Embedded {
	public List<Artist> artists;
	public List<Venue> venues;
}