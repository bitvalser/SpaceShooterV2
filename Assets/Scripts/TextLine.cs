using UnityEngine.UI;
using UnityEngine;

public class TextLine : MonoBehaviour {
    public InputField line;
    public static string nickname;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        line.onValueChange.AddListener(delegate { ChangeText(); });
    }

    public void ChangeText()
    {
        nickname = line.text;
    }

    public string getNick()
    {
        return nickname;
    }
}
