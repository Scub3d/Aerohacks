using UnityEngine;
using System.Collections;

public class relaxinSceneMenuSelector : MonoBehaviour {

	public GameObject[] menuItems;
	public GameObject[] menuItemsBorders;
	public bool locked = false;

	public int itemSelected = 0;

	// Use this for initialization
	void Start () {
		menuItems = new GameObject[4] {GameObject.Find("instrumental_menu_item"), GameObject.Find("white_noise_menu_item"), GameObject.Find("asmr_menu_item"), GameObject.Find("nature_menu_item") };
		menuItemsBorders = new GameObject[4] {GameObject.Find("border1"), GameObject.Find("border2"), GameObject.Find("border3"), GameObject.Find("border4") };

		foreach (GameObject obj in menuItemsBorders) {
			if(obj.name != "border1")
				obj.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!locked) {
			if (Input.GetAxisRaw ("Horizontal") == 1 && itemSelected != 3)
				changeSelectedItem (itemSelected + 1);
			else if (Input.GetAxisRaw ("Horizontal") == -1 && itemSelected != 0)
				changeSelectedItem (itemSelected - 1);
			if(Input.GetButtonUp("Fire1"))
				ifSelected(itemSelected);
		}
	}

	public void changeSelectedItem(int newSelectedIndex) {
		menuItemsBorders [itemSelected].GetComponent<SpriteRenderer> ().enabled = false;
		menuItemsBorders [newSelectedIndex].GetComponent<SpriteRenderer> ().enabled = true;
		itemSelected = newSelectedIndex;
		StartCoroutine (takeOne());
	}

	public void ifSelected(int index) {

	}

	public IEnumerator takeOne() {
		locked = true;
		yield return new WaitForSeconds (1);
		locked = false;
	}

}
