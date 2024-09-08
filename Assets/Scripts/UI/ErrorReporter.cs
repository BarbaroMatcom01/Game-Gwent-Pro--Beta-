using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorReporter : MonoBehaviour
{
    public GameObject prefabError;
    public static ErrorReporter Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public void Report(string message)
    {
        var verticalLayout = this.transform.GetComponent<VerticalLayoutGroup>();

        var newError = Instantiate(prefabError, new Vector3(0, 0, 0), Quaternion.identity);
        newError.transform.SetParent(verticalLayout.transform);
        newError.transform.GetComponent<TextMeshProUGUI>().text = message;
    }


}
