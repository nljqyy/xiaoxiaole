﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class UIDataBase : MonoBehaviour
{
    public string DlgName
    {
        get { return gameObject.name; }
    }
    public abstract UIShowPos ShowPos
    {
        get;
    }

    public abstract HidePage hidePage
    {
        get;
    }

    private void Awake()
    {
        Action<GameObject> action = obj =>
          {
              obj.transform.parent = gameObject.transform;
              obj.transform.localPosition = Vector3.zero;
              obj.transform.localScale = Vector3.one;
              obj.transform.localRotation = Quaternion.identity;
              Init();
          };

        StartCoroutine(LoadGameObj(action));
    }
    IEnumerator LoadGameObj(Action<GameObject> act)
    {
        if (!string.IsNullOrEmpty(DlgName))
        {
            Debug.Log("----实列化ui预制物体");
            string uipath = "UIPrefab/" + DlgName;
            GameObject obj = Resources.Load<GameObject>(uipath);
            if (obj)
            {
                GameObject temp = Instantiate<GameObject>(obj);
                if (act != null && temp)
                {
                    act(temp);
                }
            }
        }
        else
        {
            Debug.LogError("DlgName是空");
        }
        yield break;
    }

    public virtual void Init()
    {
        Debug.Log("----界面初始化------" + DlgName);
    }
    public virtual void OnOpen()
    {

    }
    public virtual void OnShow(object data)
    {
    }
    public virtual void OnHide()
    {
    }

}
