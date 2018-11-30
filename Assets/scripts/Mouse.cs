using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    private Generator gen;
    private Destroy des;
    private GameObject selectedFirst, selectedFS;
    private GameObject toSwapSecond, toSS;
    private bool first;

    public GameObject selecetedSprite;

    public void Swap()
    {
        if (selectedFirst != null)
        {
            int x = (int)selectedFirst.transform.position.x, x1 = (int)toSwapSecond.transform.position.x;
            int y = (int)selectedFirst.transform.position.y, y1 = (int)toSwapSecond.transform.position.y;
            int j = gen.matr[y][x].Second;
            selectedFirst.transform.position = new Vector2(x1, y1);
            Pair<GameObject, int> toSw = new Pair<GameObject, int>(selectedFirst, j);
            gen.matr[y1][x1].First.transform.position = new Vector2(x, y);
            gen.matr[y][x].Copy(gen.matr[y1][x1]);
            gen.matr[y1][x1].Copy(toSw);
            //selectedFirst = null;
            //toSwapSecond = null;
            Destroy(selectedFS);
            Destroy(toSS);
        }
    }

	private void Start () {
        gen = transform.GetComponent<Generator>();
        des = transform.GetComponent<Destroy>();
        first = true;
	}
	
	private void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            if (hit.collider != null && hit.collider.GetComponent<CubeParam>() != null)
            {
                if (first)
                {
                    selectedFirst = hit.collider.gameObject;
                    Destroy(selectedFS);
                    selectedFS = Instantiate(selecetedSprite, selectedFirst.transform.position, transform.rotation, transform);
                    first = false;
                }
                else if ((Mathf.Abs(selectedFirst.transform.position.x - hit.collider.gameObject.transform.position.x) <= 1 && selectedFirst.transform.position.y == hit.collider.gameObject.transform.position.y) || (Mathf.Abs(selectedFirst.transform.position.y - hit.collider.gameObject.transform.position.y) <= 1 && selectedFirst.transform.position.x == hit.collider.gameObject.transform.position.x))
                {
                    toSwapSecond = hit.collider.gameObject;
                    Destroy(toSS);
                    toSS = Instantiate(selecetedSprite, toSwapSecond.transform.position, transform.rotation, transform);
                    first = true;
                    Swap();
                    des.NeedDestr = true;
                    gen.SmsGen = true;
                }
            }
            else
                Debug.Log("Piu and miss you losser...");
        }
    }
}
