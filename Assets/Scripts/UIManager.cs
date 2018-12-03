using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameManagerDependency
{
    [SerializeField]
    private DialoguesController _dialogues;

    public void Initialize()
    {
        _dialogues.gameObject.SetActive(false);
    }
}
