using UnityEngine;

public class Data : MonoBehaviour {

    public float CurrentHight { get; set; }
    public float CurrentWidth { get; set; }
	
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
	}
}
