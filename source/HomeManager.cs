using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
	// public Text Genre 

    public Dropdown Date_Dropdown;
    
    // Start is called before the first frame update
    void Start()
    {
    	// clear all playerprefs
         PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void search() {
		set_Date();
		set_Location();
		SceneManager.LoadScene("MusicScene");
    }

    // set date
    public void set_Date() {
    	// if date is any, set date to ""
    	if (Date_Dropdown.options[Date_Dropdown.value].text == "Any Date") {
    		PlayerPrefs.SetString("starts_at", "");
    		PlayerPrefs.SetString("ends_at", "");
    	}
    	// else, define starts_at and ends_at
    	else {
    		string dropdown_date_value = Date_Dropdown.options[Date_Dropdown.value].text;
    		PlayerPrefs.SetString("starts_at", System.DateTime.ParseExact(dropdown_date_value, "MMMM dd, yyyy", null).ToString("yyyy-MM-dd\\T00:00:00\\Z"));
    		PlayerPrefs.SetString("ends_at", System.DateTime.ParseExact(dropdown_date_value, "MMMM dd, yyyy", null).ToString("yyyy-MM-dd\\T23:59:59\\Z"));
    	}
    }

    // set location
    public void set_Location() {
    	PlayerPrefs.SetString("latitude", "34.103160");
    	PlayerPrefs.SetString("longitude", "-118.326160");
    	PlayerPrefs.SetString("radius", "0.1");
    }
}
