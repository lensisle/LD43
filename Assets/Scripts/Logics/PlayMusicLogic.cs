using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Logics/Play Music Logic", order = 5)]
public class PlayMusicLogic : ActionLogic
{
    [SerializeField]
    private AudioClip _clip;

    [SerializeField]
    private bool _loop;

    public override void Execute()
    {
        GameManager.Instance.Audio.PlayMusic(_clip, _loop);
        Finish();
    }

    public override void Finish()
    {
        _callNext();
    }
}
