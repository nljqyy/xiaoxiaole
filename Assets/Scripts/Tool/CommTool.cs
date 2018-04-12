using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommTool
{
    public static GameObject FindObjForName(GameObject uiRoot,string name)
    {
        if (uiRoot.name == name)
            return uiRoot;
        Queue<GameObject> queue = new Queue<GameObject>();
        queue.Enqueue(uiRoot);
        GameObject temp=null;
        while (queue.Count>0)
        {
            temp= queue.Dequeue();
            if (temp.name == name)
            {
                queue = null;
                return temp;
            }
            int count= temp.transform.childCount;
            if (count>0)
            {
                for (int i = 0; i < count; i++)
                {
                   queue.Enqueue(temp.transform.GetChild(i).gameObject);
                }
            }
        }
        queue = null;
        return null;
    }

    public static T GetCompentCustom<T>(GameObject uiRoot,string name)
    {
        T t = default(T);
        GameObject obj=  FindObjForName(uiRoot, name);
        if (obj)
        {
           t= obj.GetComponent<T>();
        }
        return t;
    }

    public static GameObject InstantiateObj(GameObject model, GameObject parent, Vector3 pos, Vector3 scal, string name)
    {
        GameObject temp = null;
        temp = GameObject.Instantiate<GameObject>(model);
        temp.name = name;
        temp.transform.SetParent(parent.transform);
        temp.transform.localPosition = pos;
        temp.transform.localScale = scal;
        temp.transform.localRotation = Quaternion.identity;
        return temp;
    }

}
