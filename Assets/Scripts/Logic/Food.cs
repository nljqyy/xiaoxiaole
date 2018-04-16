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
    public GameObject self;
    public Image temp1;
    public Image temp2;
    private float needTime = 0.3f;
    private bool isHide = false;
    private Dir dirHide;//隐藏分类
    private List<Food> list = new List<Food>();//需要销毁的
    public void InitMove()
    {
        Dir dir = IsConnect();
        if (IsConnect() != Dir.None)//需更换图片
            RandomSprite();
        self.SetActive(true);
        self.transform.DOLocalMoveY(y_pos, x_index * needTime);
    }

    public void MovePos(Dir dir)
    {
        if (dir == Dir.Down)
        {
            if (next != null)
            {
                if (!isHide)
                {
                    self.transform.DOLocalMoveY(next.y_pos, needTime).OnComplete(() =>
                     {
                         if (!next.self.activeSelf)
                         {
                             next.MovePos(dir);
                         }

                     });
                }
                else
                {
                    last.MovePos(dir);
                }
            }

        }
    }

    public Dir IsConnect()
    {
        list.Clear();
        int lastnext = Compute(last, Dir.Up, 1);
        int leftright = Compute(left, Dir.Left, 1);
        Dir dir = Dir.None;
        if (lastnext >= 3)
            dir = dir | Dir.Up;
        if (leftright >= 3)
            dir = dir | Dir.Left;
        return dir;
    }
    public int Compute(Food f, Dir dir, int count)
    {
        Food ft = null;
        if (f != null)
        {
            if (f.ftype == ftype)
            {
                count++;
                switch (dir)
                {
                    case Dir.Down: ft = f.next; f.dirHide = Dir.Up; break;
                    case Dir.Up: ft = f.last; dirHide = Dir.Up; break;
                    case Dir.Left: ft = f.left; dirHide = Dir.Left; break;
                    case Dir.Right: ft = f.right; dirHide = Dir.Left; break;
                }
                list.Add(f);
                return Compute(ft, dir, count);
            }
        }
        if (dir == Dir.Up)
        {
            ft = next;
            dir = Dir.Down;
            return Compute(ft, dir, count);
        }
        if (dir == Dir.Left)
        {
            ft = right;
            dir = Dir.Right;
            return Compute(ft, dir, count);
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
        if (o.name == img.name) return;
        string[] strs = o.name.Split('|');
        int xx = Convert.ToInt32(strs[0]);
        int yy = Convert.ToInt32(strs[1]);
        Debug.Log("Current---" + img.gameObject.name + "---UpObj---" + o.name);
        if (x_index == xx + 1 && left.ftype != ftype)//左移动
        {
            Change(left, temp1.transform.DOLocalMoveX(left.x_pos, needTime).Pause(), temp2.transform.DOLocalMoveX(x_pos, needTime).Pause());
        }
        else if (x_index == xx - 1 && right.ftype != ftype)//右移动
        {
            Change(right, temp1.transform.DOLocalMoveX(right.x_pos, needTime).Pause(), temp2.transform.DOLocalMoveX(x_pos, needTime).Pause());
        }
        if (y_index == yy + 1 && last.ftype != ftype)//上移
        {
            Change(last, temp1.transform.DOLocalMoveY(last.y_pos, needTime).Pause(), temp2.transform.DOLocalMoveY(y_pos, needTime).Pause());
        }
        else if (y_index == yy - 1 && next.ftype != ftype)//下移
        {
            Change(next, temp1.transform.DOLocalMoveY(next.y_pos, needTime).Pause(), temp2.transform.DOLocalMoveY(y_pos, needTime).Pause());
        }
    }


    private void Change(Food other, Tweener t1, Tweener t2)
    {
        self.SetActive(false);
        other.self.SetActive(false);
        FoodType ft = ftype;
        temp1.sprite = img.sprite;
        temp2.sprite = other.img.sprite;
        img.sprite = other.img.sprite;
        other.img.sprite = temp1.sprite;
        ftype = other.ftype;
        other.ftype = ft;
        temp1.transform.localPosition = self.transform.localPosition;
        temp2.transform.localPosition = other.self.transform.localPosition;
        temp1.gameObject.SetActive(true);
        temp2.gameObject.SetActive(true);
        t1.PlayForward();
        t2.PlayForward();
        t2.OnComplete(() =>
        {
            self.SetActive(true);
            other.self.SetActive(true);
            temp1.gameObject.SetActive(false);
            temp2.gameObject.SetActive(false);
            other.HideTheSame();
        });
    }

    public void HideTheSame()
    {
        Dir dir = IsConnect();
        if ((dir & Dir.Left) != 0)
        {
            self.SetActive(false);
            list.ForEach(e => { if (e.dirHide == Dir.Left) e.self.SetActive(false); });
            isHide = true;
        }
        if ((dir & Dir.Up) != 0)
        {
            self.SetActive(false);
            list.ForEach(e => { if (e.dirHide == Dir.Up) e.self.SetActive(false); });
            isHide = true;
            //if (last != null) last.MovePos(Dir.Down);
        }
    }


}
