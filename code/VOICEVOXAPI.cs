using Newtonsoft.Json;

/// <summary>
/// APIリクエスト
/// 
/// https://beta.openai.com/docs/api-reference/authentication
/// </summary>

[JsonObject]
public class VOICEVOXQuery
{
    [JsonProperty("text")]
    public string Text { get; set; } = "";
    [JsonProperty("speaker")]
    public int Speaker { get; set; } = 1;
}

[JsonObject]
public class VOICEVOXSynthesis
{
    [JsonProperty("speaker")]
    public int Speaker { get; set; } = 1;
}

// /// <summary>
// /// APIレスポンス
// /// 
// /// https://beta.openai.com/docs/api-reference/authentication
// /// </summary>
// [JsonObject]
// public class SynthesisResponseData
// {
//     [JsonProperty("accent_phrases")]
//     public AccentPhrases AccentPhrases { get; set; }
//     [JsonProperty("object")]
//     public string Object { get; set; }
//     [JsonProperty("model")]
//     public string Model { get; set; }
//     [JsonProperty("created")]
//     public int Created { get; set; }
//     [JsonProperty("choices")]
//     public ChoiceData[] Choices { get; set; }
//     [JsonProperty("usage")]
//     public UsageData Usage { get; set; }
// }

// [JsonObject]
// public class AccentPhrases
// {
//     [JsonProperty("prompt_tokens")]
//     public int PromptTokens { get; set; }
//     [JsonProperty("completion_tokens")]
//     public int CompletionTokens { get; set; }
//     [JsonProperty("total_tokens")]
//     public int TotalTokens { get; set; }
// }

// [JsonObject]
// public class ChoiceData
// {
//     [JsonProperty("text")]
//     public string Text { get; set; }
//     [JsonProperty("index")]
//     public int Index { get; set; }
//     [JsonProperty("logprobs")]
//     public string Logprobs { get; set; }
//     [JsonProperty("finish_reason")]
//     public string FinishReason { get; set; }
// }

/// <summary>
/// APIレスポンス
/// 
/// https://beta.openai.com/docs/api-reference/authentication
/// </summary>
// [JsonObject]
// public class VOICEVOXQueryResponse
// {
//     [JsonProperty("accent_phrases")]
//     public string[] AcceptPhrases { get; set; }
//     [JsonProperty("object")]
//     public string Object { get; set; }
//     [JsonProperty("model")]
//     public string Model { get; set; }
//     [JsonProperty("created")]
//     public int Created { get; set; }
//     [JsonProperty("choices")]
//     public ChoiceData[] Choices { get; set; }
//     [JsonProperty("usage")]
//     public UsageData Usage { get; set; }
//     {
//   "accent_phrases": [
//     {
//       "moras": [
//         {
//           "text": "string",
//           "consonant": "string",
//           "consonant_length": 0,
//           "vowel": "string",
//           "vowel_length": 0,
//           "pitch": 0
//         }
//       ],
//       "accent": 0,
//       "pause_mora": {
//     "text": "string",
//         "consonant": "string",
//         "consonant_length": 0,
//         "vowel": "string",
//         "vowel_length": 0,
//         "pitch": 0
//       },
//       "is_interrogative": false
//     }
//   ],
//   "speedScale": 0,
//   "pitchScale": 0,
//   "intonationScale": 0,
//   "volumeScale": 0,
//   "prePhonemeLength": 0,
//   "postPhonemeLength": 0,
//   "outputSamplingRate": 0,
//   "outputStereo": true,
//   "kana": "string"
// }
// }

// [JsonObject]
// public class UsageData
// {
//     [JsonProperty("prompt_tokens")]
//     public int PromptTokens { get; set; }
//     [JsonProperty("completion_tokens")]
//     public int CompletionTokens { get; set; }
//     [JsonProperty("total_tokens")]
//     public int TotalTokens { get; set; }
// }

// [JsonObject]
// public class ChoiceData
// {
//     [JsonProperty("text")]
//     public string Text { get; set; }
//     [JsonProperty("index")]
//     public int Index { get; set; }
//     [JsonProperty("logprobs")]
//     public string Logprobs { get; set; }
//     [JsonProperty("finish_reason")]
//     public string FinishReason { get; set; }
// }

