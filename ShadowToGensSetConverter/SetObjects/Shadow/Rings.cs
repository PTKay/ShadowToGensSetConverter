using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Shadow
{
    public class Rings : SetObjectShadow
    {
        public int NumberOfRings;
        public float LengthRadius;
        public float Angle;
        public bool Ghost;

        public override void ReadMiscSettings(BinaryReader reader)
        {
            reader.ReadInt32(); // Ring Type
            NumberOfRings = reader.ReadInt32();
            LengthRadius = reader.ReadSingle();
            Angle = reader.ReadSingle();
            Ghost = reader.ReadInt32() != 0;
        }
    }
}
