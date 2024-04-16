using UnityEngine;

[CreateAssetMenu(fileName = "InitSceneData", menuName = "ScriptableObjects/InitSceneData")]
public class InitSceneData : ScriptableObject
{
    private string promptString =
@"次の箇条書きは会話の舞台設定です。
・会話の行われている年は{0}、月日は{1}、曜日は{2}です。
・天気は晴れです。
・時間帯は朝です。

次の文章はキャラクターAの台詞で口調の参考となります。キャラクターAは|A|で表されます。
|A|:わらわはのことを知りたいのか？
|A|:おぬしのことを知りたいのじゃ。
|A|:感謝するのじゃ。
|A|:それはすごいのう。
|A|:わらわも賛同するのじゃ。

次の箇条書きはキャラクターAの設定です。
・名前は「右近」。
・会話では必ず一人称は「わらわ」二人称は「おぬし」を使用する。
・語尾に「～なのじゃ」「～しておる」「などを使用し古風なしゃべり方をする。
・尊大な態度で古風な話し方をする。
・とても知的で創造的な内容を話す。
・好きな食べ物は「油揚げ」。
・北海道出身。
・年齢は500歳。
・少女のような外見で服装は和装。
・体には狐耳と狐の尻尾が一本が生えた狐娘である。

以下の会話はキャラクターAと人間Bの会話です。キャラクターAは|A|、人間Bは|B|で表されます。上記の舞台設定・口調・キャラクターAの設定を利用してキャラクターAの台詞を記述してください。";
    [SerializeField]
    public string openAI_API_KEY;

    [SerializeField]
    public string voicevoxURL;

    [SerializeField]
    public string gcpNaturalLanguage_API_KEY;

    [SerializeField, TextArea(10, 100)]
    public string basePrompt;


    public void OnEnable()
    {
        openAI_API_KEY = "";
        voicevoxURL = "http://localhost:50021";
        gcpNaturalLanguage_API_KEY = "";
        basePrompt = promptString;
    }
    public void OnDisable()
    {
        openAI_API_KEY = "";
        voicevoxURL = "http://localhost:50021";
        gcpNaturalLanguage_API_KEY = "";
        basePrompt = promptString;
    }
}
