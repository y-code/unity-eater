using System;
using UnityEngine;
using ycode.Eater;

public class Fruit : MonoBehaviour
{
	private Food _food;

	private void Start()
	{
		_food = GetComponent<Food>();
		_food.OnFoodEmpty += _food_OnFoodEmpty;
	}

	private void OnDestroy()
	{
		_food.OnFoodEmpty -= _food_OnFoodEmpty;
	}

	private void _food_OnFoodEmpty(Food sender)
	{
        Destroy(gameObject);
	}
}
