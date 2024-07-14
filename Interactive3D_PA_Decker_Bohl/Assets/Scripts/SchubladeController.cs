using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchubladeController : MonoBehaviour
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
        if(Input.GetKeyDown(KeyCode.E) && !_animator.GetBool("isOpened"))
        {
            print("sdfjfk");
            print(_animator);
            _animator.SetBool("isOpened", true);

        }

        else if (Input.GetKeyDown(KeyCode.E) && _animator.GetBool("isOpened"))
        {
            _animator.SetBool("isOpened", false);
        }
    }
}
