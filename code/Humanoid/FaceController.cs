using System;
using UnityEngine;
using VRM;
using DG.Tweening;
using System.Collections.Generic;


public sealed class FaceController : MonoBehaviour
{
    [SerializeField]
    VRMBlendShapeProxy proxy;
    bool isBusy = false;

    //現在の表情のblendhape
    BlendShapeKey currentFaceKey;

    /// <summary>
    /// 現在の静的な適用されているkey一覧
    /// keyのvalueを動かしている間はlistからremoveし、動かし終えたらまたaddする
    /// </summary>
    /// <typeparam name="BlendShapePreset"></typeparam>
    /// <returns></returns>
    List<string> applyedStringKeys = new List<string>();//変更している場合は適用しない
    void Start()
    {
        currentFaceKey = BlendShapeKey.CreateUnknown("");
        InitFace(0);
    }

    private void Update()
    {
        // Debug.Log("あいう");
        // Debug.Log(applyedStringKeys.Count);
    }
    public void InitFace(float duration)
    {
        ChangeFace(BlendShapeKey.CreateFromPreset(BlendShapePreset.Neutral), 0, 1, duration);
    }

    // public void ButtonClick()
    // {
    //     ChangeFace(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), 0, 1, 5);
    // }
    // public void ButtonClick1()
    // {
    //     ChangeFace(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), 0, 1, 5);
    // }


    /// <summary>
    /// おもに表情変更のために使用する
    /// 一つのみの排他的なBlendshapeをセットする感覚。
    /// preとnextとで切り替え時間を引数から変更できるようにする
    /// </summary>
    /// <param name="key"></param>
    /// <param name="startValue"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    /// <param name="ease"></param>
    public void ChangeFace(BlendShapeKey nextkey, float startValue, float endValue, float duration, Ease ease = Ease.InOutQuad)
    {
        if (isBusy || nextkey.ToString() == currentFaceKey.ToString())
        {
            return;
        }
        isBusy = true;
        startValue = Mathf.Clamp01(startValue);
        endValue = Mathf.Clamp01(endValue);
        // Debug.Log(nextkey.ToString());
        // Debug.Log(nextkey.Preset);
        //現在の表情を少しづつ減算していく
        applyedStringKeys.Remove(currentFaceKey.ToString());//現在適用してる基本表情をapplysから削除する
        float preProgress = proxy.GetValue(currentFaceKey);
        BlendShapeKey preKey = currentFaceKey;
        //Dotweenは非同期に実行されるので注意（途中で値が書き変わらないようにする）
        DOTween.To(() => preProgress, x => preProgress = x, 0, duration).OnUpdate(() => { UpdateFace(preKey, preProgress); isBusy = false; }).OnComplete(() =>
        {
            //Debug.Log(applyedStringKeys[0]);
        }).SetEase(ease);



        float progress = startValue;
        DOTween.To(() => progress, x => progress = x, endValue, duration).OnUpdate(() => { UpdateFace(nextkey, progress); isBusy = false; }).OnComplete(() =>
        {
            applyedStringKeys.Add(nextkey.ToString());
            //Debug.Log(applyedStringKeys[0]);
        }).SetEase(ease);
        currentFaceKey = nextkey;

    }


    /// <summary>
    /// 表情についかしてblendshapeする感覚
    /// </summary>
    /// <param name="key"></param>
    /// <param name="startValue"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    /// <param name="ease"></param>
    public void ApplyFace(BlendShapeKey key, float startValue, float endValue, float duration, Ease ease = Ease.InOutQuad)
    {
        // if (isBusy)
        // {
        //     return;
        // }
        // isBusy = true;



        //これから動かすものは削除
        applyedStringKeys.Remove(key.ToString());

        startValue = Mathf.Clamp01(startValue);
        endValue = Mathf.Clamp01(endValue);
        //UpdateFace(currentPreset, 0);
        float progress = startValue;
        DOTween.To(() => progress, x => progress = x, endValue, duration).OnUpdate(() =>
        {
            UpdateFace(key, progress);
        }).OnComplete(() =>
        {
            //動かし終えたら追加
            applyedStringKeys.Add(key.ToString());
        }).SetEase(ease);
        //currentPreset = preset;
    }
    void UpdateFace(BlendShapeKey nextKey, float progress)
    {
        applyedStringKeys.ForEach((stringKey) =>
        {
            if (StringIsBlendShapePreset(stringKey))
            {
                BlendShapePreset preset = (BlendShapePreset)Enum.Parse(typeof(BlendShapePreset), stringKey);
                var key = BlendShapeKey.CreateFromPreset(preset);
                proxy.AccumulateValue(key, proxy.GetValue(key));
            }
            else
            {
                var key = BlendShapeKey.CreateUnknown(stringKey);
                proxy.AccumulateValue(key, proxy.GetValue(key));
            }
        });
        proxy.AccumulateValue(nextKey, progress);
        proxy.Apply();
        //Debug.Log(applyedStringKeys);
    }

    public string GetPreset()
    {
        return currentFaceKey.ToString();
    }

    //https://dobon.net/vb/dotnet/programing/enumparse.html
    private bool StringIsBlendShapePreset(string stringKey)
    {
        //DayOfWeek列挙体に変換できるか調べ、変換できれば変換する
        BlendShapePreset preset;
        if (Enum.TryParse<BlendShapePreset>(stringKey, out preset))
        {
            //変換できた時は、結果を表示する
            //Debug.Log($"{preset}はenumに存在します");
            return true;
        }
        else
        {
            //変換できなかった時
            //Debug.Log($"{stringKey}をBlendShpaePresetに変換できません");
            return false;
        }
    }

}

