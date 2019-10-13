using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaButton : MonoBehaviour {	
	public void Enter (string location) {
		Areas.location = location;
		SceneManager.LoadScene("Dungeon");
	}
}
