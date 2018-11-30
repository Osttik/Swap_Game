using System;
using System.Collections.Generic;
using UnityEngine;

public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
        First = first;
        Second = second;
    }

    public void Copy(Pair<T, U> n)
    {
        First = n.First;
        Second = n.Second;
    }

    public T First { get; set; }
    public U Second { get; set; }
};

public class Generator : MonoBehaviour {

    public GameObject emptySprite;
    public GameObject[] blockArr = new GameObject[3];
    public bool NeedGen { get; set; }
    public bool SmsGen { get; set; }
    public int HightB { get; set; }
    public int WidthB { get; set; }
    public List<List<Pair<GameObject, int>>> matr = new List<List<Pair<GameObject, int>>>();

    private GameObject cam;
    private GameObject bck;
    private Destroy destr;
    private GameObject toNext;

    Pair<GameObject, int> Create(int x, int i)
    {
        GameObject newObj;
        int num = UnityEngine.Random.Range(0, 3);
        newObj = Instantiate(blockArr[num], new Vector2(x, i), transform.rotation, transform);
        Pair<GameObject, int> element = new Pair<GameObject, int>(newObj, num);
        return element;
    }

    void Start() {
        destr = transform.GetComponent<Destroy>();
        cam = GameObject.Find("Main Camera");
        bck = GameObject.Find("BackGround");
        bck.transform.localScale = new Vector3(Screen.height/150f, Screen.width/264f, bck.transform.localScale.z);
        NeedGen = false;
        SmsGen = true;
        toNext = GameObject.Find("ToNext");
        HightB = (int)toNext.GetComponent<Data>().CurrentHight;
        WidthB = (int)toNext.GetComponent<Data>().CurrentWidth;
        cam.transform.position = new Vector3(WidthB / 2, HightB / 2, -12);
        bck.transform.position = new Vector3(WidthB / 2, HightB / 2, bck.transform.position.z);
        cam.GetComponent<Camera>().orthographicSize = Mathf.Max(HightB/2 + 0.5f, WidthB/2 + 0.5f);
        for(int i = 0; i < HightB; i++)
        {
            matr.Add(new List<Pair<GameObject, int>>());
            for(int x = 0; x < WidthB; x++)
            {
                matr[i].Add(Create(x, i));
            }
        }

        int[] arr = new int[99999];
        arr[0] = 0;
        arr[1] = 1;
        for(int j = 1; j < 50; j++)
        {
            arr[2 * j] = arr[j];
            arr[2 * j + 1] = arr[j] + arr[j + 1];
        }
        for(int j = 0; j < 50; j++)
        {
            Debug.Log(arr[j]);
        }
	}

    private void Generate()
    {
        Debug.Log("Generate.\nPlease wait ...");
        SmsGen = false;
        //for (int i = 0; i < HightB; i++)
        //{
        //    for (int j = 0; j < WidthB; j++)
        //    {
        //        if (matr[i][j].Second == -1)
        //        {
        //            int elem = -1;
        //            for (int k = j+1; k < WidthB; k++)
        //            {
        //                if (matr[i][k].Second != -1)
        //                {
        //                    elem = k;
        //                    break;
        //                }
        //            }
        //            if (elem != -1)
        //            {
        //                matr[i][j].Copy(matr[i][elem]);
        //                matr[i][elem].Copy(new Pair<GameObject, int>(emptySprite, -1));
        //            }
        //        }
        //    }
        //}
        for (int i = 0; i < HightB; i++)
        {
            for (int j = 0; j < WidthB; j++)
            {
                if (matr[i][j].Second == -1)
                {
                    matr[i][j].Copy(Create(j, i));
                    SmsGen = true;
                }
            }
        }
        NeedGen = false;
    }
	
	void Update ()
    {
        if (NeedGen && SmsGen)
        {
            Generate();
            if (SmsGen)
                destr.NeedDestr = true;
        }
	}
}