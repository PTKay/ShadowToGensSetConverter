using ShadowToGensSetConverter.SetObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class EnemyGunBeetle3D : SetObjectGens
    {
        public float ActionRange = 100;
        public float AttackRange = 12;
        public bool DebugPointClear = false;
        public bool IsAimTarget = false;
        public bool IsArrivalEffect = false;
        public bool IsDamageFromOnlyPlayer = false;
        public bool IsPatrol = true;
        public bool IsPlayFindMotion = true;
        public bool IsRebirth = false;
        public float PathIsAccel = 0;
        public float PathIsBezier = 1;
        public bool PathPointMoveRelative = true;
        public bool Path_IsLoop = true;
        public float ShotInterval = 2;
        public float ShotReadyTime = 1;
        public float ShotSpeed = 0;
        public Position Target = new Position();
        public float ToRebirthTime = 3;
        public float Vel1D = 10;
    }
}
