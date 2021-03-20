using System.Collections;
using UnityEngine;
using ycode.Eater;

public class FoodServer : MonoBehaviour
{
    public Food FoodPrefab;
    public int intervalSeconds = 15;

    private bool _active;

    void Start()
    {
        _active = true;
        StartCoroutine(Run());
    }

    private void OnDestroy()
    {
        _active = false;
    }

    IEnumerator Run()
    {
        yield return new WaitForFixedUpdate();

        while (_active)
		{
            var food = Instantiate(FoodPrefab);
            food.transform.position = transform.position;
            yield return new WaitForSeconds(intervalSeconds);
		}
    }
}
