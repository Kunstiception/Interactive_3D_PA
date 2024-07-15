using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tonabnehmer : MonoBehaviour
{
    // Animator of the object
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_animator.GetBool("isActive"))
        {
            print("sdfjfk");
            print(_animator);
            _animator.SetBool("isActive", true);

        }

        else if (Input.GetKeyDown(KeyCode.E) && _animator.GetBool("isActive"))
        {
            _animator.SetBool("isActive", false);
        }
    }
}
