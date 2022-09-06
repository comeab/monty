namespace Application.Models
{
    public class Door
    {

        private readonly PrizeType Prize;
        public bool HasCar()
        {
            return Prize == PrizeType.CAR;
        }
        public bool HasGoat()
        {
            return Prize == PrizeType.GOAT;
        }
        public Door(PrizeType type)
        {
            Prize = type;
        }
    }

}