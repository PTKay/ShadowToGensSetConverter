using ShadowToGensSetConverter.SetObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class EnemyGunHunter3D : SetObjectGens
    {
        public float ActionRange = 18;
        public float AppearType = 0;
        public float AttackRange = 18;
        public bool DebugPointClear = false;
        public bool EnableTurnSearch = true;
        public bool EventAppear = false;
        public float FallSpeed = 10;
        public bool IsAimTarget = false;
        public bool IsArrivalEffect = false;
        public bool IsCastShadow = true;
        public bool IsDamageFromOnlyPlayer = false;
        public bool IsPlayFindMotion = false;
        public float PathIsAccel = 0;
        public float PathIsBezier = 1;
        public bool PathPointMoveRelative = true;
        public bool Path_IsLoop = false;
        public string PositionList = "";
        public float ShotInterval = 2;
        public float ShotReadyTime = 0.8f;
        public float ShotSpeed = 0;
        public Position Target = new Position();
        public float Vel1D = 10;
    }
}
