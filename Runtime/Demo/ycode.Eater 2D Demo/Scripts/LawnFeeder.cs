using System.Collections;
using UnityEngine;
using ycode.Eater;

public class LawnFeeder : MonoBehaviour
{
    public Food Food;
    public bool _active;
    public Food _food;

    // Start is called before the first frame update
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
        yield return new WaitForFixedUpdate(); // wait for all the game objects to finish `Start()` method call

        while (_active)
        {
            if (_food == null)
            {
                _food = Instantiate(Food, transform);
                _food.OnFoodEmpty += _food_OnFoodEmpty;
            }
            yield return new WaitForSeconds(20);
        }
    }

    private void _food_OnFoodEmpty(Food sender)
    {
        _food.OnFoodEmpty -= _food_OnFoodEmpty;
        _food = null;
    }
}
