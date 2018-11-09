using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.loadUtils
{
    public static class StateMachineExtension
    {
        public static void Deserialize(this StateMachine machine, string file)
        {
            if (!File.Exists(file))
            {
                return;
            }
            SectionReader sectionReader = new SectionReader();

            try
            {
                using (var fs = new FileStream(file, FileMode.Open))
                {
                    using (var sw = new StreamReader(fs))
                    {
                        string line = sw.ReadLine();
                        while (line != null)
                        {
                            line = line.Replace("\t", "").Replace("\r\n", "");
                            if (line.StartsWith(@"\\") || line.StartsWith("//"))
                            {
                                //read next line
                                line = sw.ReadLine();
                                continue;
                            }

                            //normal line parse
                            sectionReader.ReadLine(machine, line);

                            //read next line
                            line = sw.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{sectionReader.Section} {sectionReader.IndentSection} Deserialize Machine Error:{ex.Message}");
                throw ex;
            }


        }

        public static void Serialize(this StateMachine machine, string file)
        {
            using (var fs = new FileStream(file, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {

                    sw.WriteLine(new SectionWriter<MotionCardWrapper>("MOTION", machine.MotionExs).Export());
                    sw.WriteLine(new SectionWriter<IDiEx>("DI", machine.DiExs).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("DO", machine.DoExs).Export());
                    sw.WriteLine(new SectionWriter<ICylinderEx>("CY", machine.CylinderExs).Export());
                    sw.WriteLine(new SectionWriter<IVioEx>("VIO", machine.VioExs).Export());
                    sw.WriteLine();


                    sw.WriteLine(new SectionWriter<IDiEx>("ESTOP", machine.DiEstop).Export());
                    sw.WriteLine(new SectionWriter<IDiEx>("START", machine.DiStart).Export());
                    sw.WriteLine(new SectionWriter<IDiEx>("STOP", machine.DiStop).Export());
                    sw.WriteLine(new SectionWriter<IDiEx>("RESET", machine.DiReset).Export());
                    sw.WriteLine();


                    sw.WriteLine(new SectionWriter<IDoEx>("LIGHTGREEN", machine.DoLightGreen).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("LIGHTYELLOW", machine.DoLightYellow).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("LIGHTRED", machine.DoLightRed).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("BUZZER", machine.DoBuzzer).Export());
                    sw.WriteLine();


                    sw.WriteLine(new SectionWriter<IAxisEx>("AXIS", machine.AxisExs).Export());
                    sw.WriteLine(new SectionWriter<PlatformEx>("PLATFORM", machine.Platforms).Export());
                    sw.WriteLine();


                    sw.WriteLine(new SectionWriter<Station>("STATION", machine.Stations).Export());
                }
            }
        }


        public static string SerializeToString(this StateMachine machine)
        {
            var sb = new StringBuilder();

            sb.AppendLine(new SectionWriter<MotionCardWrapper>("MOTION", machine.MotionExs).Export());
            sb.AppendLine(new SectionWriter<IDiEx>("DI", machine.DiExs).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("DO", machine.DoExs).Export());
            sb.AppendLine(new SectionWriter<ICylinderEx>("CY", machine.CylinderExs).Export());
            sb.AppendLine(new SectionWriter<IVioEx>("VIO", machine.VioExs).Export());
            sb.AppendLine();

            sb.AppendLine(new SectionWriter<IDiEx>("ESTOP", machine.DiEstop).Export());
            sb.AppendLine(new SectionWriter<IDiEx>("START", machine.DiStart).Export());
            sb.AppendLine(new SectionWriter<IDiEx>("STOP", machine.DiStop).Export());
            sb.AppendLine(new SectionWriter<IDiEx>("RESET", machine.DiReset).Export());
            sb.AppendLine();

            sb.AppendLine(new SectionWriter<IDoEx>("LIGHTGREEN", machine.DoLightGreen).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("LIGHTYELLOW", machine.DoLightYellow).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("LIGHTRED", machine.DoLightRed).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("BUZZER", machine.DoBuzzer).Export());
            sb.AppendLine();


            sb.AppendLine(new SectionWriter<IAxisEx>("AXIS", machine.AxisExs).Export());
            sb.AppendLine(new SectionWriter<PlatformEx>("PLATFORM", machine.Platforms).Export());
            sb.AppendLine();


            sb.AppendLine(new SectionWriter<Station>("STATION", machine.Stations).Export());

            return sb.ToString();
        }





    }
}