using System;
using System.Configuration;
using System.Windows.Forms;
using System.Windows.Media;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
//using ArcDataBinding;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ArcDataBinding;
namespace AmherstParcelInfo
{
    public class BaseFun
    {
        public static IFeatureLayer ReturnParcelLayerIfExists(IMapControl3 m_mapControl)
        {
            IMap myMap, myTempMap;
            ILayer myLayer, myTempLayer;
            IFeatureLayer myParcelLayer = null;

            string parcelLayerName = ConfigurationManager.AppSettings["ParcelLayerName"];
            IArray arrayMap = m_mapControl.ReadMxMaps(m_mapControl.DocumentFilename);
            for (int i = 0; i < arrayMap.Count; i++)
            {
                myMap = arrayMap.get_Element(i) as IMap;
                if (myMap.Name == m_mapControl.Map.Name)
                {
                    myTempMap = myMap;
                    for (int j = 0; j < myMap.LayerCount; j++)
                    {
                        myLayer = myMap.get_Layer(j);

                        if (myLayer is GroupLayer)
                        {
                            ICompositeLayer myCL = myLayer as ICompositeLayer;
                            for (int k = 0; k < myCL.Count; k++)
                            {
                                myTempLayer = myCL.get_Layer(k);
                                if (myTempLayer.Name == parcelLayerName)
                                {
                                    myParcelLayer = myTempLayer as IFeatureLayer;
                                }
                            }
                        }
                        else
                        {
                            if (myLayer.Name == parcelLayerName)
                            {
                                myParcelLayer = myLayer as IFeatureLayer;
                            }
                        }

                    }
                }

            }
            return myParcelLayer;
        }

        public static IFeatureLayer ReturnTempLayerIfExists(IMapControl3 m_mapControl)
        {
            IMap myMap, myTempMap;
            ILayer myLayer, myTempLayer;
            IFeatureLayer myParcelLayer = null;

            string parcelLayerName = ConfigurationManager.AppSettings["TempLayerName"];
            IArray arrayMap = m_mapControl.ReadMxMaps(m_mapControl.DocumentFilename);
            for (int i = 0; i < arrayMap.Count; i++)
            {
                myMap = arrayMap.get_Element(i) as IMap;
                if (myMap.Name == m_mapControl.Map.Name)
                {
                    myTempMap = myMap;
                    for (int j = 0; j < myMap.LayerCount; j++)
                    {
                        myLayer = myMap.get_Layer(j);

                        if (myLayer is GroupLayer)
                        {
                            ICompositeLayer myCL = myLayer as ICompositeLayer;
                            for (int k = 0; k < myCL.Count; k++)
                            {
                                myTempLayer = myCL.get_Layer(k);
                                if (myTempLayer.Name == parcelLayerName)
                                {
                                    myParcelLayer = myTempLayer as IFeatureLayer;
                                }
                            }
                        }
                        else
                        {
                            if (myLayer.Name == parcelLayerName)
                            {
                                myParcelLayer = myLayer as IFeatureLayer;
                            }
                        }

                    }
                }

            }
            return myParcelLayer;
        }

        public static IEnvelope GetGraphicsExtent(IActiveView myActiveView)
        {
            /* Gets the combined extent of all the objects in the map. */
            IEnvelope GraphicsBounds;
            IEnvelope GraphicsEnvelope;
            IGraphicsContainer oiqGraphicsContainer;
            IPageLayout docPageLayout;
            IDisplay GraphicsDisplay;
            IElement oiqElement;

            GraphicsBounds = new EnvelopeClass();
            GraphicsEnvelope = new EnvelopeClass();
            docPageLayout = myActiveView as IPageLayout;
            GraphicsDisplay = myActiveView.ScreenDisplay;
            oiqGraphicsContainer = myActiveView as IGraphicsContainer;
            oiqGraphicsContainer.Reset();

            oiqElement = oiqGraphicsContainer.Next();
            while (oiqElement != null)
            {
                oiqElement.QueryBounds(GraphicsDisplay, GraphicsEnvelope);
                GraphicsBounds.Union(GraphicsEnvelope);
                oiqElement = oiqGraphicsContainer.Next();
            }

            return GraphicsBounds;

        }

