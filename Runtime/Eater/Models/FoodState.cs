namespace ycode.Eater
{
	public class FoodState
	{
		public int Calorie { get; private set; }
		public int CalorieByServing { get; }

		public bool IsEmpty => Calorie <= 0;

		public FoodState(int calorieByServing, int servings)
		{
			CalorieByServing = calorieByServing;
			Calorie = calorieByServing * servings;
		}

		public bool TryConsuming(out int calorieTaken)
		{
			if (IsEmpty)
			{
				calorieTaken = 0;
				return false;
			}
			else
			{
				Calorie -= CalorieByServing;
				calorieTaken = CalorieByServing;
				return true;
			}
		}
	}
}
