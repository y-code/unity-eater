using System.Collections;
using UnityEngine;
using ycode.Eater;

public class Cow : MonoBehaviour
{
    private Eater _eater;
    private Rigidbody2D _rigidbody;

    private bool _active;

    private CowSpawner[] _goals;
    private CowSpawner _goal;

    public float Speed = 1f;
    public float friction = 0.2f;

    void Start()
    {
        _eater = GetComponent<Eater>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _active = true;
        _goals = FindObjectsOfType<CowSpawner>();

        StartCoroutine(SwitchGoal());
        StartCoroutine(Live());
    }

    private void OnDestroy()
    {
        _active = false;
    }

    void Update()
    {
        if (_eater != null)
        {
            if (_eater.TargetingFood != null)
            {
                Vector2 direction = (_eater.TargetingFood.transform.position - transform.position).normalized;
                if (_rigidbody.velocity.magnitude < .5f)
                    _rigidbody.AddForce(direction * Speed);
            }
            else if (_goal != null && _eater.HasEnergy)
            {
                Vector2 direction = (_goal.transform.position - transform.position).normalized;
                if (_rigidbody.velocity.magnitude < .5f)
                    _rigidbody.AddForce(direction * Speed);
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.angularVelocity = 0f;
            }

            // add friction
            _rigidbody.AddForce(_rigidbody.velocity * -1 * friction);
        }
    }

    IEnumerator SwitchGoal()
    {
        yield return new WaitForFixedUpdate(); // wait for all the game objects to finish `Start()` method call

        while (_active)
        {
            _goal = _goals[(int)(_goals.Length * Random.value)];
            yield return new WaitForSeconds(10f * Random.value);
        }
        yield return null;
    }

    IEnumerator Live()
    {
        yield return new WaitForFixedUpdate(); // wait for all the game objects to finish `Start()` method call

        while (_active)
        {
            yield return new WaitForSeconds(2f);
            _eater.State.Consume(1);
        }
        yield return null;
    }
}
