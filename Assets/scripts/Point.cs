using UnityEngine.UI;
using UnityEngine;

public class Point : MonoBehaviour {

	public int Points { get; set; }

    private int toSort = 0, toSortIndex = 1;
    private Text pT;

	private void Start () {
        Points = 0;
        pT = GameObject.Find("Point").GetComponent<Text>();
	}
	
	
	private void Update () {
        pT.text = System.Convert.ToString(Points);
        for (int i = 1; i <= 3; i++)
        {
            if (!PlayerPrefs.HasKey(i.ToString()))
            {
                PlayerPrefs.SetInt(i.ToString(), Points);
            }
            else if (PlayerPrefs.GetInt(i.ToString()) < Points)
            {
                toSort = PlayerPrefs.GetInt(i.ToString());
                toSortIndex = i;
                PlayerPrefs.SetInt(i.ToString(), Points);
            }
            if (toSort > PlayerPrefs.GetInt(i.ToString()))
            {
                int j = PlayerPrefs.GetInt(toSortIndex.ToString());
                PlayerPrefs.SetInt(i.ToString(), toSort);
                toSort = j;
            }
        }
        PlayerPrefs.Save();
    }

    public void ToMenu()
    {
        Application.LoadLevel("Menu");
    }
}
