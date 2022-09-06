using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models
{
    public class GameRun
    {
        private readonly Random rnd = new Random();
        private readonly List<Door> DoorList;
        public GameRun(int numberOfDoors = 3)
        {
            DoorList = CreateDoorList(numberOfDoors);
        }

        public bool HasUserWon(bool isSwitchingDoor)
        {
            return GetResult(isSwitchingDoor).HasCar();
        }

        private List<Door> CreateDoorList(int numberOfDoors)
        {
            var doors = new List<Door>();
            doors.AddRange(Enumerable.Repeat(new Door(PrizeType.GOAT), numberOfDoors));
            doors[rnd.Next(doors.Count)] = new Door(PrizeType.CAR);

            return doors;
        }

        private Door GetResult(bool isSwitchingDoor)
        {
            int randomDoorIndex = rnd.Next(DoorList.Count);
            Door userChoice = DoorList[randomDoorIndex];
            DoorList.RemoveAt(randomDoorIndex);

            if (isSwitchingDoor)
            {
                var goatDoor = DoorList.Where(x => x.HasGoat()).FirstOrDefault();
                DoorList.Remove(goatDoor);
                userChoice = DoorList[0];
            }

            return userChoice;
        }
    }

}