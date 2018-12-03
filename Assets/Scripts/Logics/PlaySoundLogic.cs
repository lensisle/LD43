using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Logics/Play Sound Logic", order = 4)]
public class PlaySoundLogic : ActionLogic
{
    [SerializeField]
    private AudioClip _clip;

    public override void Execute()
    {
        GameManager.Instance.Audio.PlaySound(_clip);
        Finish();
    }

    public override void Finish()
    {
        _callNext();
    }
}
