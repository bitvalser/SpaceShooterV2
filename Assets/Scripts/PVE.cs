using UnityEngine.UI;
using UnityEngine;

public class PVE : MonoBehaviour
{
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(TaskOnClickPVE);
    }
    void TaskOnClickPVE()
    {
        Debug.Log("res");
        Application.LoadLevel("PVE");
    }
}
