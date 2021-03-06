﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectMrg
{

    public class EObj
    {
        public float time;
        public GameObject e;
        public EObj(GameObject o, float t)
        {
            e = o;
            time = t;
        }
    }
   // private static Dictionary<EffectType, EObj> dicEff = new Dictionary<EffectType, EObj>();
    private static Dictionary<int, GameObject> samplist = new Dictionary<int, GameObject>();
    private static string path = "Effect/";
    private static GameObject _parent;
    private static int[] effectNum = { 13, 14, 23, 24 };


    #region  特效加载
    private static GameObject GetParent()
    {
        if (!_parent) _parent = GameObject.Find("effect");
        return _parent;
    }

    //public static void PlayEffect(EffectType etype)
    //{
    //    EObj ej = FindEff(etype);
    //    if (ej == null)
    //    {
    //        GameObject temp = Load(etype);
    //        float time = ParticleSystemLength(temp.transform);
    //        ej = new EObj(temp, time);
    //        dicEff.Add(etype, ej);
    //    }
    //    ej.e.SetActive(true);
    //   // SDKManager.Instance.StartCoroutine(HideEffect(ej));
    //}

    //private static EObj FindEff(EffectType etype)
    //{
    //    EObj e;
    //    dicEff.TryGetValue(etype, out e);
    //    return e;
    //}
    //private static GameObject Load(EffectType etype)
    //{
    //    GameObject model = Resources.Load<GameObject>(path + etype.ToString());
    //    GameObject e = GameObject.Instantiate<GameObject>(model);
    //    e.transform.SetParent(GetParent().transform);
    //    e.transform.localPosition = Vector3.zero;
    //    e.transform.localScale = Vector3.one;
    //    model = null;
    //    return e;
    //}

    //private static IEnumerator HideEffect(EObj ej)
    //{
    //    yield return new WaitForSeconds(ej.time);
    //    ej.e.SetActive(false);
    //}

    //private static float ParticleSystemLength(Transform transform)
    //{
    //    ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
    //    float maxDuration = 0;
    //    foreach (ParticleSystem ps in particleSystems)
    //    {
    //        if (ps.emission.enabled)
    //        {
    //            if (ps.main.loop)
    //            {
    //                return -1f;
    //            }
    //            float dunration = 0f;
    //            if (ps.emissionRate <= 0)
    //            {
    //                dunration = ps.startDelay + ps.startLifetime;
    //            }
    //            else
    //            {
    //                dunration = ps.startDelay + Mathf.Max(ps.duration, ps.startLifetime);
    //            }
    //            if (dunration > maxDuration)
    //            {
    //                maxDuration = dunration;
    //            }
    //        }
    //    }
    //    return maxDuration;
    //}

    //public static void Clear()
    //{
    //    foreach (var item in dicEff)
    //    {
    //        GameObject.Destroy(item.Value.e);
    //    }
    //    dicEff.Clear();
    //    samplist.Clear();
    //}
    #endregion
}
