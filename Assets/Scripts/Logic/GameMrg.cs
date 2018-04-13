
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMrg : MonoBehaviour {

    public static GameMrg Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {

        LogicMrg.Instance.Init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartIe(float time,Action act)
    {
        StopAllCoroutines();
        StartCoroutine(Ie(time,act));
    }
    IEnumerator Ie(float time, Action act)
    {
        yield return new WaitForSeconds(time);
        if (act != null) act();
    }
}
