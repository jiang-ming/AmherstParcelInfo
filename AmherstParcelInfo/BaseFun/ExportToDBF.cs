using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseUI;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using Microsoft.Office.Interop.Excel;


namespace AmherstParcelInfo
{
    public class DataTableToDBF
    {
        //change datagridview into datatable
        // parameter name is "dv"

        public static System.Data.DataTable dvtodt(DataGridView dv)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataColumn dc;
            for (int i = 0; i < dv.ColumnCount; i++)
            {
                dc = new System.Data.DataColumn();
                dc.ColumnName = dv.Columns[i].HeaderText.ToString();
                dt.Columns.Add(dc);
            }
            for (int j = 0; j < dv.Rows.Count; j++)
            {
                System.Data.DataRow dr = dt.NewRow();
                for (int x = 0; x < dv.Columns.Count; x++)
                {
                    dr[x] = dv.Rows[j].Cells[x].Value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        // export the datagridview in tio excel
        //public static void ExportToExcel(DataGridView dgv,string filename)
        //{
        //    //get datatable
        //    System.Data.DataTable dt = dvtodt(dgv);
  
        //        MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerHourglass;
        //        ExportToExcel(dt,filename);
        //        MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            
        //}

        public static void ExportToExcel(DataGridView dgv, string filename)
        {
            System.Data.DataTable dt = dvtodt(dgv);
          
            if (dt == null) return;
            Microsoft.Office.Interop.Excel._Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("can not create excel object");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range = null;
            long totolCount = dt.Rows.Count;
            long rowRead = 0;
            double percent = 0;

            // write data into excel
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);
                range.EntireColumn.AutoFit();
                range.EntireRow.AutoFit();

            }

            //write the content
            for (int r = 0; r < dt.DefaultView.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.DefaultView[r][i];
                }
                rowRead++;
                percent = ((double)(100 * rowRead)) / totolCount;
                System.Windows.Forms.Application.DoEvents();
            }
            range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            if (dt.Columns.Count > 1)
            {
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;

            }
            try
            {
                workbook.Saved = true;
                //Use saveas instead of savecopyas to avoid format error message
                workbook.SaveCopyAs(filename);
                // XlFileFormat.xlExcel8 is inportant for 97-03 old Excel format
                //workbook.SaveAs(filename, XlFileFormat.xlExcel8, Type.Missing, Type.Missing,
                    //false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                #region Hidden folder and file
                try
                {
                    string ExtFile = System.IO.Path.GetExtension(filename);
                    string FileName_temp = System.IO.Path.GetFileNameWithoutExtension(filename) + "_hidden";
                    string FilePath = System.IO.Path.GetDirectoryName(filename);
                    string datea = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
                    string newFileDirectory = FilePath + @"\" + datea;
                    DirectoryInfo source = new DirectoryInfo(newFileDirectory);

                    if (source.Exists == false)
                    {
                        Directory.CreateDirectory(FilePath + @"\" + datea);
                    }
                    else if (source.Exists == true)
                    {
                    }
                    FileInfo f_directoy = new FileInfo(newFileDirectory);
                    f_directoy.Attributes = FileAttributes.Hidden;

                    string newFilePath = FilePath + @"\" + datea + @"\" + FileName_temp + ExtFile;
                    FileInfo f = new FileInfo(newFilePath);
                    if (f.Exists == true)
                    {
                        string timea = string.Format("{0:hh_mm_ss}", DateTime.Now);
                        FileName_temp = FileName_temp + "_" + timea;
                        newFilePath = FilePath + @"\" + datea + @"\" + FileName_temp + ExtFile;
                    }
                    else
                    {

                    }
                    File.Delete(newFilePath);
                    File.Copy(filename, newFilePath, true);
                    f = new FileInfo(newFilePath);
                    f.Attributes = FileAttributes.Hidden;
                }
                catch
                {
                    MessageBox.Show("Double copy is not allowed, which was not expected");
                }
                #endregion Hidden folder and file

            }
            catch (Exception)
            {
                MessageBox.Show("error");
                return;
            }
            workbook.Close();
            if (xlApp != null)
            {
                xlApp.Workbooks.Close();
                xlApp.Quit();
                int generation = System.GC.GetGeneration(xlApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
                System.GC.Collect(generation);
            }
            GC.Collect();
            System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            System.DateTime startTime = new DateTime();
            int m, killID = 0;
            for (m = 0; m < excelProc.Length; m++)
            {
                if (startTime < excelProc[m].StartTime)
                {
                    startTime = excelProc[m].StartTime;
                    killID = m;
                }

            }
            if (excelProc[killID].HasExited == false)
            {
                excelProc[killID].Kill();
            }
     
        }



        public static void ExportToDBF(string FileName)
        {
           
            //Access the file geodatabase 
            string gdbPath = ConfigurationManager.AppSettings["FullGDBPath"];

            if (!Directory.Exists(gdbPath))
            {
                MessageBox.Show("No right path found");
                return;
            }

            IWorkspaceFactory myWSF = new FileGDBWorkspaceFactoryClass();
            IFeatureWorkspace myFWS = (IFeatureWorkspace)myWSF.OpenFromFile(gdbPath, 0);

            ITable myTable = myFWS.OpenTable(ConfigurationManager.AppSettings["ParcelLayerName"]);
            IDataset myDataSet = myTable as IDataset;

            ITableName myInTablename = myDataSet.FullName as ITableName;
            IDatasetName myInDsName = myInTablename as IDatasetName;

            IWorkspaceFactory pWokespaceFact = new ShapefileWorkspaceFactory();
            string FilePath_output = System.IO.Path.GetDirectoryName(FileName);
            IWorkspace pWksp = pWokespaceFact.OpenFromFile(FilePath_output, 0);
            IDataset pWksPdataset = pWksp as IDataset;
            IWorkspaceName pWkSpName = pWksPdataset.FullName as IWorkspaceName;
            IDatasetName pOutDsName = new TableNameClass();
            string FileName_output = System.IO.Path.GetFileNameWithoutExtension(FileName);
            FileInfo f_name = new FileInfo(FileName);
            if (f_name.Exists == true)
            {
                File.Delete(FileName);
            }

            pOutDsName.Name = FileName_output;
            pOutDsName.WorkspaceName = pWkSpName;

            if (MainForm.myCurrentSelectionSet != null)
            {
                //if (MainForm.myExportToDBFSpatialFilter == null)
                //{
                //    MessageBox.Show("error!");
                //    return;
                //}
                IFeatureDataConverter2 myFDC = new FeatureDataConverterClass();
                myFDC.ConvertTable(myInDsName, null, MainForm.myCurrentSelectionSet, pOutDsName, myTable.Fields, "", 0, 0);
                MainForm.myCurrentSelectionSet = null;


            }
            else
            {
                MessageBox.Show("No feature selected!");
                return;
            }

            // show the success
        
            MessageBox.Show("Your *.dbf was exported successfully!");
        }

        public static void FillMapLayoutElements(string customizedTitle, string layoutScale, string bufferDis)
        {

            IPageLayoutControl2 pageLayoutControl = MainForm.m_pageLayoutControl;
            IActiveView myLayoutActive = MainForm.m_pageLayoutControl.PageLayout as IActiveView;
            IGraphicsContainer myGC = MainForm.m_pageLayoutControl.PageLayout as IGraphicsContainer;
            IMap myMap = myLayoutActive.FocusMap;
            IMapFrame myMF = (IMapFrame)myGC.FindFrame(myMap);
            IGraphicsContainerSelect gcs = pageLayoutControl.PageLayout as IGraphicsContainerSelect;
            Tuple<List<string>, double> tupleLegendText = new Tuple<List<string>, double>(null, 0);

            IElement titletag1 = pageLayoutControl.FindElementByName("TitleTag1");
            IElement titletag2 = pageLayoutControl.FindElementByName("TitleTag2");
            IElement parcelSelected = pageLayoutControl.FindElementByName("SelectedParcel");

            IElement txe_Legend_L = pageLayoutControl.FindElementByName("Legend_L");
            IElement txe_Legend_S = pageLayoutControl.FindElementByName("Legend_S");
            IElement txe_Legend_R = pageLayoutControl.FindElementByName("Legend_R");
            IElement txe_Legend_TL = pageLayoutControl.FindElementByName("Legend_TL");
            IElement txe_Legend_TS = pageLayoutControl.FindElementByName("Legend_TS");
            IElement txe_Legend_TR = pageLayoutControl.FindElementByName("Legend_TR");

            IElement eleTitDelete = pageLayoutControl.FindElementByName("Title");
            IElement eleBufDelete = pageLayoutControl.FindElementByName("Buffer");
            IElement eleDesDelete = pageLayoutControl.FindElementByName("Description");



            if (layoutScale == "Auto")
            {
                //if (envLayout != null)
                //{
                //    //myLayoutActive. = envLayout;
                //}
            }
            else
            {
                myLayoutActive.FocusMap.MapScale = Convert.ToDouble(layoutScale) * 12;
            }

            #region Delete old elements
            //Search the myGC and clear the last elements for layout
            //Delete Description text element in the title box
            if (eleDesDelete != null)
                myGC.DeleteElement(eleDesDelete);
            // Delete the Title text element
            if (eleTitDelete != null)
                myGC.DeleteElement(eleTitDelete);
            if (eleBufDelete != null)
                myGC.DeleteElement(eleBufDelete);

            if ((EditHelper.TheTRWindow == null) && (EditHelper.TheSearchWindow != null))
            {
                gcs.UnselectAllElements();
                gcs.SelectElement(titletag1);
                gcs.SelectElement(parcelSelected);
                pageLayoutControl.GraphicsContainer.BringToFront(gcs.SelectedElements);
                gcs.UnselectAllElements();
                gcs.SelectElement(titletag2);
                gcs.SelectElement(txe_Legend_L);
                gcs.SelectElement(txe_Legend_S);
                gcs.SelectElement(txe_Legend_R);
                gcs.SelectElement(txe_Legend_TL);
                gcs.SelectElement(txe_Legend_TS);
                gcs.SelectElement(txe_Legend_TR);
                pageLayoutControl.GraphicsContainer.SendToBack(gcs.SelectedElements);
                gcs.UnselectAllElements();
            }
            else if ((EditHelper.TheTRWindow != null) && (EditHelper.TheSearchWindow == null))
            {
                gcs.UnselectAllElements();
                gcs.SelectElement(titletag2);
                pageLayoutControl.GraphicsContainer.BringToFront(gcs.SelectedElements);
                gcs.UnselectAllElements();
                gcs.SelectElement(titletag1);
                gcs.SelectElement(parcelSelected);
                pageLayoutControl.GraphicsContainer.SendToBack(gcs.SelectedElements);
                gcs.UnselectAllElements();

                //Add Description of the map
                string description = EditHelper.TheTRWindow.Description;
                double fontsizeDes = EditHelper.Descriptionsize;
                //TextSymbol 
                ITextSymbol pDescriptionSymbol1 = new TextSymbolClass();
                IRgbColor pdesRGB = new RgbColorClass();
                pdesRGB.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Black);
                pDescriptionSymbol1.Color = pdesRGB;
                pDescriptionSymbol1.Size = fontsizeDes;
                pDescriptionSymbol1.Font.Bold = false;
                pDescriptionSymbol1.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull;
                pDescriptionSymbol1.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                //Geometry
               ESRI.ArcGIS.Geometry.IPoint desPoint = new PointClass();
                desPoint.PutCoords(0.3, 9.65);
                //TextElement
                ITextElement descriptionEle = new TextElementClass();
                descriptionEle.Symbol = pDescriptionSymbol1;
                descriptionEle.Text = description;
                IElement eledes = descriptionEle as IElement;
                eledes.Geometry = desPoint;
                IElementProperties3 eleproperties = eledes as IElementProperties3;
                eleproperties.Name = "Description";
                myGC.AddElement(eledes, 0);

                #region Change legend area
                gcs.UnselectAllElements();

                //Single side
                if ((EditHelper.TheTRWindow.RType_L == EditHelper.TheTRWindow.RType_R) || (EditHelper.TheTRWindow.RType_L == "Null") || (EditHelper.TheTRWindow.RType_R == "Null"))
                {
                    IRgbColor legendcolor = new RgbColorClass();
                    string legendtxt;
                    if (EditHelper.TheTRWindow.RType_R == "Null")
                    {
                        legendcolor.RGB = System.Drawing.ColorTranslator.ToWin32(EditHelper.TheTRWindow.RColor_L);
                        legendtxt = EditHelper.TheTRWindow.RType_L;
                    }
                    else
                    {
                        legendcolor.RGB = System.Drawing.ColorTranslator.ToWin32(EditHelper.TheTRWindow.RColor_R);
                        legendtxt = EditHelper.TheTRWindow.RType_R;
                    }

                    if (legendtxt == null) return;

                    //change legend elements order
                    gcs.SelectElement(txe_Legend_S);
                    gcs.SelectElement(txe_Legend_TS);
                    pageLayoutControl.GraphicsContainer.BringToFront(gcs.SelectedElements);
                    gcs.UnselectAllElements();
                    gcs.SelectElement(txe_Legend_L);
                    gcs.SelectElement(txe_Legend_R);
                    gcs.SelectElement(txe_Legend_TL);
                    gcs.SelectElement(txe_Legend_TR);
                    pageLayoutControl.GraphicsContainer.SendToBack(gcs.SelectedElements);
                    gcs.UnselectAllElements();

                    tupleLegendText = BaseFun.TextResize(legendtxt, 12, 80, 8,3);
                    //Change Legend text and size
                    ITextElement elelegendText = txe_Legend_TS as ITextElement;
                    elelegendText.Text = string.Join(@"\n", tupleLegendText.Item1);
                    ITextSymbol textsymbol_S = new TextSymbolClass(); /////Change Symbol
                    textsymbol_S.Size = tupleLegendText.Item2;
                    textsymbol_S.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                    textsymbol_S.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    ESRI.ArcGIS.Geometry.IPoint ptS = new PointClass();
                    ptS.PutCoords(0.8, 0.5651);
                    txe_Legend_TS.Geometry = ptS;
                    elelegendText.Symbol = textsymbol_S;// assign new symbol

                    //Change Legend COlor
                    IFillShapeElement legendrectS = txe_Legend_S as IFillShapeElement;
                    ISimpleFillSymbol simplefillsymbolS = new SimpleFillSymbolClass();
                    simplefillsymbolS.Color = legendcolor;
                    ISimpleLineSymbol simplelinesymbolS = BaseFun.GenerateSLS(0.1, 50, 50, 50);
                    simplefillsymbolS.Outline = simplelinesymbolS;
                    legendrectS.Symbol = simplefillsymbolS;
                }
                //Double side
                else
                {
                    IRgbColor legendcolorL = new RgbColorClass();
                    legendcolorL.RGB = System.Drawing.ColorTranslator.ToWin32(EditHelper.TheTRWindow.RColor_L);
                    string legendtxtL = EditHelper.TheTRWindow.RType_L;
                    if (legendtxtL == null) return;

                    IRgbColor legendcolorR = new RgbColorClass();
                    legendcolorR.RGB = System.Drawing.ColorTranslator.ToWin32(EditHelper.TheTRWindow.RColor_R);
                    string legendtxtR = EditHelper.TheTRWindow.RType_R;
                    if (legendtxtR == null) return;
                    string strLL, strLR;
                    double dSizeLL, dSizeLR;

                    tupleLegendText = BaseFun.TextResize(legendtxtL, 12, 80, 8,3);
                    strLL = string.Join(@"\n", tupleLegendText.Item1);
                    dSizeLL = tupleLegendText.Item2;

                    tupleLegendText = BaseFun.TextResize(legendtxtR, 12, 80, 8,3);
                    strLR = string.Join(@"\n", tupleLegendText.Item1);
                    dSizeLR = tupleLegendText.Item2;

                    if (dSizeLL > dSizeLR)
                    {
                        tupleLegendText = BaseFun.TextResize(legendtxtL, 12, 80, dSizeLR,3);
                        strLL = string.Join(@"\n", tupleLegendText.Item1);
                        dSizeLL = tupleLegendText.Item2;
                    }
                    else if (dSizeLL < dSizeLR)
                    {
                        tupleLegendText = BaseFun.TextResize(legendtxtR, 12, 80, dSizeLL,3);
                        strLR = string.Join(@"\n", tupleLegendText.Item1);
                        dSizeLR = tupleLegendText.Item2;
                    }

                    gcs.SelectElement(txe_Legend_L);
                    gcs.SelectElement(txe_Legend_R);
                    gcs.SelectElement(txe_Legend_TL);
                    gcs.SelectElement(txe_Legend_TR);
                    pageLayoutControl.GraphicsContainer.BringToFront(gcs.SelectedElements);
                    gcs.UnselectAllElements();
                    gcs.SelectElement(txe_Legend_S);
                    gcs.SelectElement(txe_Legend_TS);
                    pageLayoutControl.GraphicsContainer.SendToBack(gcs.SelectedElements);
                    gcs.UnselectAllElements();

                    //////////First
                    //Change Legend text and size
                    ITextElement elelegendTextL = txe_Legend_TL as ITextElement;
                    elelegendTextL.Text = strLL;
                    ITextSymbol textsymbol_L = new TextSymbolClass(); /////Change Symbol
                    textsymbol_L.Size = dSizeLL;
                    textsymbol_L.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                    textsymbol_L.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    ESRI.ArcGIS.Geometry.IPoint ptL = new PointClass();
                    ptL.PutCoords(0.8, 0.6711);
                    txe_Legend_TL.Geometry = ptL;
                    elelegendTextL.Symbol = textsymbol_L;// assign new symbol

                    //Change Legend Color
                    IFillShapeElement legendrectL = txe_Legend_L as IFillShapeElement;
                    ISimpleFillSymbol simplefillsymbolL = new SimpleFillSymbolClass();
                    simplefillsymbolL.Color = legendcolorL;
                    ISimpleLineSymbol simplelinesymbolL = BaseFun.GenerateSLS(0.1, 50, 50, 50);
                    simplefillsymbolL.Outline = simplelinesymbolL;
                    legendrectL.Symbol = simplefillsymbolL;

                    ///////Second

                    //Change Legend text and size
                    ITextElement elelegendTextR = txe_Legend_TR as ITextElement;
                    elelegendTextR.Text = strLR;
                    ITextSymbol textsymbol_R = new TextSymbolClass(); /////Change Symbol
                    textsymbol_R.Size = dSizeLR;
                    textsymbol_R.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                    textsymbol_R.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    ESRI.ArcGIS.Geometry.IPoint ptR = new PointClass();
                    ptR.PutCoords(0.8, 0.4591);
                    txe_Legend_TR.Geometry = ptR;
                    elelegendTextR.Symbol = textsymbol_R;// assign new symbol

                    //Change Legend COlor
                    IFillShapeElement legendrectR = txe_Legend_R as IFillShapeElement;
                    ISimpleFillSymbol simplefillsymbolR = new SimpleFillSymbolClass();
                    simplefillsymbolR.Color = legendcolorR;
                    ISimpleLineSymbol simplelinesymbolR = BaseFun.GenerateSLS(0.1, 50, 50, 50);
                    simplefillsymbolR.Outline = simplelinesymbolR;
                    legendrectR.Symbol = simplefillsymbolR;
                }
                #endregion change legend area

            }

            #endregion delete old elements
            # region Unused
            # region Add a scale text
            //// CREATE THE ENVELOPE TO DEFINE THE LOCATION OF THE 
            //// MAP SURROUND (AN ENVELOPE IN THE LOWER LEFT 
            //IEnvelope pEnv1 = new EnvelopeClass();
            //pEnv1.PutCoords(3.5, 0.5, 4, 0.8);

            ////When creating a map surround, need to speciy the item UID 
            //UID pID = new UIDClass();
            //pID.Value = "esriCarto.ScaleText";

            //// CREATE THE SURROUND FRAME USING THE UID for scale bar 

            //IMapSurroundFrame pMSF = myMF.CreateSurroundFrame(pID, null);
            //IMapSurround pMapSurr = pMSF.MapSurround;
            //pMapSurr.Name = "ScaleText";

            //// CREATE AN ELEMENT AND ASSIGN IT THE MAP SURROUND FRAME
            //// AND THE ENVELOPE GEOMETRY
            //IElement pEE1 = (IElement)pMSF;

            //IScaleText pScaleText = new ScaleTextClass();
            //pScaleText = (IScaleText)pMapSurr;
            //INumericFormat pNumericFormat = new NumericFormatClass();
            //pNumericFormat.UseSeparator = false;
            //pNumericFormat.RoundingValue = 2;


            //pScaleText.NumberFormat = pNumericFormat as INumberFormat;
            //pScaleText.PageUnits = esriUnits.esriInches;
            //pScaleText.PageUnitLabel = "inch";


            ////Assign the Map Units to be feet on the Text Scale
            //pScaleText.MapUnits = esriUnits.esriFeet;
            //pScaleText.MapUnitLabel = "feet";

            //IRgbColor pRGB_scale = new RgbColorClass();
            //pRGB_scale.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Black);

            ////pScaleText.Style = esriScaleTextStyleEnum.esriScaleTextCustom;
            //ITextSymbol stTextSymbol = new TextSymbolClass();
            //stTextSymbol.Size = 14;
            //stTextSymbol.Color = pRGB_scale;
            ////stTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            ////stTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;

            //pScaleText.Symbol = stTextSymbol;


            //// ADD THE ELEMENT AND REDRAW MAP
            ////IPoint ptst = new PointClass();
            ////ptst.PutCoords(3.75, 0.46);
            //pEE1.Geometry = pEnv1;
            ////pEE1.Geometry = ptst;
            //myGC.AddElement(pEE1, 0);
            //myLayoutActive.Refresh();

            # endregion Add a scale text

            #region Add a north arrow

            //IEnvelope iEnv_NA = new EnvelopeClass();
            //iEnv_NA.PutCoords(7.3, 0.14, 8.1, 0.93);
            //ESRI.ArcGIS.esriSystem.UID uid2 = new UIDClass();
            //uid2.Value = "esriCarto.markerNorthArrow";

            //// create a surround 

            //pMSF = myMF.CreateSurroundFrame(uid2, null);
            //pMapSurr = pMSF.MapSurround;
            //pMapSurr.Name = "NorthArrow";

            ////Add the envelop geometry
            //IElement pEE2 = (IElement)pMSF;

            //pEE2.Geometry = iEnv_NA;
            //myGC.AddElement(pEE2, 0);
            //myLayoutActive.Refresh();

            #endregion Add a north arrow
            #endregion Unused

            #region Add text elements (buffer and title) for both application 1 and application 2
            //Add title of the map
            ESRI.ArcGIS.Geometry.IPoint ptPoint = new PointClass();
            ptPoint.PutCoords(1.925, 10.12);

            double titlesize = EditHelper.Titlesize;

            IRgbColor pRGB = new RgbColorClass();
            pRGB.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Navy);