        public static ISimpleLineSymbol GenerateSLS(double width, int r, int g, int b)
        {
            ISimpleLineSymbol simplelineSymbol = new SimpleLineSymbolClass();
            simplelineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
            simplelineSymbol.Width = width;
            IRgbColor rgbcolor = new RgbColorClass();
            rgbcolor.Red = r;
            rgbcolor.Green = g;
            rgbcolor.Blue = b;
            simplelineSymbol.Color = rgbcolor;


            return simplelineSymbol;
        }

        public static ISimpleFillSymbol GenerateSFSByColor(System.Drawing.Color color,double olwidth)
        {
            IRgbColor myColor = new RgbColorClass();
            myColor.RGB = System.Drawing.ColorTranslator.ToWin32(color);
            ISimpleLineSymbol iSLS = new SimpleLineSymbolClass();
            iSLS.Style = esriSimpleLineStyle.esriSLSSolid;
            iSLS.Width = olwidth;
            iSLS.Color = myColor;
            ISimpleFillSymbol iSFS;
            iSFS = new SimpleFillSymbol();
            iSFS.Style = esriSimpleFillStyle.esriSFSSolid;
            iSFS.Outline = iSLS;
            IRgbColor irgb = new RgbColorClass();
            irgb.RGB = System.Drawing.ColorTranslator.ToWin32(color);
            iSFS.Color = irgb;
            return iSFS;
        }

        public static Tuple<List<string>, double> TextResize(string text, double height, double width, double startSize,int rowCount)
        {
            double fontsize = startSize;
            while (fontsize > 0)
            {
                string[] originalParas = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> wrappedLines = new List<string>();
                StringBuilder actualLine = new StringBuilder();
                double actualWidth = 0;
                double spacelength = (new FormattedText(" ", CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight,
                    new Typeface("Arial"), fontsize, Brushes.Black)).WidthIncludingTrailingWhitespace;
                foreach (var paragraph in originalParas)
                {
                    string[] originalLines = paragraph.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in originalLines)
                    {

                        FormattedText formatted = new FormattedText(item + " ", CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight,
                            new Typeface("Arial"), fontsize, Brushes.Black);

                        actualWidth += formatted.WidthIncludingTrailingWhitespace;
                        if (actualWidth > width)
                        {
                            string newline = actualLine.ToString();
                            if (newline.Length > 0)
                                wrappedLines.Add(newline);
                            actualWidth = formatted.WidthIncludingTrailingWhitespace;
                            actualLine.Clear();
                            actualLine.Append(item + " ");
                        }
                        else if (actualWidth == width)
                        {
                            string newline = actualLine.Append(item + " ").ToString();
                            if (newline.Length > 0)
                                wrappedLines.Add(newline);
                            actualLine.Clear();
                            actualWidth = 0;

                        }
                        else
                        {
                            actualLine.Append(item + " ");
                        }
                    }
                    if (actualLine.Length > 0)
                    {
                        string whitespace = null;
                        if (width > actualWidth)
                            whitespace = new string(' ', ((int)((width - actualWidth) / spacelength)));
                        wrappedLines.Add(actualLine.Append(whitespace).ToString());
                    }
                    actualLine.Clear();
                    actualWidth = 0;
                }
                string union = string.Join("\n", wrappedLines);
                FormattedText preFormatted = new FormattedText(union, CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight,
                    new Typeface("Arial"), fontsize, Brushes.Black);
                if (preFormatted.Height > height || preFormatted.WidthIncludingTrailingWhitespace > width||wrappedLines.Count()>rowCount)
                    fontsize -= 0.5F;
                else
                {
                    return Tuple.Create(wrappedLines, fontsize);
                }
            }
            return null;
        }

