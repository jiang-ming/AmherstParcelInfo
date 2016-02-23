using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
namespace AmherstParcelInfo
{
    public class parcel
    {
        public int OID { get; set; }
        public string PrintKey { get; set; }
        //public string SBL { get; set; }
        public string STNUM { get; set; }
        public string STNAME { get; set; }
        public string ParcelAdd { get; set; }
        public IPolygon Shape { get; set; }
    }
}