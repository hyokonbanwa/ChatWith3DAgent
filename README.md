### Chat With 3DAgent 概要
- ChatGPT(gpt-3.5-turbo-instruct)を使用した狐娘AIエージェントと会話ができるアプリです。
- ※2023年1月頃に作成したアプリ。当時は言語モデルとして「text-davinci-03」を使用していたが、2024年1月に廃止されたため現在は「gpt-3.5-turbo-instruct」を用いている。
- ※プロジェクトに有料のライブラリが含まれるためコードは一部公開とする。

### 実際のプレイ画面

<video src="https://github.com/hyokonbanwa/ChatWith3DAgent/assets/84362902/62ba7314-71c5-4c8c-8522-531bf6467fc4"></video>


### アプリ実行方法
1. OpenAI API Keyを取得する（参考：Value Note様の記事：[https://www.value-domain.com/media/openai/](https://www.value-domain.com/media/openai/#:~:text=0.0200-,OpenAI%E3%81%AEAPI%E3%81%AE%E6%BA%96%E5%82%99%EF%BC%88API%E3%82%AD%E3%83%BC%E3%81%AE%E5%8F%96%E5%BE%97%EF%BC%89,-%E3%81%BE%E3%81%9A%E3%81%AF%E3%80%81OpenAI%20API) ) 
1. Google Natural Language APIを有効化し、Google Cloud API Keyを取得する。 (参考：光岡 高宏様の記事：https://zenn.dev/tmitsuoka0423/articles/get-gcp-api-key)
1. [VOICEVOX](https://voicevox.hiroshiba.jp/)をダウンロード&起動(参考チュートリアル：https://voicevox.hiroshiba.jp/how_to_use/)
1. リポジトリをクローンする(git clone https://github.com/hyokonbanwa/ChatWith3DAgent.git）
1. クローンしたフォルダ配下の「VRMChatAgent.exe」を起動する。
1. 起動画面の「OpenApiKey」に「Open AI API Key」を入力、「GoogleNaturalLanguage ApiKey」に「Google Cloud API Key」を入力する。
1. ゲーム開始をクリックし、右下のテキストボックスにAIエージェントに話しかけたい文章を入力してください。<br>
※補足. 起動画面の「言語モデルへの命令文(Prompt)」を編集することでAIエージェントの知識・振る舞い・性格をカスタマイズできます。なおプロンプト中の{0}～{3}には内部で対応した日付の変数が代入されます。


### 使用した技術
- C# (Unity)
- PlaticSCM (バージョン管理)
- OpenAI API (テキスト生成)
- Google Cloud Natural Language API (テキスト感情分析)

### 使用したライブラリや素材とクレジット表記
- uLipSync（口パク実装）：https://github.com/hecomi/uLipSync
    - Copyright (c) 2021 hecomi
      This software is released under the MIT License.
      http://opensource.org/licenses/mit-license.php 
- BreathControler (息による動き実装)：https://mebiustos.hatenablog.com/entry/2015/08/31/201902
    - Copyright (c) 2015 Toshiaki Aizawa (https://twitter.com/xflutexx)
      This software is released under the MIT License.
      http://opensource.org/licenses/mit-license.php 
- UniVRM（Unity上でVRM形式の3Dモデルの動作させるライブラリ）: https://vrm.dev/univrm/import/univrm_import/
- 右近（使用したVRMの3Dモデル）：https://hub.vroid.com/characters/4612164211441790719/models/9051368064615794365
    - 作成者：「キツネツキ」様　(https://twitter.com/_kitsune_tsuki_)
- VOICEVOX（TextToSpeech）：https://voicevox.hiroshiba.jp/
- VeryAnimation (有料のUnityアニメーション作成アセット)：https://assetstore.unity.com/packages/tools/animation/very-animation-96826?locale=ja-JP
- NuGetForUnity ( C#ライブラリをUnityで利用可能にするアセット)：https://github.com/GlitchEnzo/NuGetForUnity

### コードについて
コードは「/code」フォルダー以下にあります。
* InitGameSystem.cs
  * 最初のシーンを管理するオブジェクトに付けるコンポーネント
* InitSceneData.cs
  * ユーザデータを格納するScriptableObjecに付けるコンポーネント
* InteractionSystem.cs
  * ユーザーの入力→ChatGPTの応答→エージェントの動作のループ処理を行う
* LogSystem.cs
  * 今までの会話履歴を表示するCanvasに付けるコンポーネント
* MakeAudioSyste.cs
  * VOICEVOXでの音声生成を管理
* GoogleNaturalLanguage.cs
  * ChatGPTの応答文の感情分析
* Humanoid/CharacterBehaivar.cs
  * VRMオブジェクトにつけるコンポーネント。キャラクターの表情とアニメーションを再生する
