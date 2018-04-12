using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIAtlasManager
{
    private const string path = "UIAtlas/";
    private static UIAtlasManager instance;
    public static UIAtlasManager Instance
    {
        get { return instance; }
    }
    private UIAtlasManager()
    { }
    static UIAtlasManager()
    {
        instance = new UIAtlasManager();
    }
    private Dictionary<string, Sprite[]> atlasDic = new Dictionary<string, Sprite[]>();

    public Sprite LoadSprite(string atlasName, string spriteName)
    {
        Sprite sp = FindSprite(atlasName, spriteName);
        if (sp == null)
        {
          string newPath = path + atlasName;
            UnityEngine.Object[] _atlas = Resources.LoadAll(newPath);
            Sprite[] sps=Resources.LoadAll<Sprite>(newPath);
          sp= GetSpriteForAtlas(sps, spriteName);
           atlasDic.Add(atlasName, sps);
        }
        return sp;
    }



    private Sprite FindSprite(string atlasName, string spriteName)
    {
        if (atlasDic.ContainsKey(atlasName))
        {
            Sprite[] sp = atlasDic[atlasName];
            return GetSpriteForAtlas(sp, spriteName);
        }
        return null;
    }

    private Sprite GetSpriteForAtlas(Sprite[] sps, string spriteName)
    {
        for (int i = 0; i < sps.Length; i++)
        {
            if (sps[i].GetType() == typeof(Sprite))
            {
                if (sps[i].name == spriteName)
                    return sps[i];
            }
        }
        Debug.LogWarning("图片名:" + spriteName + ";在图集中找不到");
        return null;
    }

    public void Clear()
    {
        atlasDic.Clear();
    }
}
