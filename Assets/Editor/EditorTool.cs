using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;

public class EditorTool
{
    static string path = "Assets/Animation/fishhook.controller";
    static AnimatorController animator = AssetDatabase.LoadAssetAtPath(path, typeof(AnimatorController)) as AnimatorController;

    [MenuItem("Tools/ChangeAnimSpeed/fishhook_down")]
    static void fishhook_down()
    {
        SetAniamtionSpeed(animator, AnimationName.fishhook_down, 0.5f);
        SetAniamtionSpeed(animator, AnimationName.fishhook_up, 0.5f);
    }
    [MenuItem("Tools/ChangeAnimSpeed/fishhook_get")]
    static void fishhook_get()
    {
        SetAniamtionSpeed(animator, AnimationName.fishhook_get, 1f);
        SetAniamtionSpeed(animator, AnimationName.fishhook_letgo, 2f);
    }

    static void SetAniamtionSpeed(AnimatorController ac, AnimationName name, float speed)
    {
        //AnimatorController ac = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        AnimatorControllerLayer[] layers = ac.layers;
        AnimatorStateMachine state = layers[0].stateMachine;
        ChildAnimatorState[] sts = state.states;
        for (int i = 0; i < sts.Length; i++)
        {
            if (name.ToString() == sts[i].state.name)
            {
                sts[i].state.speed = speed;
                Debug.Log(name + "------修改成功");
                break;
            }
        }
    }




    //打包语音数据包
    [MenuItem("Tools/BuildAssetScripteObj")]
    static void BuildAssetBundlesExcell()
    {
        ExcelScriptObj es = ScriptableObject.CreateInstance<ExcelScriptObj>();
        es.voiceData = ExcelAccess.SelectTables(ExcelAccess.ExcelName);
        es.voiceType = ExcelAccess.SelectTables(ExcelAccess.ExcelType);
        if (File.Exists(holderPath))
            File.Delete(holderPath);
        AssetDatabase.CreateAsset(es, holderPath);
        Debug.Log("Build ScripteObj Success");
        AssetDatabase.Refresh();
    }

    static string holderPath
    {
        get { return "Assets/Resources/voiceNames.asset"; }
    }


}
