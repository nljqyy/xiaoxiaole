using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class Food
{
    public Food last;
    public Food next;
    public Food left;
    public Food right;
    public FoodType ftype;
    public int x;
    public int y;
    public Image self;
   
}
