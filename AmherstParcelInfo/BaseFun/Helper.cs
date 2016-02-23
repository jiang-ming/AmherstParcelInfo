using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace AmherstParcelInfo
{
    class Helper
    {
        public static IFeatureLayer FindMyFeatureLayer(IMap inMap, string inName)
        {
            string isfound = "false";
            ILayer tempLayer;
            IFeatureLayer goodLayer = new FeatureLayerClass();
            for (int i = 0; i < inMap.LayerCount; i++)
            {
                tempLayer = inMap.get_Layer(i);
                if (tempLayer is IFeatureLayer)
                {
                    if (tempLayer.Name == inName)
                    {
                        isfound = "true";
                        goodLayer = tempLayer as IFeatureLayer;
                    }
                }
            }
            //duplicate name in the map.? How we deal with it
            if (isfound == "true")
            {
                return goodLayer;
            }
            else
            {
                return null;
            }
        }

        //there was an unused function here Flashgeometry( we can take a look at it next time)

    }
}