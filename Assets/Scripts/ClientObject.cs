using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using SimpleJSON;
//using System.Threading.Tasks;

public class ClientObject : MonoBehaviour {
	public string flightID;
	public TextMesh textPrefab;
	private FIStatic flight;
	private FIDynamic flight2;
	private PlaneXMLv1 PlaneXML;
	private NetworkCredential c;
	// Use this for initialization
	IEnumerator Start () {
		PlaneXML = new PlaneXMLv1 ();
		c = new NetworkCredential("kushagra.pundeer@gmail.com", "B8955E7C-C03F-4CF9-9B8D-C38C50FDA67A");
		PlaneXML.Credentials = c;
		flight = PlaneXML.FlightInfo(flightID, true, true);
		print ("OK");
		while(true)
		{
			StartCoroutine("refresh");
			yield return new WaitForSeconds(5);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void refresh()
	{
		flight2 = PlaneXML.FlightStatus(flightID);
		print("Latitude " + flight2.Lat);
		print("Longitude " + flight2.Lon);
		print("Altitude " + flight2.Alt);
		print("Arrival Mins. " + flight2.ArrivalMins);
		print("Ground Speed " + flight2.GS);
		UpdatePlaces ((float)flight2.Lat, (float)flight2.Lon,(float)flight2.Alt);
	}

	void UpdatePlaces(float lat, float lng, float height)
	{
		print ("Updating places");
		//Add area calc.
		string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + lat + "," + lng + "&radius=100&types=natural_feature|country|colloquial_area|neighborhood|&key=AIzaSyC-CL4XpshMmVqpGnBiuL1GRe0PLinj-U0";
		print ("Getting url " + url);
		WWW www = new WWW (url);
		StartCoroutine ("WaitForDl", www);
		string json = www.text;
		print (json);
		
		var N = JSON.Parse (json);
		int i = -1;
		while (N["results"][++i]["id"] != null) {
			Vector3 pos = new Vector3 (N ["results"] [i] ["geometry"] ["location"] ["lat"].AsFloat, 0.01f, N ["results"] [i] ["geometry"] ["location"] ["lng"].AsFloat);
			pos -= new Vector3 (lat, 0f, lng);
			//pos*=1000;
			print (pos);
			TextMesh tmp = Instantiate (textPrefab, pos, Quaternion.identity) as TextMesh;
			tmp.text = N ["results"] [i] ["name"];
			//meshList.Add(tmp);
			if (i > 100)
				break;
		}
	}

	IEnumerator WaitForDl(WWW www)
	{
		yield return www;
	}


}