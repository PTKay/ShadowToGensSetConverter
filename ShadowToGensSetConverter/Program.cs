using HedgeLib.IO;
using ShadowToGensSetConverter.Helpers;
using ShadowToGensSetConverter.Light;
using ShadowToGensSetConverter.Mapper;
using ShadowToGensSetConverter.SetObjects.Common;
using ShadowToGensSetConverter.SetObjects.Gens;
using ShadowToGensSetConverter.SetObjects.Shadow;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ShadowToGensSetConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.WriteLine("""
                    ShadowToGensSetConverter
                    Converts a HeroesPowerPlant exported Shadow set .ini file into a Generations compatible .xml set file.
                    If found, it also converts all the other Shadow .ini stage set files.

                    Usage:
                    ShadowToGensSetConverter.exe <setDifficultyFile> <scaleTransform [0.1]>

                    setDifficultyFile - The INI file exported from HeroesPowerPlant. 
                                        Should be the one marked with the difficulty (_nrm for normal, _hrd for hard)
                    scaleTransform    - The scale of the resulting set XML. 
                                        Defaults to 0.1, which is the recommended scaling for Shadow stages
                    """);

                Console.ReadLine();
                return;
            }

            string setDifficultyFile = args[0];
            string dir = Path.GetDirectoryName(setDifficultyFile);
            List<string> setFiles = GetSetFileListFromDroppedFile(setDifficultyFile);

            List<SetObjectShadow> setDataHeroes = [];
            foreach (string file in setFiles)
            {
                if (File.Exists(file))
                {
                    setDataHeroes.AddRange(ShadowSetParser.ParseShadowIni(file));
                }
            }

            List<SetObjectGens> setData = [];
            int lastId = 1000; // Start listing objects in ID 1000 to avoid conflicts
            foreach (var heroesObject in setDataHeroes)
            {
                List<SetObjectGens> setObjects = ShadowToGensSetObjectMapper.MapToGens(heroesObject);
                if (setObjects != null)
                {
                    foreach(SetObjectGens setObject in setObjects) {
                        setObject.SetObjectID = lastId++;
                        setData.Add(setObject);
                    }
                }
            }

            // Scale result
            float scale = args.Length > 1 ? float.Parse(args[1], CultureInfo.InvariantCulture.NumberFormat) : 0.1f;
            ScalePositions(setData, scale);

            string convertedXml = XmlSerialiser.GenerateGensXml(setData);

            File.WriteAllText(Path.Combine(dir, "setdata_base.set.xml"), convertedXml);

            CreateOmnis(setDataHeroes, Path.Combine(dir, "lights"));
            CreateOmnis(setData, Path.Combine(dir, "lights"));
        }

        private static void CreateOmnis(List<SetObjectShadow> setData, string lightsPath)
        {
            Directory.CreateDirectory(lightsPath);

            int lampOmniIndex = 0;
            foreach(SetObjectShadow setObject in setData)
            {
                if (setObject.GetType() == typeof(Decoration1))
                {
                    Decoration1 obj = setObject as Decoration1;
                    if (obj.DecorationType == 0)
                    {
                        SonicLightXml light = new SonicLightXml
                        {
                            Position = obj.Position,
                            Color = new Color() { R = 0.996f, G = 0.996f, B = 0.27f },
                            OmniLightRange = new OmniLightRange() { OmniLightInnerRange = 0f, OmniLightOuterRange = 9f }
                        };

                        // Adjust position
                        obj.Rotation.y += 90;

                        light.Position = VectorOperations.CalculateNextPosition(obj.Position, obj.Rotation, -5);
                        light.Position.y += 10.5f;

                        SaveLight(light, Path.Combine(lightsPath, $"Converted_StreetLampLight{lampOmniIndex++}.light"));
                    }
                }
            }
        }

        private static void CreateOmnis(List<SetObjectGens> setData, string lightsPath)
        {
            Directory.CreateDirectory(lightsPath);

            int particleLight = 0;
            foreach (SetObjectGens setObject in setData)
            {
                if (setObject.GetType() == typeof(SetParticle))
                {
                    SetParticle particle = setObject as SetParticle;
                    if (particle.EffectName.StartsWith("ef_st_csc_yh1_bg_fire"))
                    {
                        SonicLightXml light = new SonicLightXml
                        {
                            Position = particle.Position,
                            Color = new Color() { R = 1f, G = 0.447f, B = 0.074f }, // #ff7213
                            OmniLightRange = new OmniLightRange() { OmniLightInnerRange = 5f, OmniLightOuterRange = 15f }
                        };

                        SaveLight(light, Path.Combine(lightsPath, $"Converted_ParticleLight{particleLight++}.light"));
                    }
                }
            }
        }

        public static void SaveLight(SonicLightXml light, string path)
        {
            using (var fileStream = File.Create(path))
            {
                var writer = new ExtendedBinaryWriter(fileStream, true);

                // Write Header
                writer.Write(0);
                writer.Write(1);
                writer.Write(0);
                writer.Write(24);
                writer.Write(0);
                writer.Write(0);

                // Set Omni Light
                writer.Write(1);

                // Position
                writer.Write(light.Position.x);
                writer.Write(light.Position.y);
                writer.Write(light.Position.z);

                // Color
                writer.Write(light.Color.R);
                writer.Write(light.Color.G);
                writer.Write(light.Color.B);

                // Omni Information
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);

                writer.Write(light.OmniLightRange.OmniLightInnerRange);
                writer.Write(light.OmniLightRange.OmniLightOuterRange);

                // Fill Header
                writer.Write(0);
                fileStream.Position = 0;
                writer.Write((uint)fileStream.Length);

                fileStream.Position = 8;
                uint finalTablePosition = (uint)fileStream.Length - 4;
                writer.Write(finalTablePosition - 0x18);

                fileStream.Position = 16;
                writer.Write(finalTablePosition);
            }
        }

        private static List<string> GetSetFileListFromDroppedFile(string setDifficultyFile)
        {
            string path = Path.GetDirectoryName(setDifficultyFile);
            string fileName = Path.GetFileName(setDifficultyFile);
            string stgName = fileName.Substring(0, fileName.IndexOf("_"));

            bool isHardMode = fileName.Contains("_hrd");

            return [
                Path.Combine(path, $"{stgName}_cmn.ini"),
                Path.Combine(path, $"{stgName}_ds1.ini"),
                isHardMode ? Path.Combine(path, $"{stgName}_hrd.ini") : Path.Combine(path, $"{stgName}_nrm.ini")
            ];
        }

        private static void ScalePositions(List<SetObjectGens> setData, float scale)
        {
            foreach (var item in setData)
            {
                item.Position.Scale(scale);
                if (item.MultiSetParam != null)
                {
                    foreach (var multiSetElement in item.MultiSetParam.Elements)
                    {
                        multiSetElement.Position.Scale(scale);
                    }
                }
            }
        }
    }
}
