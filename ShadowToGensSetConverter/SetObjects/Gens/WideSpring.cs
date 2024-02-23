using ShadowToGensSetConverter.SetObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class WideSpring : SetObjectGens
    {
        public float FirstSpeed = 32;
        public bool IsBreak = false;
        public bool IsHomingAttackEnable = true;
        public bool IsStartVelocityConstant = true;
        public float KeepVelocityDistance = 5;
        public float OutOfControl = 0.4f;
        public bool m_IsConstantPosition = true;
    }
}
