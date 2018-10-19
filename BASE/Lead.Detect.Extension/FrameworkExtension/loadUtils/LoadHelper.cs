using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.elementExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.loadUtils
{

    public enum MachineSection
    {
        MOTION,
        DI,
        ESTOP, START, STOP, RESET,
        DO,
        LIGHTGREEN, LIGHTYELLOW, LIGHTRED, BUZZER,
        CY,
        VIO,
        AXIS,
        PLATFORM,
        STATION,
    }

    public enum SectionKey
    {
        BEGIN,
        END,
    }

    public class SectionReader
    {
        /// <summary>
        /// main section
        /// </summary>
        public string Section;
        /// <summary>
        /// indent case for platform/station
        /// </summary>
        public string IndentSection;

        private int _indentIndex = 0;


        public void ReadLine(StateMachine machine, string line)
        {

            //begin
            if (string.IsNullOrEmpty(Section))
            {
                if (line.EndsWith("BEGIN"))
                {
                    var data = line.Split(' ');
                    if (data.Length < 2 || data[1] != "BEGIN")
                    {
                        throw new Exception($"{Section} {line} FORMAT ERROR");
                    }
                    Section = line.Split(' ')[0];
                }
            }
            else
            {
                //indent begin
                if (line.EndsWith("BEGIN"))
                {
                    //BEGIN LINE
                    if (!string.IsNullOrEmpty(Section))
                    {
                        //INDENT LINE
                        ParseContent(machine, line);
                        IndentSection = line.Split(' ')[1];
                    }
                    else
                    {
                        throw new Exception($"{Section} {line} FORMAT ERROR");
                    }

                }
                else if (line.EndsWith("END"))
                {
                    var data = line.Split(' ');
                    if (data.Length != 2 || data[1] != "END")
                    {
                        throw new Exception($"{Section} {line} FORMAT ERROR");
                    }

                    //END LINE
                    if (!string.IsNullOrEmpty(IndentSection))
                    {
                        if (data[0] != IndentSection)
                        {
                            throw new Exception($"{Section} {line} FORMAT ERROR");
                        }

                        IndentSection = string.Empty;
                    }
                    else
                    {
                        if ((data[0] != Section && string.IsNullOrEmpty(IndentSection))
                            || (data[0] != IndentSection && !string.IsNullOrEmpty(IndentSection)))
                        {
                            throw new Exception($"{Section} {line} FORMAT ERROR");
                        }

                        if (!string.IsNullOrEmpty(Section))
                        {
                            Section = string.Empty;
                        }
                    }
                }
                else
                {
                    //NORMAL LINE
                    ParseContent(machine, line);
                }
            }
        }


        public void ParseContent(StateMachine machine, string line)
        {
            if (Section != "PLATFORM")
            {
                if (line.Length == 0)
                {
                    //read next line
                    return;
                }
            }


            switch (Section)
            {
                case "MOTION":
                    new MotionCardWrapper().Import(line, machine);
                    break;
                case "DI":
                    new DiEx().Import(line, machine);
                    break;
                case "ESTOP":
                case "START":
                case "STOP":
                case "RESET":
                    DiEx.Import(Section, line, machine);
                    break;
                case "DO":
                    new DoEx().Import(line, machine);
                    break;
                case "LIGHTGREEN":
                case "LIGHTYELLOW":
                case "LIGHTRED":
                case "BUZZER":
                    DoEx.Import(Section, line, machine);
                    break;
                case "CY":
                    new CylinderEx().Import(line, machine);
                    break;
                case "VIO":
                    new VioEx().Import(line, machine);
                    break;

                case "AXIS":
                    new AxisEx().Import(line, machine);
                    break;
                case "PLATFORM":
                    if (string.IsNullOrEmpty(IndentSection))
                    {
                        IndentSection = line.Split(' ')[1];
                        PlatformEx.Import(line, machine);
                        _indentIndex = 0;
                    }
                    else
                    {
                        if (line.StartsWith("NULL") || line.StartsWith("null") || line.StartsWith("Null"))
                        {
                            machine.Platforms.Values.FirstOrDefault(p => p.Name == IndentSection).Axis[_indentIndex] = null;
                        }
                        else
                        {
                            var axis = new AxisEx();
                            axis.Import(line, machine);
                            machine.Platforms.Values.FirstOrDefault(p => p.Name == IndentSection).Axis[_indentIndex] = axis;
                        }
                        _indentIndex++;
                    }
                    break;
                case "STATION":
                    if (string.IsNullOrEmpty(IndentSection))
                    {
                        IndentSection = line.Split(' ')[1];
                        Station.Import("STATION", line, machine);
                    }
                    else
                    {
                        var data = line.Split(' ');
                        if (data.Length == 6)
                        {
                            //import task
                            Station.Import("STATIONTASK", line, machine.Find<Station>(IndentSection));
                        }
                        else if (data.Length == 7)
                        {
                            //import pause signal
                            Station.Import("PAUSESIGNAL", line, machine.Find<Station>(IndentSection));
                        }
                        else
                        {
                            throw new Exception($"{Section} {line} FORMAT ERROR");
                        }
                    }
                    break;

                default:
                    break;
            }


        }

    }


    public class SectionWriter<T> where T : IElement
    {
        public SectionWriter(string section, Dictionary<int, T> elements)
        {
            Section = section;
            Elements = elements;
        }

        public Dictionary<int, T> Elements;

        public string Section;

        public string Export()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Section.ToUpper()} BEGIN");
            foreach (var ele in Elements)
            {
                sb.AppendLine($"\t{ele.Key} {ele.Value.Export()}");
            }
            sb.AppendLine($"{Section.ToUpper()} END\r\n");
            return sb.ToString();
        }

    }



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

                    sw.WriteLine(new SectionWriter<IMotionWrapper>("MOTION", machine.MotionExs).Export());
                    sw.WriteLine(new SectionWriter<IDiEx>("DI", machine.DiExs).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("DO", machine.DoExs).Export());
                    sw.WriteLine(new SectionWriter<ICylinderEx>("CY", machine.CylinderExs).Export());
                    sw.WriteLine(new SectionWriter<IVioEx>("VIO", machine.VioExs).Export());


                    sw.WriteLine(new SectionWriter<IDiEx>("ESTOP", machine.DiEstop).Export());
                    sw.WriteLine(new SectionWriter<IDiEx>("START", machine.DiStart).Export());
                    sw.WriteLine(new SectionWriter<IDiEx>("STOP", machine.DiStop).Export());
                    sw.WriteLine(new SectionWriter<IDiEx>("RESET", machine.DiReset).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("LIGHTGREEN", machine.DoLightGreen).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("LIGHTYELLOW", machine.DoLightYellow).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("LIGHTRED", machine.DoLightRed).Export());
                    sw.WriteLine(new SectionWriter<IDoEx>("BUZZER", machine.DoBuzzer).Export());


                    sw.WriteLine(new SectionWriter<IAxisEx>("AXIS", machine.AxisExs).Export());
                    sw.WriteLine(new SectionWriter<PlatformEx>("PLATFORM", machine.Platforms).Export());
                    sw.WriteLine(new SectionWriter<Station>("STATION", machine.Stations).Export());
                }
            }
        }


        public static string SerializeToString(this StateMachine machine)
        {
            var sb = new StringBuilder();

            sb.AppendLine(new SectionWriter<IMotionWrapper>("MOTION", machine.MotionExs).Export());
            sb.AppendLine(new SectionWriter<IDiEx>("DI", machine.DiExs).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("DO", machine.DoExs).Export());
            sb.AppendLine(new SectionWriter<ICylinderEx>("CY", machine.CylinderExs).Export());
            sb.AppendLine(new SectionWriter<IVioEx>("VIO", machine.VioExs).Export());


            sb.AppendLine(new SectionWriter<IDiEx>("ESTOP", machine.DiEstop).Export());
            sb.AppendLine(new SectionWriter<IDiEx>("START", machine.DiStart).Export());
            sb.AppendLine(new SectionWriter<IDiEx>("STOP", machine.DiStop).Export());
            sb.AppendLine(new SectionWriter<IDiEx>("RESET", machine.DiReset).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("LIGHTGREEN", machine.DoLightGreen).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("LIGHTYELLOW", machine.DoLightYellow).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("LIGHTRED", machine.DoLightRed).Export());
            sb.AppendLine(new SectionWriter<IDoEx>("BUZZER", machine.DoBuzzer).Export());


            sb.AppendLine(new SectionWriter<IAxisEx>("AXIS", machine.AxisExs).Export());
            sb.AppendLine(new SectionWriter<PlatformEx>("PLATFORM", machine.Platforms).Export());
            sb.AppendLine(new SectionWriter<Station>("STATION", machine.Stations).Export());

            return sb.ToString();
        }





    }
}