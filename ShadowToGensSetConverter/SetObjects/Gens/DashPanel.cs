using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class DashPanel : SetObjectGens
    {
        public bool IsChangeCameraWhenChangePath = false;
        public bool IsChangePath = false;
        public bool IsConstantStartVelocity = true;
        public bool IsInvisible = false;
        public bool IsTo3D = false;
        public bool IsUseDelayCamera = true;
        public float OutOfControl = 1;
        public float Speed = 65;
        public float SpeedMax = 60;
        public float SpeedMin = 30;
    }
}
