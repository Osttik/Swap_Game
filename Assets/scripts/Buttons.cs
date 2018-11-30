using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {
    
    private Text vHight;
    private Text vWidth;
    public Text[] scoreTable = new Text[3];
    private Slider sHight;
    private Slider sWidth;
    private GameObject data;
    private GameObject score;
    private const int max = 20;
    private const int min = 3;

    private void Start()
    {
        vHight = GameObject.Find("TextHight").GetComponent<Text>();
        vWidth = GameObject.Find("TextWidth").GetComponent<Text>();
        sHight = GameObject.Find("SliderHight").GetComponent<Slider>();
        sWidth = GameObject.Find("SliderWidth").GetComponent<Slider>();
        data = GameObject.Find("ToNext");
        score = GameObject.Find("ScoreGround");

        sHight.maxValue = max;
        sHight.minValue = min;
        sWidth.maxValue = max;
        sWidth.minValue = min;

        score.active = false;
    }

    public void ToStart()
    {
        data.GetComponent<Data>().CurrentHight = sHight.value;
        data.GetComponent<Data>().CurrentWidth = sWidth.value;
        Application.LoadLevel("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenScore()
    {
        score.active = true;
    }

    public void CloseScore()
    {
        score.active = false;
    }

    public void Update()
    {
        vHight.text = System.Convert.ToString(sHight.value);
        vWidth.text = System.Convert.ToString(sWidth.value);

        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                scoreTable[i-1].text = i.ToString() + " : " + PlayerPrefs.GetInt(i.ToString()).ToString();
            }
        }
    }
}
