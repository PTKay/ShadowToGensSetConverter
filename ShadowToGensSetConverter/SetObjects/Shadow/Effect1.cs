using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Shadow
{
    public class Effect1 : SetObjectShadow
    {
        public int EfectType;
        public float ScaleX;
        public float ScaleY;
        public float ScaleZ;

        public override void ReadMiscSettings(BinaryReader reader)
        {
            EfectType = reader.ReadInt32();
            ScaleX = reader.ReadInt32();
            ScaleY = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
        }
    }
}
