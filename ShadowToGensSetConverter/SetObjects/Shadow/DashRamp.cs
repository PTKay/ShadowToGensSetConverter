using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Shadow
{
    internal class DashRamp : SetObjectShadow
    {

        public float Strength;
        public float Height;
        public float NoControlTime;

        public override void ReadMiscSettings(BinaryReader reader)
        {
            Strength = reader.ReadSingle();
            Height = reader.ReadSingle();
            NoControlTime = reader.ReadSingle();
        }
    }
}
