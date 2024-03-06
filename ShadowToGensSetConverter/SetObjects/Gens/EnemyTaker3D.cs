using ShadowToGensSetConverter.SetObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class EnemyTaker3D : SetObjectGens
    {
        public float ActionRange = 100;
        public float AppearType = 0;
        public float AttackInterval = 4;
        public float AttackRange = 100;
        public bool DebugPointClear = false;
        public bool DebugPointClearMove = false;
        public bool IsAimTarget = false;
        public bool IsArrivalEffect = false;
        public bool IsAttack = false;
        public bool IsDamageFromOnlyPlayer = true;
        public bool IsMessageOn = false;
        public bool IsPlayFindMotion = true;
        public bool IsReturn = false;
        public bool IsShotAim = false;
        public bool IsThrowDown = false;
        public bool IsTurnToPlayer = true;
        public float MoveSpeed = 5;
        public float MoveType = 0;
        public float PathIsAccel = 1;
        public float PathIsAccelMove = 0;
        public float PathIsBezier = 1;
        public float PathIsBezierMove = 1;
        public bool PathPointMoveRelative = true;
        public bool PathPointMoveRelativeMove = true;
        public bool Path_IsLoop = false;
        public bool Path_IsLoopMove = true;
        public string PositionList = "";
        public List<Position> PositionListMove = new List<Position>();
        public float ReadyTime = 2.5f;
        public float ReviveTime = 5;
        public float ReviveType = 0;
        public float ShotLength = 3;
        public float ShotSpeed = 20;
        public Position Target = new Position();
        public float TurnAccel = 4;
        public float TurnTime = 1;
        public float TurnToPlayerRate = 0.1f;
        public float Vel1D = 30;
        public float Vel1DMove = 10;
        public Position WayPointA = new Position();
        public Position WayPointB = new Position();
    }
}
