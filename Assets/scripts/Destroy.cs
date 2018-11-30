using UnityEngine;

public class Destroy : MonoBehaviour
{
    private Generator gen;
    private Point point;
    public bool NeedDestr { get; set; }
    public bool DestroyedSms { get; set; }

	private void Start ()
    {
        gen = transform.GetComponent<Generator>();
        point = transform.GetComponent<Point>();
        NeedDestr = true;
        DestroyedSms = false;
	}

    private int ToUp(int right, int up)
    {
        string name = gen.matr[right][up].First.transform.GetComponent<CubeParam>().Name;
        int result = 0;
        for (int k = up; k < gen.WidthB; k++)
        {
            string st = gen.matr[right][k].First.transform.GetComponent<CubeParam>().Name;
            if (st == name && name != "~")
            {
                result++;
            }
            else
            {
                break;
            }
        }
        return result;
    }

    private int ToRight(int right, int up)
    {
        string name = gen.matr[right][up].First.transform.GetComponent<CubeParam>().Name;
        int result = 0;
        for (int k = right; k < gen.HightB; k++)
        {
            string st = gen.matr[k][up].First.transform.GetComponent<CubeParam>().Name;
            if (st == name && name != "~")
            {
                result++;
            }
            else
            {
                break;
            }
        }
        return result;
    }

    private void ToDestroy()
    {
        Debug.Log("Destroying.\nPlease wait ...");
        NeedDestr = false;
        DestroyedSms = false;
        gen = transform.GetComponent<Generator>();
        for (int i = 0; i < gen.HightB; i++)
        {
            for (int k = 0; k < gen.WidthB; k++)
            {
                int right = ToUp(i, k), up = ToRight(i, k);
                //Debug.Log("Up: " + i + " Right: " + k + "\nnum = " + up);
                if (up >= 3)
                {
                    for (int o = i; o < up + i; o++)
                    {
                        DestroyedSms = true;
                        NeedDestr = true;
                        point.Points += gen.matr[o][k].First.transform.GetComponent<CubeParam>().Point;
                        Destroy(gen.matr[o][k].First);
                        gen.matr[o][k].Copy(new Pair<GameObject, int>(gen.emptySprite, -1));
                    }
                    //Debug.Log("Destroing up\nBy " + up);
                }
                if (right >= 3)
                {
                    for (int o = k; o < right + k; o++)
                    {
                        DestroyedSms = true;
                        NeedDestr = true;
                        //Debug.Log("right: " + right + "\nO: " + o);
                        point.Points += gen.matr[i][o].First.transform.GetComponent<CubeParam>().Point;
                        Destroy(gen.matr[i][o].First);
                        gen.matr[i][o].Copy(new Pair<GameObject, int>(gen.emptySprite, -1));
                    }
                    //Debug.Log("Destroing right\nBy " + right);
                }
            }
        }
        if (NeedDestr == false)
            transform.GetComponent<Mouse>().Swap();
            gen.NeedGen = true;
    }

	private void Update ()
    {
        if (NeedDestr)
            ToDestroy();
	}
}