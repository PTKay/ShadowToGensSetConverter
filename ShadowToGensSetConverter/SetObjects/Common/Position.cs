using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShadowToGensSetConverter.SetObjects.Common
{
    public class Position
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

        [XmlIgnore]
        public float x = 0;
        [XmlIgnore]
        public float y = 0;
        [XmlIgnore]
        public float z = 0;

        public Position() { }

        public Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Scale(float scale)
        {
            this.x = x * scale;
            this.y = y * scale;
            this.z = z * scale;
        }
    }
}
