using ShadowToGensSetConverter.SetObjects.Gens;
using ShadowToGensSetConverter.SetObjects.Shadow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.Mapper
{
    public class ShadowSetParser
    {
        private static Dictionary<string, Func<SetObjectShadow>> MapDictionary = new Dictionary<string, Func<SetObjectShadow>>()
        {
            { "0001_Spring", () => new SetObjects.Shadow.Spring() },
            { "0002_Long-Spring", () => new LongSpring() },
            { "0003_Dash-Panel", () => new SetObjects.Shadow.DashPanel() },
            { "0004_Dash-Ramp", () => new DashRamp() },
            { "0010_Rings", () => new Rings() },
            { "2589_Destructable1", () => new Destructable1() },
            { "258A_Effect1", () => new Effect1() },
            { "2588_Decoration1", () => new Decoration1() },
            { "0022_Red-Fruit", () => new RedFruit() }
        };

        public static List<SetObjectShadow> ParseShadowIni(string filePath)
        {
            string[] file = File.ReadAllLines(filePath);
            var list = new List<SetObjectShadow>();

            SetObjectShadow temp = null;
            foreach (string s in file)
            {
                if (s.StartsWith("obj "))
                {
                    string objectType = s.Substring(3).Trim();

                    if (temp != null)
                    {
                        temp.ReadMiscSettings();
                        list.Add(temp);
                    }

                    Func<SetObjectShadow> func;
                    if (MapDictionary.TryGetValue(objectType, out func))
                    {
                        temp = func.Invoke();
                    }
                    else
                    {
                        temp = new SetObjectShadow();
                    }

                    temp.Name = objectType;
                }
                else if (s.StartsWith("link "))
                {
                    temp.Link = s.Substring(5, 2);
                }
                else if (s.StartsWith("rend "))
                {
                    temp.Rend = s.Substring(5, 2);
                }
                else if (s.StartsWith("v "))
                {
                    string[] j = s.Split(' ');
                    temp.Position = new SetObjects.Common.Position(float.Parse(j[1], CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(j[2], CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(j[3], CultureInfo.InvariantCulture.NumberFormat));
                }
                else if (s.StartsWith("r "))
                {
                    string[] j = s.Split(' ');
                    temp.Rotation = new SetObjects.Common.Rotation(float.Parse(j[1], CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(j[2], CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(j[3], CultureInfo.InvariantCulture.NumberFormat));
                }
                else if (s.StartsWith("b "))
                {
                    string[] j = s.Split(' ');
                    temp.B = s;
                }
                else if (s.StartsWith("misc "))
                {
                    string newMiscString = Regex.Replace(s[5..], @"\s+", "");
                    var miscSettings = new List<byte>();
                    for (int j = 0; j < newMiscString.Length; j += 2)
                    {
                        var byteasstring = new string(new char[] { newMiscString[j], newMiscString[j + 1] });
                        miscSettings.Add(Convert.ToByte(byteasstring, 16));
                    }
                    temp.MiscSettings = miscSettings.ToArray();
                }
                else if (s == "EndOfFile")
                {
                    if (temp != null)
                    {
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
    }
}
