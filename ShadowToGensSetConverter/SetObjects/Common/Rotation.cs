using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShadowToGensSetConverter.SetObjects.Common
{
    public class Rotation
    {
        [XmlElement("x")]
        public string xAsString
        {
            get => x.ToString("F6", CultureInfo.InvariantCulture.NumberFormat);
            set => x = float.Parse(value);
        }
        [XmlElement("y")]
        public string yAsString
        {
            get => y.ToString("F6", CultureInfo.InvariantCulture.NumberFormat);
            set => y = float.Parse(value);
        }
        [XmlElement("z")]
        public string ZAsString
        {
            get => z.ToString("F6", CultureInfo.InvariantCulture.NumberFormat);
            set => z = float.Parse(value);
        }
        [XmlElement("w")]
        public string wAsString
        {
            get => w.ToString("F6", CultureInfo.InvariantCulture.NumberFormat);
            set => w = float.Parse(value);
        }

        [XmlIgnore]
        public float x;
        [XmlIgnore]
        public float y;
        [XmlIgnore]
        public float z;
        [XmlIgnore]
        public float w;

        public Rotation() { }

        public Rotation(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
