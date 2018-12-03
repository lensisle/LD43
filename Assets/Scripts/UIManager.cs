using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameManagerDependency
{
    [SerializeField]
    private DialoguesController _dialogues;

    private Queue<DialoguePage> _pages;

    public bool IsBusy
    {
        get 
        {
            return _dialogues.isActiveAndEnabled;
        }
    }

    public void Initialize()
    {
        _dialogues.gameObject.SetActive(false);
        _pages = new Queue<DialoguePage>();
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

    private void Update()
    {
        if (IsBusy && Input.GetKeyDown(KeyCode.Space) && _pages.Count == 0) 
        {
            _dialogues.gameObject.SetActive(false);
        }
        else if (IsBusy && Input.GetKeyDown(KeyCode.Space) && _pages.Count > 0)
        {
            DialoguePage page = _pages.Dequeue();
            _dialogues.SetTitle(page.Title);
            _dialogues.SetContent(page.Content);
        }
        else if (IsBusy == false && _pages.Count > 0)
        {
            _dialogues.gameObject.SetActive(true);
            DialoguePage page = _pages.Dequeue();
            _dialogues.SetTitle(page.Title);
            _dialogues.SetContent(page.Content);
        }
    }
}
