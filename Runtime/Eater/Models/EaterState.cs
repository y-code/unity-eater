namespace ycode.Eater
{
	public class EaterState
	{
		private readonly int StomachMax;
		public int Stomach { get; private set; }

		public EaterState(int stomachMax)
		{
			StomachMax = stomachMax;
		}

		public bool IsFull => Stomach >= StomachMax;
		public bool HasEnergy => Stomach > 0;

		public void Eat(int calorie)
		{
			Stomach += calorie;
		}

		public bool Consume(int calorie)
		{
			if (Stomach < calorie)
				return false;

			Stomach -= calorie;
			return true;
		}
	}
}
