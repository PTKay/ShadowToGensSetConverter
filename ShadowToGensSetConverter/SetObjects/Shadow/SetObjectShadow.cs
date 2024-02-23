using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ShadowToGensSetConverter.SetObjects.Common;

namespace ShadowToGensSetConverter.SetObjects.Shadow
{
    public class SetObjectShadow : SetObject
    {
        public string Name;

        public string Link;
        public string Rend;
        public string B;
        public byte[] MiscSettings;

        public void ReadMiscSettings()
        {
            if (MiscSettings != null)
            {
                ReadMiscSettings(new BinaryReader(new MemoryStream(MiscSettings)));
            }
        }

        public virtual void ReadMiscSettings(BinaryReader reader)
        {
            // No specific logic for generic object
        }
    }
}
