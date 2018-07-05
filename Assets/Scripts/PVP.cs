using UnityEngine.UI;
using UnityEngine;

public class PVP : MonoBehaviour
{
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(TaskOnClickPVP);
    }
    void TaskOnClickPVP()
    {
        Debug.Log("res");
        Application.LoadLevel("pvp");
    }
}
