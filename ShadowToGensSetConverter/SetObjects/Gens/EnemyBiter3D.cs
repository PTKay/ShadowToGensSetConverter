using ShadowToGensSetConverter.SetObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class EnemyBiter3D : SetObjectGens
    {
        public float ActionRange = 100;
        public float AttackRange = 10;
        public bool IsAimTarget = false;
        public bool IsArrivalEffect = false;
        public bool IsCastShadow = true;
        public bool IsDamageFromOnlyPlayer = false;
        public bool IsMessageOn = false;
        public bool IsPlayFindMotion = true;
        public float MoveSpeed = 5;
        public float MoveType = 0;
        public float ReadyTime = 1;
        public float SearchType = 0;
        public Position Target = new Position();
        public Position WayPointA = new Position();
        public Position WayPointB = new Position();
    }
}
