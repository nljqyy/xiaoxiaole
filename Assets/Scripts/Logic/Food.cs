using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public sealed class Food
{
    public Food last;
    public Food next;
    public Food left;
    public Food right;
    public FoodType ftype = FoodType.None;
    public int x_index;
    public int y_index;
    public float x_pos;
    public float y_pos;
    public Image img;
    private GameObject self;
    private float needTime = 0.3f;

    public Food(Food last, Food next, Food left, Food right, Image img, int x, int y, float posX, float posY)
    {
        this.last = last;
        this.next = next;
        this.left = left;
        this.right = right;
        this.img = img;
        this.x_index = x;
        this.y_index = y;
        this.x_pos = posX;
        this.y_pos = posY;
        this.self = img.gameObject;
        RandomSprite();
    }

    public void InitMove()
    {
        if (IsConnect())//需更换图片
            RandomSprite();
        self.SetActive(true);
        self.transform.DOLocalMoveY(y_pos, x_index * needTime).OnComplete(() =>
        {

        });
    }

    public void MoveDown()
    {
        //if (next != null && next.ftype == FoodType.None)
        //{
        //    self.transform.DOMoveY(next.self.transform.position.y, needTime).OnComplete(() =>
        //    {
        //        if (last != null)
        //        {
        //            last.MoveDown();
        //        }

        //    });
        //}
    }

    public bool IsConnect()
    {
        int countlast = Compute(last, Dir.Up, 1);
        int countnext = Compute(next, Dir.Down, 1);
        int countleft = Compute(left, Dir.Left, 1);
        int countright = Compute(right, Dir.Right, 1);
        if (countlast >= 3 || countnext >= 3 || countleft >= 3 || countright >= 3)
        {
            return true;
        }
        return false;
    }
    public int Compute(Food f, Dir dir, int count)
    {
        if (f != null)
        {
            if (f.ftype == ftype)
            {
                count++;
                Food ft = null;
                switch (dir)
                {
                    case Dir.Down: ft = f.next; break;
                    case Dir.Up: ft = f.last; break;
                    case Dir.Left: ft = f.left; break;
                    case Dir.Right: ft = f.right; break;
                }
                count = Compute(ft, dir, count);
            }

        }
        return count;
    }

    public void RandomSprite()
    {
        int type = (int)ftype;
        while (type == (int)ftype)
        {
            type = UnityEngine.Random.Range(1, 8);
        }
        ftype = (FoodType)type;
        img.sprite = UIAtlasManager.Instance.LoadSprite(UIAtlasName.UIMain, ftype.ToString());
    }

    public void PointerUp(GameObject o)
    {
        string[] strs = o.name.Split('|');
        int xx = Convert.ToInt32(strs[0]);
        int yy = Convert.ToInt32(strs[1]);
        Food temp = this;
        Debug.Log("Current---" + img.gameObject.name + "---UpObj---" + o.name);
        if (x_index == xx + 1 && left.ftype != ftype)
        {
            self.transform.DOLocalMoveX(left.x_pos, needTime);
            left.self.transform.DOLocalMoveX(x_pos, needTime);
        }
        else if (x_index == xx - 1 && right.ftype != ftype)
        {
            self.transform.DOLocalMoveX(right.x_pos, needTime);
            right.self.transform.DOLocalMoveX(x_pos, needTime);
            this.self = right.self;
            this.ftype = right.ftype;
            right.self = temp.self;
            right.ftype = temp.ftype;
        }
        if (y_index == yy + 1 && last.ftype != ftype)
        {
            self.transform.DOLocalMoveY(last.y_pos, needTime);
            last.self.transform.DOLocalMoveY(y_pos, needTime);
        }
        else if (y_index == yy - 1 && next.ftype != ftype)
        {
            self.transform.DOLocalMoveY(next.y_pos, needTime);
            next.self.transform.DOLocalMoveY(y_pos, needTime);
        }
    }
}
