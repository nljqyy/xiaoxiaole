using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class LogicMrg
{
    private LogicMrg() { }
    static LogicMrg() { _instance = new LogicMrg(); }
    private static LogicMrg _instance;
    public static LogicMrg Instance { get { return _instance; } }
    private GameObject parentbg;
    private GameObject parentfood;
    private GameObject modelbg;
    private GameObject modefood;
    private List<GameObject> list = new List<GameObject>();
    private Food[,] nums = new Food[6, 6];
    private Image temp1;
    private Image temp2;

    public void Init()
    {
        parentbg = GameObject.Find("kuang");
        modelbg = Resources.Load<GameObject>("Prefab/img_bg");
        parentfood = GameObject.Find("foods");
        modefood = Resources.Load<GameObject>("Prefab/food");
        CreateBg();
        CreateFood();
        Resources.UnloadUnusedAssets();
        GameMrg.Instance.StartIe(0.5f, StarMove);
    }

    private void CreateBg()
    {
        GameObject temp;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                temp = GameObject.Instantiate<GameObject>(modelbg);
                temp.transform.SetParent(parentbg.transform);
                temp.transform.localScale = Vector3.one;
                list.Add(temp);
            }
        }
    }

    private void CreateFood()
    {
        GameObject temp;
        Image img;
        Food food;
        Food last;
        Food next;
        Food left;
        Food right;
        int x, y;
        for (int i = 0; i < 2; i++)
        {
            temp = GameObject.Instantiate<GameObject>(modefood);
            temp.transform.SetParent(parentfood.transform);
            temp.transform.localScale = Vector3.one;
            temp.SetActive(false);
            if (i == 0) temp1 = temp.GetComponent<Image>();
            else
                temp2 = temp.GetComponent<Image>();
        }
        for (int i = 0; i < 6; i++)
        {
            x = i * 60;
            for (int j = 0; j < 6; j++)
            {
                y = j * -60;
                temp = GameObject.Instantiate<GameObject>(modefood);
                temp.transform.SetParent(parentfood.transform);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = new Vector3(x, 0, 0);
                temp.name = i + "|" + j;
                img = temp.GetComponent<Image>();
                img.gameObject.SetActive(false);
                food = new Food();
                nums[i, j] = food;
                food.temp1 = temp1;
                food.temp2 = temp2;
                food.self = temp;
                food.img = temp.GetComponent<Image>();
                food.x_index = i;
                food.y_index = j;
                food.x_pos = x;
                food.y_pos = y;
                food.RandomSprite();
                UIEventLisener.Get(temp).OnUp += food.PointerUp;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                nums[i,j].last = j == 0 ? null : nums[i, j - 1];
                nums[i, j].next = j == 5 ? null : nums[i, j + 1];
                nums[i, j].left = i == 0 ? null : nums[i - 1, j];
                nums[i, j].right = i == 5 ? null : nums[i + 1, j];
            }
        }
    }

    private void StarMove()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (nums[i, j] != null)
                    nums[i, j].InitMove();
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (nums[i, j] != null)
                    GameObject.Destroy(nums[i, j].img.gameObject);
            }
        }
        foreach (var item in list)
        {
            GameObject.Destroy(item);
        }
        list.Clear();
        Resources.UnloadUnusedAssets();
    }
}
