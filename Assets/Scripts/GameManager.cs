using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CustomCamera _camera;

    [SerializeField]
    private PlayerComp _player;

	void Start ()
    {
        _player.Initialize();
        _camera.Initialize();
        _camera.FollowTarget(_player.Transform);
	}
	
	void Update ()
    {
		
	}
}
