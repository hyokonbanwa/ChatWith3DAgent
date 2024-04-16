using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitGameSystem : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField]
    private InputField openApiInput;
    [SerializeField]
    private InputField gnlApiInput;
    [SerializeField]
    private InputField voicevoxInput;
    [SerializeField]
    private InputField promptInput;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button finishButton;
    [SerializeField]
    private InitSceneData data;
    void Start()
    {
        openApiInput.text = data.openAI_API_KEY;
        gnlApiInput.text = data.gcpNaturalLanguage_API_KEY;
        voicevoxInput.text = data.voicevoxURL;
        promptInput.text = data.basePrompt;
        nextButton.onClick.AddListener(() =>
        {
            if (openApiInput.text != "" && gnlApiInput.text != "" && voicevoxInput.text != "" && promptInput.text != "")
            {
                data.openAI_API_KEY = openApiInput.text;
                data.gcpNaturalLanguage_API_KEY = gnlApiInput.text;
                data.voicevoxURL = voicevoxInput.text;
                data.basePrompt = promptInput.text;
                SceneManager.LoadScene("MainScene");
            }

        });
        finishButton.onClick.AddListener(this.Quit);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}
