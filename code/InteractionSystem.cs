using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using VRM;
using Google.Cloud.Language.V1;
using UnityEngine.SceneManagement;
using System.Threading;

public class InteractionSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private InputField input;
    [SerializeField]
    private Button inputButton;

    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private GameObject logObj;
    private LogSystem logSystem;

    [SerializeField]
    private GameObject characterObject;
    private CharacterBehaivar characterBehaivar;

    [SerializeField]
    private InitSceneData data;
    [SerializeField]
    private string openAI_API_KEY;

    [SerializeField]
    private string voicevoxURL;

    [SerializeField]
    private string gcpNaturalLanguage_API_KEY;

    [SerializeField, TextArea(10, 100)]
    private string basePrompt;

    private bool systemLock;

    private ChatSystem chatSystem;
    private MakeAudioSystem makeAudioSystem;

    private GoogleNaturalLanguage googleNaturalLanguage;
    //private Text //buttonText;




    void Start()
    {
        //         string tmp = @"
        // 以下は、とある人工知能を搭載したAIアシスタントとの会話です。このアシスタントは、親切で、創造的で、賢くて、とてもフレンドリーです。

        // Human:こんにちは、あなたは誰ですか？
        // AI:私の名前はアシスタントです。";

        //basePrompt = basePrompt != "" ? basePrompt : tmp;
        CancellationTokenSource cts = null;
        //buttonText = inputButton.GetComponentInChildren<Text>();

        openAI_API_KEY = data.openAI_API_KEY;
        voicevoxURL = data.voicevoxURL;
        gcpNaturalLanguage_API_KEY = data.gcpNaturalLanguage_API_KEY;
        basePrompt = data.basePrompt;

        chatSystem = new ChatSystem(openAI_API_KEY, basePrompt);
        chatSystem.onPromptCreated += (prompt) =>
        {
            //時間を取得
            var TodayNow = DateTime.Now;
            //テキストUIに年・月・日・秒を表示させる
            string year = TodayNow.Year.ToString() + "年 ";
            string monthDay = TodayNow.Month.ToString() + "月" + TodayNow.Day.ToString() + "日";

            // 曜日を取得(DayOfWeek列挙型)
            DayOfWeek result = TodayNow.DayOfWeek;
            // DayOfWeek列挙型を日本語に
            string[] weeks = { "日", "月", "火", "水", "木", "金", "土" };
            string week = weeks[(int)result] + "曜日";
            return String.Format(prompt, year, monthDay, week);
        };

        makeAudioSystem = new MakeAudioSystem(voicevoxURL);
        googleNaturalLanguage = new GoogleNaturalLanguage(gcpNaturalLanguage_API_KEY);
        characterBehaivar = characterObject.GetComponent<CharacterBehaivar>();
        logSystem = logObj.GetComponent<LogSystem>();
        inputButton.onClick.AddListener(() =>
        {
            //buttonText.text = "1";
            if (systemLock == true) return;
            //buttonText.text = "2";
            //前回のタスクをリセットする
            cts?.Cancel();
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            SystemStart(token);
            input.text = "";
        });

        quitButton.onClick.AddListener(() =>
        {
            cts?.Cancel();
            SceneManager.LoadScene("InitScene");

            // #if UNITY_EDITOR
            //             UnityEditor.EditorApplication.isPlaying = false;
            // #else
            //       Application.Quit();
            // #endif
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    async UniTask SystemStart(CancellationToken token)
    {

        string userInputText = input.text;
        //buttonText.text = "3";
        if (string.IsNullOrEmpty(userInputText)) return;
        systemLock = true;
        //buttonText.text = "4";
        logSystem.AddDungeonLog("あなた：" + userInputText);
        string agentReturnText = await chatSystem.sendChat(userInputText);
        logSystem.AddDungeonLog("エージェント：" + agentReturnText + Environment.NewLine);


        Document document = Document.FromPlainText(agentReturnText);
        //buttonText.text = "5";
        var response = googleNaturalLanguage.AnalyzeSentiment(document);
        //buttonText.text = "6";
        var (voicevoxNum, changeKey) = MakeExpressionFromResponse(response);


        //buttonText.text = "7";
        var audioClip = await makeAudioSystem.MakeClip(voicevoxNum, agentReturnText);
        //buttonText.text = "8";
        //BlendShapeKey changeKey = BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun);
        characterBehaivar.SetAudioCLip(audioClip);
        characterBehaivar.ChangeFace(changeKey, 0, 1, 1);
        await characterBehaivar.Speak();
        // changeKey = BlendShapeKey.CreateUnknown("bikkuri");
        // characterBehaivar.ChangeFace(changeKey, 0, 1, 1);
        systemLock = false;
        DecayEmotion(token);//感情減衰
    }

    private async UniTask DecayEmotion(CancellationToken token)
    {
        await UniTask.Delay(5000, cancellationToken: token);
        BlendShapeKey changeKey = BlendShapeKey.CreateFromPreset(BlendShapePreset.Neutral);
        characterBehaivar.ChangeFace(changeKey, 0, 1, 2);

    }

    private (int, BlendShapeKey) MakeExpressionFromResponse(AnalyzeSentimentResponse response)
    {
        float score = response.DocumentSentiment.Score;
        float magnitude = response.DocumentSentiment.Magnitude;
        BlendShapeKey expressionShapeKey;
        int voicevoxNum;
        if (score >= 0.5 || (score >= 0.3 && magnitude >= 0.5))
        {
            expressionShapeKey = BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun);
            voicevoxNum = 0;
        }
        else if (score <= -0.5 || (score <= -0.3 && magnitude >= 0.5))
        {
            expressionShapeKey = BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow);
            voicevoxNum = 37;
        }
        else if (magnitude >= 1)
        {
            expressionShapeKey = BlendShapeKey.CreateUnknown("bikkuri");
            voicevoxNum = 6;
        }
        else
        {
            expressionShapeKey = BlendShapeKey.CreateFromPreset(BlendShapePreset.Neutral);
            voicevoxNum = 2;
        }
        Debug.Log($"score:{score}, magnitude:{magnitude}");
        Debug.Log($"blendshape:{expressionShapeKey.ToString()}, voicevoxNum:{voicevoxNum}");

        return (voicevoxNum, expressionShapeKey);
    }


}
