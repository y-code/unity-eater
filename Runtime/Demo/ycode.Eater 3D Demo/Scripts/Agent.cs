using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using ycode.DebugUtilities;
using ycode.Eater;

public class Agent : MonoBehaviour
{
    static private int sequencer = 0;

    public Transform Goal;

    private NavMeshAgent _navAgent;
    private Eater _eater;

    public Spawner[] _spawners;
    public bool _active;

    void Start()
    {
        gameObject.name = $"Eater {sequencer++}";

        _eater = GetComponent<Eater>();
        _navAgent = GetComponent<NavMeshAgent>();
        if (Goal != null)
            _navAgent.destination = Goal.position;

        _spawners = FindObjectsOfType<Spawner>();
        _active = true;
        StartCoroutine(SwitchGoal());
        StartCoroutine(Run());

        var debugPanel = FindObjectOfType<DebugPanel>();
        var eater = GetComponent<Eater>();
        debugPanel?.Add(() => gameObject.name, () => eater?.State.Stomach.ToString());
    }

    private void OnDestroy()
    {
        _active = false;
    }

    void Update()
    {
        if (_eater != null && _navAgent != null)
		{
            if (_eater.TargetingFood != null)
                _navAgent.destination = _eater.TargetingFood.transform.position;
            else if (Goal != null && _eater.HasEnergy)
                _navAgent.destination = Goal.transform.position;
            else
                _navAgent.destination = _navAgent.transform.position;
        }
    }

    IEnumerator SwitchGoal()
    {
        while (_active)
        {
            Goal = _spawners[(int)(_spawners.Length * Random.value)].transform;
            yield return new WaitForSeconds(10 * Random.value);
        }
        yield return null;
    }

    IEnumerator Run()
	{
        while (_active)
		{
            yield return new WaitForSeconds(2);
            _eater?.State.Consume(1);
		}
	}
}
