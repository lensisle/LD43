using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Logics/Show Text Logic", order = 1)]
public class ShowTextLogic : ActionLogic
{
    [SerializeField]
    private List<DialoguePage> _pages;

    public override void Execute()
    {
        foreach(DialoguePage page in _pages)
        {
            GameManager.Instance.UI.AppendDialogue(page.Title, page.Content);
        }

        GameManager.Instance.UI.Dialogues.OnFinish += Finish;
    }

    public override void Finish()
    {
        GameManager.Instance.UI.Dialogues.OnFinish -= Finish;
        _callNext();
    }
}
