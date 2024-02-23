using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShadowToGensSetConverter.SetObjects.Gens.Elements
{
    [XmlRoot(ElementName = "MultiSetParam")]
    public class MultiSetParam
    {
        [XmlElement("Element")]
        public List<Element> Elements = new List<Element>();

        public int BaseLine = 1;
        public int Direction = 0;
        public int Interval = 1;
        public int IntervalBase = 0;
        public int PositionBase = 0;
        public int RotationBase = 0;
    }
}
