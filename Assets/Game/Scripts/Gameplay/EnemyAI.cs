using System;
using System.Linq;
using UnityEngine;
using static Cinetica.Utility.Utils;

namespace Cinetica.Gameplay
{
    public class EnemyAI : MonoBehaviour
    {
        public Difficulty difficulty;
        public float imprecision = 1f;
        
        public void Awake()
        {
            RoundManager.OnTurnStart.AddListener(TurnStart);
            RoundManager.OnTurnEnd.AddListener(TurnEnd);
            switch (difficulty)
            {
                case Difficulty.Easy:
                    imprecision = 1f;
                    break;
                case Difficulty.Medium:
                    imprecision = 0.5f;
                    break;
                case Difficulty.Hard:
                    imprecision = 0.1f;
                    break;
            }
        }

        public void OnDestroy()
        {
            RoundManager.OnTurnStart.RemoveListener(TurnStart);
            RoundManager.OnTurnEnd.RemoveListener(TurnEnd);
        }

        public void TurnStart()
        {
            if (RoundManager.IsPlayerTurn()) return;
            RoundManager.selectedBuilding = GetSelectedBuilding();
            RoundManager.targetBuilding = GetTargetBuilding();
            RoundManager.force = GetForce();
            RoundManager.angle = GetAngle();
        }

        public void TurnEnd()
        {
            
        }

        public Building GetSelectedBuilding()
        {
            var weapons = Building.GetAllWeapons(Side.Enemy);
            
            if (difficulty == Difficulty.Hard)
            {
                // Try selecting railguns first
                var railgun = weapons.First(x => x.buildingType == BuildingType.Railgun);
                if (railgun) return railgun;
            }

            return Pick(weapons);
        }

        public Building GetTargetBuilding()
        {
            var plyWeapons = Building.GetAllWeapons(Side.Player);
            var plyBuildings = Building.GetAliveBuildings(Side.Player);
            
            if (difficulty == Difficulty.Hard)
            {
                var playerRailgun = plyWeapons.First(x => x.buildingType == BuildingType.Railgun);
                if (playerRailgun) return playerRailgun;
            }
            
            
            
            return null;
        }

        public float GetForce()
        {
            // TODO: Select force based on selections above
            return 100f;
        }

        public float GetAngle()
        {
            // TODO: Get angle based on selections above
            return 0;
        }
    }
}