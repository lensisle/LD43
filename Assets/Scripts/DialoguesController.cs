using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePage 
{
    public string Title;
    public string Content;
}

public class DialoguesController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private TextMeshProUGUI _content;

    public void SetTitle(string title)
    {
        _title.text = title;
    }

    public void SetContent(string content)
    {
        _content.text = content;
    }
}