        public static void selectAdjacentParcelsByBuffer(IFeatureLayer parcellayer, IElement myBufferElement, ListView listview, Label lblAdjacentParcelNum, Label lblNoOwnerNameParcelNum, Timer timer1, BindingSource bindingSource2, DataGridView dataGridView2)
        {
            // Spatial filter
            IFeatureClass myFC = parcellayer.FeatureClass;

            IGeometry queryGeometry = myBufferElement.Geometry;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = queryGeometry;

            spatialFilter.GeometryField = myFC.ShapeFieldName;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            //spatialFilter.SubFields = "OwnerAdd, Printkey, SBL, OBJECTID";

            //Execute the query and iterate through the cursor
            using (ComReleaser comReleaser = new ComReleaser())
            {
                MainForm.LogTxt = "";
                IFeatureCursor ParcelCursor = myFC.Search(spatialFilter, false);
                comReleaser.ManageLifetime(ParcelCursor);
                IFeature ParcelFeature = null;
                ParcelFeature = ParcelCursor.NextFeature();
                int featureNum = 0;
                int emptyNum = 0;
                string querySubtract = ""; //query to get parcels without oname

                listview.Items.Clear();
                while (ParcelFeature != null)
                {
                    //string name = Convert.ToString(ParcelFeature.get_Value(ixONAME));
                    string SBL = Convert.ToString(ParcelFeature.get_Value(MainForm.ixSBL));
                    string OBJECTID = Convert.ToString(ParcelFeature.get_Value(MainForm.ixOBJECTID));

                    string printkey = Convert.ToString(ParcelFeature.get_Value(MainForm.ixPrintkey));
                    string stnum = Convert.ToString(ParcelFeature.get_Value(MainForm.ixSTNUM));
                    string stname = Convert.ToString(ParcelFeature.get_Value(MainForm.ixSTNAME));
                    string parceladd = Convert.ToString(ParcelFeature.get_Value(MainForm.ixParcelAdd));
                    string owneradd = Convert.ToString(ParcelFeature.get_Value(MainForm.ixOWNERADD));
                    string oname = Convert.ToString(ParcelFeature.get_Value(MainForm.ixONAME));
                    if (oname.Trim() == "")
                    {
                        querySubtract = querySubtract + "Printkey ='" + printkey + "' or ";
                        ListViewItem lv = new ListViewItem();
                        lv.SubItems[0].Text = OBJECTID;
                        lv.SubItems.Add(printkey);
                        lv.SubItems.Add(SBL);
                        listview.Items.Add(lv);
                        emptyNum = emptyNum + 1;

                        //MainForm.LogTxt = MainForm.LogTxt + "\r\n" + OBJECTID + "," + printkey + "," + stnum + "," + stname + "," + parceladd + "," + owneradd + "," + oname + ",";
                        MainForm.LogTxt = MainForm.LogTxt + printkey + "\r\n";
                    }

                    featureNum = featureNum + 1;
                    ParcelFeature = ParcelCursor.NextFeature();
                }

                double ratio = emptyNum / featureNum;
                //if (ratio > 0.2)
                if (emptyNum>10)
                {
                    MessageBox.Show("More then 10 empty parcels are found. Please Contact with BrianB!");
                }

                lblAdjacentParcelNum.Text = Convert.ToString(featureNum);
                lblNoOwnerNameParcelNum.Text = Convert.ToString(emptyNum);

                if (emptyNum != 0)
                {
                    timer1.Start();
                    timer1.Interval = 200;
                }
                else
                {
                    timer1.Stop();
                    lblNoOwnerNameParcelNum.Visible = true;
                }

                //Select the features intersect with the buffer area
                IFeatureSelection myFeatselect = parcellayer as IFeatureSelection;
                myFeatselect.SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

                //fillAdjancetParcelsGridView(querySubtract, myFeatselect);
                if (querySubtract != string.Empty)
                {
                    IQueryFilter myNewFilter = new QueryFilterClass();
                    querySubtract = querySubtract.Substring(0, querySubtract.Length - 3);
                    myNewFilter.WhereClause = querySubtract;
                    myFeatselect.SelectFeatures(myNewFilter, esriSelectionResultEnum.esriSelectionResultSubtract, false);
                }
                MainForm.myCurrentSelectionSet = myFeatselect.SelectionSet;

                IFeatureLayerDefinition pFLD1 = (IFeatureLayerDefinition)parcellayer;
                IFeatureLayer myNewFL = pFLD1.CreateSelectionLayer(parcellayer.Name, true, null, null);

                ITable myTable = myNewFL as ITable;

                // Use the ESRI tablewapper class to pass it the featurelayer
                TableWrapper myWrapper1 = new TableWrapper(myTable);

                // assign the data source converted by tablewrapper to the datagridview and bindingsource
                bindingSource2.DataSource = myWrapper1;
                dataGridView2.DataSource = bindingSource2;
                // dataGridView2 in SearchWindow and TRWindow
                if (EditHelper.TheTRWindow!=null)
                {
                    string configColumnsName = ConfigurationManager.AppSettings["ParcelLayerVisableColumnNames"].ToString().ToLower();
                    if (!string.IsNullOrEmpty(configColumnsName))
                    {
                        List<string> visiableColumnsNameList = configColumnsName.Split(',').Select(p => p.Trim()).ToList();
                        foreach (DataGridViewColumn column in dataGridView2.Columns)
                        {
                            if (visiableColumnsNameList.IndexOf(column.Name.Trim().ToLower()) < 0)
                            {
                                dataGridView2.Columns[column.Index].Visible = false;
                            }
                        }
                    }
                }
                else if (EditHelper.TheSearchWindow != null)
                {
                    int n = dataGridView2.ColumnCount;
                    for (int i = 0; i < n; i++)
                    {
                        dataGridView2.Columns[i].Visible = false;
                        if (dataGridView2.Columns[i].Name == "Printkey" | dataGridView2.Columns[i].Name == "OWNERADD" | dataGridView2.Columns[i].Name == "SBL")
                        {
                            dataGridView2.Columns[i].Visible = true;
                        }
                    }
                }
               
                


                //    if (!string.IsNullOrEmpty(configColumnsName))
                //    {
                //        List<string> visiableColumnsNameList = configColumnsName.Split(',').Select(p => p.Trim()).ToList();
                //        foreach (DataGridViewColumn column in dataGridView2.Columns)
                //        {
                //            if (visiableColumnsNameList.IndexOf(column.Name.Trim().ToLower()) < 0)
                //            {
                //                dataGridView2.Columns[column.Index].Visible = false;
                //            }
                //        }
                //    }
                pFLD1.DefinitionExpression = "";
                myFeatselect.Clear();
                dataGridView2.Refresh();
            }
        }

