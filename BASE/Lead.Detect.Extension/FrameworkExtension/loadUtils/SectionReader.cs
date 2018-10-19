using System;
using System.Linq;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.elementExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.loadUtils
{
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
                            var platform = machine.Platforms.Values.FirstOrDefault(p => p.Name == IndentSection);
                            if (platform != null)
                            {
                                platform.Axis[_indentIndex] = null;
                            }

                            _indentIndex++;
                        }
                        else
                        {
                            IAxisEx axis = new AxisEx();
                            axis.Import(line, machine);
                            axis = machine.Find<IAxisEx>(axis.Name);

                            var platform = machine.Platforms.Values.FirstOrDefault(p => p.Name == IndentSection);
                            if (platform != null)
                            {
                                platform.Axis[_indentIndex] = axis;
                            }

                            _indentIndex++;
                        }
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
                    throw new Exception($"Not Support Section - {Section}");
            }
        }
    }
}