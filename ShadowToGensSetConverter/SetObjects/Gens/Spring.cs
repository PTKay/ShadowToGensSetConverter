using ShadowToGensSetConverter.SetObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class Spring : SetObjectGens
    {
        public float AimDirection = 0;
        public float BaseRotation = 0;
        public float DebugShotTimeLength = 2;
        public float FirstSpeed = 32;
        public bool IsBreak = false;
        public bool IsChangeCameraWhenPathChange = true;
        public bool IsHomingAttackEnable = true;
        public bool IsLongBase = false;
        public bool IsPathChange = false;
        public bool IsSideSet = false;
        public bool IsStartVelocityConstant = true;
        public bool IsWallWalk = false;
        public bool IsWithBase = false;
        public bool IsYawUpdate = true;
        public float KeepVelocityDistance = 5;
        public float MotionType = 0;
        public float OutOfControl = 0.4f;
        public bool m_IsConstantFrame = false;
        public bool m_IsConstantPosition = true;
        public bool m_IsMonkeyHunting = false;
        public bool m_IsStopBoost = false;
        public bool m_IsTo3D = false;
        public Position m_MonkeyTarget = new Position();
    }
}
