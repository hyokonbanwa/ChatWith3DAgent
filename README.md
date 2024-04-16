### Chat With 3DAgent 概要
- ChatGPT(gpt-3.5-turbo-instruct)を使用した狐娘AIエージェントと会話ができるアプリです。
- ※2023年1月頃に作成したアプリ。当時は言語モデルとして「text-davinci-03」を使用していたが、2024年1月に廃止されたため現在は「gpt-3.5-turbo-instruct」を用いている。
- 　プロジェクトに有料のライブラリが含まれるためコードは一部公開とする。

### 実際のプレイ画面

<video src="https://github.com/hyokonbanwa/ChatWith3DAgent/assets/84362902/62ba7314-71c5-4c8c-8522-531bf6467fc4"></video>


### アプリ実行方法
１．

２．
  



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

/**
BreathController

Copyright (c) 2015 Toshiaki Aizawa (https://twitter.com/xflutexx)

This software is released under the MIT License.
 http://opensource.org/licenses/mit-license.php …
*/

### コードについて
InitGameSystem.cs
最初のシーンを管理するオブジェクトに付けるコンポーネント
InitSceneData.cs
ユーザデータを格納するScriptableObjecに付けるコンポーネント
InteractionSystem.cs

