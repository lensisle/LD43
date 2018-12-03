using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class DialoguePage 
{
    public string Title;
    public string Content;
}

public class DialoguesController : MonoBehaviour
{
    private Queue<DialoguePage> _pages;
    public int PageCount
    {
        get 
        {
            return _pages.Count;
        }
    }

    public event Action OnFinish;

    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private TextMeshProUGUI _content;

    public void Initialize()
    {
        _pages = new Queue<DialoguePage>();
    }

    private void SetActiveByDialogue()
    {
        DialoguePage page = _pages.Dequeue();
        SetTitle(page.Title);
        SetContent(page.Content);
        gameObject.SetActive(true);
    }

    public void AppendDialogue(string title, string content)
    {
        DialoguePage page = new DialoguePage
        {
            Title = title,
            Content = content
        };

        _pages.Enqueue(page);
    }

    public void CheckActivity()
    {
        if (GameManager.Instance.UI.IsBusy == false && PageCount > 0)
        {
            SetActiveByDialogue();
        }
    }

    private void SetTitle(string title)
    {
        _title.text = title;
    }

    private void SetContent(string content)
    {
        _content.text = content;
    }

    void Update()
    {
        if (GameManager.Instance.UI.IsBusy && Input.GetKeyDown(KeyCode.Space) && _pages.Count == 0)
        {
            gameObject.SetActive(false);
            if (OnFinish != null)
            {
                OnFinish();
            }
        }
        else if (GameManager.Instance.UI.IsBusy && Input.GetKeyDown(KeyCode.Space) && _pages.Count > 0)
        {
            DialoguePage page = _pages.Dequeue();
            SetTitle(page.Title);
            SetContent(page.Content);
        }
    }
}
