using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameManagerDependency
{
    [SerializeField]
    private DialoguesController _dialogues;
    public DialoguesController Dialogues { get { return _dialogues; } }

    public bool IsBusy
    {
        get 
        {
            return _dialogues.isActiveAndEnabled;
        }
    }

    public void Initialize()
    {
        _dialogues.Initialize();
        _dialogues.gameObject.SetActive(false);
    }

    public void AppendDialogue(string title, string content)
    {
        _dialogues.AppendDialogue(title, content);
    }

    private void Update()
    {
        _dialogues.CheckActivity();
    }
}
