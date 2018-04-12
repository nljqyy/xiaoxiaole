using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class LogicMrg
{
    private GameObject parent;
    private GameObject model;
    private List<GameObject> list = new List<GameObject>();

    private Food[,] nums = new Food[6, 6];

    public void Init()
    {
        parent = GameObject.Find("kuang");
        model = Resources.Load<GameObject>("Prefab/img_bg");
        if (model)
        {
            GameObject temp;
            Image img;
            Food food;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    temp = GameObject.Instantiate<GameObject>(model);
                    temp.transform.SetParent(parent.transform);
                    img = temp.transform.Find("game_e").GetComponent<Image>();
                    img.gameObject.SetActive(false);
                    food = new Food();
                    food.self = img;
                    food.last = i == 0 ? null : nums[i - 1, j];
                    food.next = i == 5 ? null : nums[i + 1, j];
                    food.left = j == 0 ? null : nums[i, j - 1];
                    food.right = j == 5 ? null : nums[i, j + 1];
                    food.x = i;
                    food.y = j;
                    nums[i, j] = food;
                    list.Add(temp);
                }
            }
        }
    }
}
