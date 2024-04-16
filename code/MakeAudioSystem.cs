using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;


public class MakeAudioSystem
{
    /// <summary>
    /// APIエンドポイント
    /// </summary>
    private string API_END_POINT;
    //const string API_QUERY = "http://localhost:50021/audio_query";
    //const string API_SYNTHESIS = "http://localhost:50021/synthesis";

    public MakeAudioSystem(string serverURL)
    {
        API_END_POINT = serverURL;
    }


    //このアシスタントはゲームエンジンUnity内部のHumanoid型キャラクターに搭載されています。
    public async UniTask<AudioClip> MakeClip(int id, string text)
    {
        //レスポンス取得
        var response = await SendQuery(id, text);
        //Debug.Log(response);
        var audioClip = await MakeAudio(id, response);
        //Debug.Log(audioClip);
        //string path = string.Format ("{0}/{1}", Application.persistentDataPath, "recording.wav");
        //AudioClip audioClip = WavUtility.ToAudioClip(audio, 0, name = "recording.wav");
        //Debug.Log("audioClip");
        return audioClip;

    }
    // 終了ボタン
    // QuitButton.onClick.AddListener(() =>
    // {
    //     Application.Quit();
    // });


    //     public async void InitChat()
    //     {
    //         string prompt =

    // ;
    //         //初回実行
    //         //レスポンス取得
    //         var response = await GetAPIResponse(prompt);
    //         //レスポンスからテキスト取得
    //         string outputText = response.Choices.FirstOrDefault().Text;
    //         Output.text = outputText.TrimStart('\n');
    //         Debug.Log(outputText);

    //     }

    /// <summary>
    /// APIからレスポンス取得
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private async UniTask<string> SendQuery(int id, string text)
    {
        VOICEVOXQuery query = new()
        {
            Text = text,
            Speaker = id, //レスポンスのテキストが途切れる場合、こちらを変更する
        };

        string requestJson = JsonConvert.SerializeObject(query, Formatting.Indented);
        //Debug.Log(requestJson);

        // POSTするデータ
        byte[] data = System.Text.Encoding.UTF8.GetBytes(requestJson);


        string jsonString = null;
        // Getパラメーター部分の作成
        var queryString = System.Web.HttpUtility.ParseQueryString("");
        queryString.Add("text", text);
        queryString.Add("speaker", id.ToString());

        // URIとクエリストリングをマージ
        var uriBuilder = new System.UriBuilder(API_END_POINT)
        {
            Path = "audio_query",
            Query = queryString.ToString()
        };
        // POSTリクエストを送信
        using (UnityWebRequest request = new UnityWebRequest(uriBuilder.Uri, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(data);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            await request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.InProgress:
                    //Debug.Log("リクエスト中");
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log("リクエスト成功");
                    jsonString = request.downloadHandler.text;
                    // // レスポンスデータを表示
                    // Debug.Log(jsonString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }

        return jsonString;
    }
    /// <summary>
    /// APIからレスポンス取得
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private async UniTask<AudioClip> MakeAudio(int id, string jsonBody)
    {

        string requestJson = jsonBody;
        //Debug.Log(requestJson);

        // POSTするデータ
        byte[] data = System.Text.Encoding.UTF8.GetBytes(requestJson);


        AudioClip clip = null;
        // Getパラメーター部分の作成
        var queryString = System.Web.HttpUtility.ParseQueryString("");
        queryString.Add("speaker", id.ToString());

        // URIとクエリストリングをマージ
        var uriBuilder = new System.UriBuilder(API_END_POINT)
        {
            Path = "synthesis",
            Query = queryString.ToString()
        };
        // POSTリクエストを送信
        using (UnityWebRequest request = new UnityWebRequest(uriBuilder.Uri, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(data);
            request.downloadHandler = new DownloadHandlerAudioClip(uriBuilder.Uri, AudioType.WAV);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("accept", "audio/wav");
            request.SetRequestHeader("responseType", "stream");
            await request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.InProgress:
                    //Debug.Log("リクエスト中");
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log("リクエスト成功");
                    clip = ((DownloadHandlerAudioClip)request.downloadHandler).audioClip;
                    // jsonString = request.downloadHandler.data;
                    // // レスポンスデータを表示
                    // Debug.Log(jsonString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }

        // デシリアライズ

        return clip;
    }

    // private async UniTask<UnityWebRequest> Query(string text)
    // {
    //     // Getパラメーター部分の作成
    //     var queryString = System.Web.HttpUtility.ParseQueryString("");
    //     queryString.Add("text", text);
    //     queryString.Add("speaker", 1.ToString());

    //     // URIとクエリストリングをマージ
    //     var uriBuilder = new System.UriBuilder(API_QUERY)
    //     {
    //         Query = queryString.ToString()
    //     };

    //     Debug.Log(uriBuilder.Uri);

    //     // Getの実行
    //     UnityWebRequest request = UnityWebRequest.Get(uriBuilder.Uri);
    //     await request.SendWebRequest();
    //     return request;
    // }
}
