using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class LogSystem : MonoBehaviour
{
    //ダンジョンログ
    [SerializeField]
    private Text combat_log_text;
    private string log_history = "会話ログ\n";

    void Start()
    {
        combat_log_text.text = log_history;
        //combat_log_textという名前のオブジェクトを探してテキストコーポネントを格納
        //combat_log_text = GameObject.Find("combat_log_text").GetComponent<Text>();

        // //このようにテキストを追加する。
        // string text = "テキストが変更された\n";

        // //テキストを反映させる
        // AddDungeonLog(text);
    }

    //ダンジョンログを更新する
    public void AddDungeonLog(string text)
    {
        log_history += text + "\n";
        combat_log_text.text = log_history;
    }

    public void ResetDungeonLog(string text)
    {
        log_history = text;
        combat_log_text.text = log_history;

    }

}//end