        //public static IElement BufferSelectedFeature(IFeature myFeature, double bufferDistance)
        //{
        //    IElement myElement = new PolygonElementClass();

        //    if (myFeature != null && bufferDistance > 0)
        //    {
        //        //Buffer the selected feature
        //        ITopologicalOperator topologicalOperator;
        //        topologicalOperator = (ITopologicalOperator)myFeature.Shape;

        //        IRgbColor bufferColor = new RgbColorClass();
        //        bufferColor.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.OrangeRed);

        //        ISimpleFillSymbol fillSymbolForBuffer = new SimpleFillSymbolClass();
        //        ESRI.ArcGIS.Display.ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbolClass();
        //        simpleLineSymbol.Width = 3;
        //        simpleLineSymbol.Color = bufferColor;
        //        simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDash;
        //        fillSymbolForBuffer.Outline = simpleLineSymbol;
        //        fillSymbolForBuffer.Style = esriSimpleFillStyle.esriSFSNull;

        //        IFillShapeElement myFillShapeForBuffer = new PolygonElementClass();
        //        myFillShapeForBuffer.Symbol = fillSymbolForBuffer;
        //        myElement = myFillShapeForBuffer as IElement;
        //        myElement.Geometry = topologicalOperator.Buffer(bufferDistance);
        //    }

        //    return myElement;
        //}

        //public static void ZoomToSelectedFeatureExtent(IActiveView myActiveView, IFeature selectedFeature, int ratio)
        //{
        //    IEnvelope myEnv = selectedFeature.Shape.Envelope;
        //    myEnv.Expand(ratio, ratio, true);

        //    myActiveView.Extent = myEnv;
        //    myActiveView.Refresh();
        //    myActiveView.ScreenDisplay.UpdateWindow();
        //}


        //public static IElement SetTheLabelForSelectedFeature(string selectedFeatureStreetNumber, double Coord_x, double Coord_y)
        //{
        //    IPoint pPoint = new PointClass();
        //    pPoint.PutCoords(Coord_x, Coord_y);

        //    // set the color of the label
        //    IRgbColor pRGBColor = new RgbColorClass();
        //    pRGBColor.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.White);

        //    //set the symbol of the label
        //    ITextSymbol pTextSymbol = new TextSymbolClass();
        //    pTextSymbol.Color = pRGBColor;
        //    pTextSymbol.Size = 8;
        //    //set the textelement of the label
        //    ITextElement pTextElement = new TextElementClass();
        //    pTextElement.Text = selectedFeatureStreetNumber;
        //    pTextElement.Symbol = pTextSymbol;

        //    IElement pTextEle = pTextElement as IElement;
        //    pTextEle.Geometry = pPoint;

        //    return pTextEle;
        //}
    }
}