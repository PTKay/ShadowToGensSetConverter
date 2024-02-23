using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Shadow
{
    internal class Spring : SetObjectShadow
    {
        public float Strength;
        public float NoControlTime;

        public override void ReadMiscSettings(BinaryReader reader)
        {
            Strength = reader.ReadSingle();
            NoControlTime = reader.ReadSingle();
        }
    }
}
