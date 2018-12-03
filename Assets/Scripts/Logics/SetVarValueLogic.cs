using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Logics/Set Var Value Logic", order = 2)]
public class SetVarValueLogic : ActionLogic
{
    [SerializeField]
    private string _varID;

    [SerializeField]
    private int _value;

    [SerializeField]
    private bool _sumQuantity;

    public override void Execute()
    {
        GameManager.Instance.GameEvents.SetVariableValue(_varID, _value, _sumQuantity);
        Finish();
    }

    public override void Finish()
    {
        _callNext();
    }
}
