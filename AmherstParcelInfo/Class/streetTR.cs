using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
namespace AmherstParcelInfo
{
    //never used
    public class streetTR
    {
        public int STID { get; set; }
        public int Width { get; set; }
        public string TYPE { get; set; }
        public IPolyline Polyline { get; set; }
    }
}