            ITextSymbol pTextSymbol1 = new TextSymbolClass();
            pTextSymbol1.Color = pRGB;
            pTextSymbol1.Size = titlesize;
            pTextSymbol1.Font.Bold = true;
            pTextSymbol1.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            pTextSymbol1.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;

            ITextElement docTextEle = new TextElementClass();
            docTextEle.Text = customizedTitle;
            docTextEle.Symbol = pTextSymbol1;
            IElement docEle = docTextEle as IElement;
            docEle.Geometry = ptPoint;
            IElementProperties3 elepropertiesTit = docEle as IElementProperties3;
            elepropertiesTit.Name = "Title";
            myGC.AddElement(docEle, 0);
            MainForm.PreviousParceladd = docTextEle.Text;

            //Add buffer distance to lengend
            ITextElement docTextBuf = new TextElementClass();
            ESRI.ArcGIS.Geometry.IPoint ptPoint2 = new PointClass();
            ITextSymbol pTextSymbol_Buf = new TextSymbolClass();
            IRgbColor pRGB_buf = new RgbColorClass();
            pRGB_buf.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.OrangeRed);
            pTextSymbol_Buf.Color = pRGB_buf;
            pTextSymbol_Buf.Size = 7;
            pTextSymbol_Buf.Font.Bold = true;
            pTextSymbol_Buf.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            pTextSymbol_Buf.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
            ptPoint2.PutCoords(0.8, 0.2);

            docTextBuf.Text = "Buffer " + bufferDis + "'";
            docTextBuf.Symbol = pTextSymbol_Buf;
            IElement docEle3 = docTextBuf as IElement;
            docEle3.Geometry = ptPoint2;

            IElementProperties3 elePropertiesBuffer = docEle3 as IElementProperties3;
            elePropertiesBuffer.Name = "Buffer";
            myGC.AddElement(docEle3, 0);

            MainForm.PreviousBufferadd = docTextBuf.Text;

            #endregion


            myLayoutActive.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            myLayoutActive.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
            myLayoutActive.Refresh();
        }

    }
}
