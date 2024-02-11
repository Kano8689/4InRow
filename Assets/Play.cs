using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public GameObject Prefab, Parent;
    public GameObject[,] allBoxes;
    public Sprite Red, Black, Green;
    int Rows, Cols;
    int playBox = 8, winBox = 4;
    bool isTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        allBoxes = new GameObject[playBox, playBox];

        Parent.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        Parent.GetComponent<GridLayoutGroup>().constraintCount = playBox;

        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j < playBox; j++)
            {
                int m = i;
                int k = j;
                GameObject g = Instantiate(Prefab, Parent.transform);
                g.name = "(" + i + "," + j + ")";
                g.tag = "null";
                g.GetComponentInChildren<Button>().onClick.AddListener(() => onClickBtns(m, k));
                if (i == playBox - 1)
                {
                    g.GetComponentInChildren<Button>().interactable = true;
                }
                else
                {
                    g.GetComponentInChildren<Button>().interactable = false;
                }
                allBoxes[i, j] = g;
            }
        }
    }

    void onClickBtns(int m, int k)
    {
        isTurn = true;
        allBoxes[m, k].GetComponent<Image>().sprite = Red;
        allBoxes[m, k].tag = "red";
        StartCoroutine(HoldForPutBall(k, Red, "red"));
    }

    IEnumerator HoldForPutBall(int j, Sprite Ball, String Tag)
    {
        for (int i = playBox - 1; i > 0; i--)
        {
            if (allBoxes[i - 1, j].tag == "null")
            {
                yield return new WaitForSeconds(0.05f);
                allBoxes[i, j].GetComponent<Image>().sprite = null;
                allBoxes[i, j].tag = "null";
                allBoxes[i - 1, j].GetComponent<Image>().sprite = Ball;
                allBoxes[i - 1, j].tag = Tag;
            }
        }
        CheckWinFor(Tag);
        StartCoroutine(HoldForCallAI());
    }
    IEnumerator HoldForCallAI()
    {
        yield return new WaitForSeconds(1f);
        if (isTurn)
        {
            AI();
        }
    }
    void AI()
    {
        isTurn = false;
        int r = UnityEngine.Random.Range(0, playBox);
        //StartCoroutine(HoldForPutBall(r, Black, "black"));

        int Win = winBox;
        Win = 3;
        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < Win; k++, t++)
                {
                    if (allBoxes[k, i].tag == "red")
                    {
                        temp++;
                    }
                }
                t = 0;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++)
                    {
                        if (allBoxes[l, i].tag == "null")
                        {
                            r = i;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < Win; k++, t++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == Red)
                    if (allBoxes[i, k].tag == "red")
                    {
                        temp++;
                    }
                }
                t = 0;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++)
                    {
                        if (allBoxes[i, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - Win; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < Win; k++, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == Red)
                    if (allBoxes[s, k].tag == "red")
                    {
                        temp++;
                    }
                }
                t = 0;
                s = i;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++, s++)
                    {
                        if (allBoxes[s, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - Win; i++)
        {
            for (int j = playBox - 1; j >= Win - 1; j--)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < Win; k--, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == Red)
                    if (allBoxes[s, k].tag == "red")
                    {
                        temp++;
                    }
                }
                t = 0;
                s = i;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l--, t++, s++)
                    {
                        if (allBoxes[s, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < Win; k++, t++)
                {
                    if (allBoxes[k, i].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++)
                    {
                        if (allBoxes[l, i].tag == "null")
                        {
                            r = i;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < Win; k++, t++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[i, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++)
                    {
                        if (allBoxes[i, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - Win; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < Win; k++, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[s, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                s = i;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++, s++)
                    {
                        if (allBoxes[s, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - Win; i++)
        {
            for (int j = playBox - 1; j >= Win - 1; j--)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < Win; k--, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[s, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                s = i;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l--, t++, s++)
                    {
                        if (allBoxes[s, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        Win--;





        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < Win; k++, t++)
                {
                    if (allBoxes[k, i].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++)
                    {
                        if (allBoxes[l, i].tag == "null")
                        {
                            r = i;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < Win; k++, t++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[i, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++)
                    {
                        if (allBoxes[i, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - Win; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < Win; k++, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[s, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                s = i;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++, s++)
                    {
                        if (allBoxes[s, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - Win; i++)
        {
            for (int j = playBox - 1; j >= Win - 1; j--)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < Win; k--, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[s, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                s = i;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l--, t++, s++)
                    {
                        if (allBoxes[s, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        Win--;





        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < Win; k++, t++)
                {
                    if (allBoxes[k, i].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++)
                    {
                        if (allBoxes[l, i].tag == "null")
                        {
                            r = i;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < Win; k++, t++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[i, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++)
                    {
                        if (allBoxes[i, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - Win; i++)
        {
            for (int j = 0; j <= playBox - Win; j++)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < Win; k++, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[s, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                s = i;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l++, t++, s++)
                    {
                        if (allBoxes[s, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - Win; i++)
        {
            for (int j = playBox - 1; j >= Win - 1; j--)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < Win; k--, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == black)
                    if (allBoxes[s, k].tag == "black")
                    {
                        temp++;
                    }
                }
                t = 0;
                s = i;
                if (temp == Win - 1)
                {
                    t = 0;
                    for (int l = j; t < Win; l--, t++, s++)
                    {
                        if (allBoxes[s, l].tag == "null")
                        {
                            r = l;
                            allBoxes[playBox - 1, r].GetComponent<Image>().sprite = Black;
                            StartCoroutine(HoldForPutBall(r, Black, "black"));
                            return;
                        }
                    }
                }
            }
        }
        Win--;

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void CheckWinFor(String Tag)
    {
        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - winBox; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < winBox; k++, t++)
                {
                    if (allBoxes[i, k].tag == Tag)
                    {
                        temp++;
                    }
                }
                if (temp == winBox)
                {
                    t = 0;
                    for (int k = j; t < winBox; k++, t++)
                    {
                        if (allBoxes[i, k].tag == Tag)
                        {
                            allBoxes[i, k].GetComponent<Image>().sprite = Green;
                            //allBoxes[i, k].tag = "win";
                            Debug.Log(Tag + " Win1");
                        }
                    }
                }
            }
        }
        for (int i = 0; i < playBox; i++)
        {
            for (int j = 0; j <= playBox - winBox; j++)
            {
                int t = 0;
                int temp = 0;
                for (int k = j; t < winBox; k++, t++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == Red)
                    if (allBoxes[k, i].tag == Tag)
                    {
                        temp++;
                    }
                }
                if (temp == winBox)
                {
                    t = 0;
                    for (int k = j; t < winBox; k++, t++)
                    {
                        if (allBoxes[k, i].tag == Tag)
                        {
                            allBoxes[k, i].GetComponent<Image>().sprite = Green;
                            //allBoxes[i, k].tag = "win";
                            Debug.Log(Tag + " Win2");
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - winBox; i++)
        {
            for (int j = 0; j <= playBox - winBox; j++)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < winBox; k++, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == Red)
                    if (allBoxes[s, k].tag == Tag)
                    {
                        temp++;
                    }
                }
                if (temp == winBox)
                {
                    t = 0;
                    s = i;
                    for (int k = j; t < winBox; k++, t++, s++)
                    {
                        if (allBoxes[s, k].tag == Tag)
                        {
                            allBoxes[s, k].GetComponent<Image>().sprite = Green;
                            //allBoxes[s, k].tag = "win";
                            Debug.Log(Tag + " Win3");
                        }
                    }
                }
            }
        }
        for (int i = 0; i <= playBox - winBox; i++)
        {
            for (int j = playBox - 1; j >= winBox - 1; j--)
            {
                int t = 0;
                int temp = 0;
                int s = i;
                for (int k = j; t < winBox; k--, t++, s++)
                {
                    //if (allBoxes[i, k].GetComponent<Image>().sprite == Red)
                    if (allBoxes[s, k].tag == Tag)
                    {
                        temp++;
                    }
                }
                if (temp == winBox)
                {
                    t = 0;
                    s = i;
                    for (int k = j; t < winBox; k--, t++, s++)
                    {
                        if (allBoxes[s, k].tag == Tag)
                        {
                            allBoxes[s, k].GetComponent<Image>().sprite = Green;
                            //allBoxes[s, k].tag = "win";
                            Debug.Log(Tag + " Win4");
                        }
                    }
                }
            }
        }
    }

    void AfterWin()
    {

    }
}
