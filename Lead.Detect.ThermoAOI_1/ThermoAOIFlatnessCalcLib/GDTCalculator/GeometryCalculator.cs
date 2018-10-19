using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1.Calculators;
using MachineUtilityLib.UtilProduct;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalculator
{
    [XmlInclude(typeof(A117WithFinCalculator))]
    [XmlInclude(typeof(A117NoFinCalculator))]
    [XmlInclude(typeof(A147WithFinCalculator))]
    [XmlInclude(typeof(A147NoFinCalculator))]
    [XmlInclude(typeof(X1311Calculator))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GeometryCalculator : UserSettings<GeometryCalculator>
    {
        public GeometryCalculator()
        {
        }

        [Description("产品类型名称")]
        public string ProductName { get; set; }

        [Description("产品形位公差测试项")]
        public List<GDTCalc> GeoCalcs { get; protected set; } = new List<GDTCalc>();

        public virtual Thermo1Product Calculate(Thermo1Product productData)
        {
            //no reentrant
            lock (this)
            {
                //clear
                foreach (var g in GeoCalcs)
                {
                    g.Clear();
                }

                if (productData.ProductType == ProductName)
                {
                    var rawPos = new List<PosXYZ>();
                    rawPos.AddRange(productData.RawDataUp);
                    rawPos.AddRange(productData.RawDataDown);

                    try
                    {
                        //calculate geometry

                        //calculate datum geos
                        var datumGeos = GeoCalcs.FindAll(g => g.IsDatum);
                        foreach (var datumGeo in datumGeos)
                        {
                            var pos = rawPos.FindAll(p => p.Name == datumGeo.SourcePos);
                            if (pos.Count > 0)
                            {
                                var spc = productData.SPCItems.FirstOrDefault(s => s.SPC == datumGeo.Name);
                                if (spc == null)
                                {
                                    throw new Exception($"{datumGeo.Name} SPC Error");
                                }

                                datumGeo.SetSpec(spc.SPEC, spc.UpLimit, spc.DownLimit);
                                datumGeo.DoCalc(pos);
                                datumGeo.UpdateValue(ref spc);
                            }
                        }

                        //calculate other geos
                        var otherGeos = GeoCalcs.FindAll(g => !g.IsDatum);
                        foreach (var otherGeo in otherGeos)
                        {
                            var pos = rawPos.FindAll(p => p.Name == otherGeo.SourcePos);
                            if (pos.Count > 0)
                            {
                                var spc = productData.SPCItems.FirstOrDefault(s => s.SPC == otherGeo.Name);
                                if (spc == null)
                                {
                                    throw new Exception($"{otherGeo.Name} SPC Error");
                                }

                                otherGeo.SetDatum(GeoCalcs.First(g => g.Name == otherGeo.DatumName).Datum);
                                otherGeo.SetSpec(spc.SPEC, spc.UpLimit, spc.DownLimit);
                                otherGeo.DoCalc(pos);
                                otherGeo.UpdateValue(ref spc);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        productData.Error = $"GeometryCalcError";
                        productData.Status = ProductStatus.ERROR;
                    }
                }
                else
                {
                    productData.Error = $"GeometryCalcError";
                    productData.Status = ProductStatus.ERROR;
                }

                return productData;
            }
        }


        public void Export(string file)
        {
            var sb = new StringBuilder();

            sb.Append(ProductName);
            sb.Append("\r\n");

            foreach (var g in GeoCalcs)
            {
                sb.Append($"{g.Name},{g.GDTType},{g.DatumName},{g.SourcePos}\r\n");
            }

            using (var fs = new FileStream(file, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(sb.ToString());
                }
            }
        }


        public static void Import(string desc)
        {
            var data = desc.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var g = new GeometryCalculator();
            g.ProductName = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                var geoData = data[i].Split(',');
                if (geoData.Length >= 4)
                {
                    switch (geoData[1])
                    {
                        case nameof(GDTType.Flatness):
                            g.GeoCalcs.Add(new FlatnessCalc() { Name = geoData[0], DatumName = geoData[2], SourcePos = geoData[3] });
                            break;
                        case nameof(GDTType.Parallelism):
                            g.GeoCalcs.Add(new ParallelismCalc() { Name = geoData[0], DatumName = geoData[2], SourcePos = geoData[3] });
                            break;
                        case nameof(GDTType.ProfileOfSurface):
                            g.GeoCalcs.Add(new ProfileOfSurfaceCalc() { Name = geoData[0], DatumName = geoData[2], SourcePos = geoData[3] });
                            break;
                    }
                }
            }
        }


        public override string ToString()
        {
            return $"{GetType().Name}\r\n{ProductName}\r\n\r\n{string.Join("\r\n\r\n", GeoCalcs.Select(g => g.ToString()))}";
        }

        public override bool CheckIfNormal()
        {
            if (string.IsNullOrEmpty(ProductName))
            {
                return false;
            }

            return true;
        }
    }
}