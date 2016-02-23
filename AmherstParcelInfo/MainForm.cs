using System;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using ArcDataBinding;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using System.ComponentModel;
using ESRI.ArcGIS.Output;
using System.Runtime.InteropServices;

namespace AmherstParcelInfo
{
    public sealed partial class MainForm : Form
    {
        #region class public members
        public static IMapControl3 m_mapControl = null;
        public static IPageLayoutControl2 m_pageLayoutControl = null;
        public static IFeatureLayer myParcelLayer;
        public static IFeatureLayer myTempLayer;
        public static string PreviousParceladd = "";
        public static string PreviousBufferadd = "";
        public static int sum_n;
        public static int sum_NoOwnername;
        public static TableWrapper tableWrapper;
        public static IDataset myDataSet;
        public static ITable myTable;
        public static IFeatureSelection myFeatselect;
        public static ISelectionSet mySelSet;
        public static bool isActivated_SelectParcelByLine = false;
        public static bool isActivated_SelectByLine = false;
        public static bool isActivated_MapClickParcelSelect = false;
        public static bool isActivated_RestrictedStreet = false;
        public static ISelectionSet myCurrentSelectionSet;
        public static string LogTxt="";
        //public static ISpatialFilter myExportToDBFSpatialFilter;
        //public static string TR_Map_Title;
        //public static string TR_Description;
        public static int ixOBJECTID, ixPrintkey, ixSTNUM, ixSTNAME, ixParcelAdd, ixONAME, ixSBL, ixOWNERADD;
        #endregion

        #region class private members
        private static ControlsSynchronizer m_controlsSynchronizer = null;
        private string m_mapDocumentName = string.Empty;

        private INewLineFeedback m_lineFeedback;
        private bool m_isMouseDown = false;
        private string fileName = string.Empty;
        private SelectedFeaturesForm oldSelectedForm;
        private ContextMenu1 m_contextMenu = new ContextMenu1();
        private IToolbarControl m_toolbarControl = null;
        //private SearchWindow oldSearchForm;
        //private SearchWindow currentSearchWindow { get; set; }
        //private TRWindow oldTRWindow;
        //private TRWindow currentTRWindow { get; set; }

        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //get the MapControl, pageLayout Control and ToolbarControl
                m_mapControl = (IMapControl3)axMapControl1.Object;
                m_pageLayoutControl = (IPageLayoutControl2)axPageLayoutControl1.Object;
                m_toolbarControl = (IToolbarControl)axToolbarControl1.Object;

                //initialize the controls synchronization class
                m_controlsSynchronizer = new ControlsSynchronizer(m_mapControl, m_pageLayoutControl);
                //bind the controls together (both point at the same map) and set the MapControl as the active control
                m_controlsSynchronizer.BindControls(true);

                //add the framework controls (TOC and Toolbars) in order to synchronize then when the
                //active control changes (call SetBuddyControl)
                m_controlsSynchronizer.AddFrameworkControl(axToolbarControl1.Object);
                //m_controlsSynchronizer.AddFrameworkControl(axToolbarControl2.Object);
                m_controlsSynchronizer.AddFrameworkControl(axTOCControl1.Object);

                //fileName = ConfigurationManager.AppSettings["FullMXDFilePath"];
                //LoadMXD(fileName);

                fileName = ConfigurationManager.AppSettings["FullMXDFilePath"];
                LoadMXD(fileName);
                IFeatureClass featureclass = myParcelLayer.FeatureClass;
                ixPrintkey = featureclass.FindField("Printkey");
                ixSTNAME = featureclass.FindField("STNAME");
                ixSTNUM = featureclass.FindField("STNUM");
                ixParcelAdd = featureclass.FindField("ParcelAdd");
                ixOBJECTID = featureclass.FindField("OBJECTID");
                ixONAME = featureclass.FindField("ONAME1");
                ixSBL = featureclass.FindField("SBL");
                ixOWNERADD = featureclass.FindField("OWNERADD");
                //m_mapControl.Extent = m_mapControl.FullExtent;

                m_contextMenu.SetHook(m_toolbarControl.Buddy);
                IToolbarMenu2 toolbarMenu = m_contextMenu.ContextMenu;
                toolbarMenu.CommandPool = m_toolbarControl.CommandPool;
                EditHelper.TheMainForm = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void LoadMXD(string filename)
        {
            if (!m_mapControl.CheckMxFile(filename))
            {
                MessageBox.Show("Please check the mxd! No ParcelSearch.mxd found in specific folder!");
                Application.Exit();
            }

            m_mapControl.LoadMxFile(filename);

            myParcelLayer = BaseFun.ReturnParcelLayerIfExists(m_mapControl);
            /////////!!!///////myTempLayer = BaseFun.ReturnTempLayerIfExists(m_mapControl);
            if (myParcelLayer == null)
            {
                MessageBox.Show("No Parcel Layer Found!");
                Application.Exit();
            }

           

            ////////!!!//////if (myTempLayer == null)
            //////////////{
            //////////////    MessageBox.Show("No Temp Layer Found!");
            //////////////    Application.Exit();
            //////////////}

            //Set up Synchronize between map view and layout view
            IMapDocument mapDoc = new MapDocumentClass();

            if (mapDoc.get_IsPresent(filename) && !mapDoc.get_IsPasswordProtected(filename))
            {
                mapDoc.Open(filename, string.Empty);

                // set the first map as the active view
                IMap map = mapDoc.get_Map(0);
                mapDoc.SetActiveView((IActiveView)map);

                m_controlsSynchronizer.PageLayoutControl.PageLayout = mapDoc.PageLayout;
                m_controlsSynchronizer.ReplaceMap(map);

                mapDoc.Close();
            }
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                m_controlsSynchronizer.ActivateMap();
            }
            else
            {
                m_controlsSynchronizer.ActivatePageLayout();
            }
        }

        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));

            if ((isActivated_SelectByLine) || (isActivated_SelectParcelByLine) || (isActivated_RestrictedStreet))
            {
                if (m_lineFeedback != null)
                {
                    if (!m_isMouseDown) return;

                    int X = e.x;
                    int Y = e.y;

                    IActiveView activeView = m_mapControl.ActiveView as IActiveView;

                    IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y) as IPoint;
                    m_lineFeedback.MoveTo(point);
                }

            }
        }

        private void parcelNotificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (fileName != ConfigurationManager.AppSettings["FullMXDFilePath"])
            //{
            //    fileName = ConfigurationManager.AppSettings["FullMXDFilePath"];
            //    LoadMXD(fileName);
            //}
            if (EditHelper.TheSearchWindow != null)
            {
                EditHelper.TheSearchWindow.Close();
                EditHelper.TheSearchWindow = null;
            }
            tabControl1.SelectedIndex = 0;
            SearchWindow newFrm = new SearchWindow();
            newFrm.Show();
            newFrm.TopMost = true;

            newFrm.ExportToPDF += new SearchWindow.ExportToPDFEventHandler(ExportToPDF);
            newFrm.ActiveMapLayoutView += new SearchWindow.ActiveMapLayoutViewEventHandler(ActiveLayOutView);

            EditHelper.TheSearchWindow = newFrm;
        }

        private void trafficRestrictionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (fileName != ConfigurationManager.AppSettings["FullMXDFilePath_TR"])
            //{
            //    fileName = ConfigurationManager.AppSettings["FullMXDFilePath_TR"];
            //    LoadMXD(fileName);
            //}
            if (EditHelper.TheTRWindow != null)
            {
                EditHelper.TheTRWindow.Close();
                EditHelper.TheTRWindow = null;
            }
            tabControl1.SelectedIndex = 0;
            TRWindow newFrm = new TRWindow();
            newFrm.Show();
            newFrm.TopMost = true;

            newFrm.ExportToPDF += new TRWindow.ExportToPDFEventHandler(ExportToPDF);
            newFrm.ActiveMapLayoutView += new TRWindow.ActiveMapLayoutViewEventHandler(ActiveLayOutView);

            EditHelper.TheTRWindow = newFrm;

        }

        private void selectByLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switchSelectTools("isActivated_SelectByLine");
            m_mapControl.CurrentTool = null;

        }



        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            //If the right button clcik then popup the contextmenu
            if (e.button == 2)
            {
                m_contextMenu.PopupMenu(e.x, e.y, m_mapControl.hWnd);
            }

            //If other tools activated, then not run the selectbyline tool
            if ((e.button == 1) && (m_mapControl.CurrentTool == null))
            {
                //Run the select by line tool
                if (isActivated_SelectByLine || isActivated_SelectParcelByLine || isActivated_RestrictedStreet)
                {
                    if (e.button != 1)
                    {
                        return;
                    }
                    else
                    {

                        int X = e.x;
                        int Y = e.y;

                        IActiveView activeView = m_mapControl.ActiveView as IActiveView;
                        IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y) as IPoint;

                        if (m_lineFeedback == null)
                        {
                            m_lineFeedback = new ESRI.ArcGIS.Display.NewLineFeedback();
                            m_lineFeedback.Display = activeView.ScreenDisplay;
                            m_lineFeedback.Start(point);
                        }
                        else
                        {
                            m_lineFeedback.AddPoint(point);
                        }

                        m_isMouseDown = true;
                        return;
                    }
                }

                //Run the map click select if the checkbox is selected
                else if (isActivated_MapClickParcelSelect)
                {
                    if (e.button != 1)
                    {
                        return;
                    }
                    else
                    {

                        int X = e.x;
                        int Y = e.y;

                        IActiveView activeView = m_mapControl.ActiveView as IActiveView;
                        IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y) as IPoint;
                        try
                        {
                            IGeometry myGeom = point;
                            //Set the spaital reference
                            ISpatialReference SpatialRef = m_mapControl.SpatialReference;
                            myGeom.SpatialReference = SpatialRef;

                            //Set the spaitalFilter
                            ISpatialFilter mySpaitalFilter = new SpatialFilterClass();
                            mySpaitalFilter.Geometry = myGeom;
                            mySpaitalFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                            //Indicate if the shift button is pressed
                            IFeatureClass tempFC = Helper.FindMyFeatureLayer(m_mapControl.ActiveView.FocusMap, "Parcels").FeatureClass;



                            IFeatureCursor myFeatureCursor = tempFC.Search(mySpaitalFilter, true);

                            IFeature myFea = myFeatureCursor.NextFeature();

                            if (myFea != null)
                            {
                                //Add the selected feature to the oidList 

                                EditHelper.TheSearchWindow.addParcel_WithMapSelect(myFea);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }

                    }
                }
            }

        }

        public static void switchSelectTools(string strTool)
        {

            switch (strTool)
            {
                case "isActivated_SelectParcelByLine":
                    isActivated_SelectParcelByLine = true;
                    isActivated_SelectByLine = false;
                    isActivated_RestrictedStreet = false;
                    return;
                case "isActivated_SelectByLine":
                    isActivated_SelectParcelByLine = false;
                    isActivated_SelectByLine = true;
                    isActivated_RestrictedStreet = false;
                    return;
                case "isActivated_RestrictedStreet":
                    isActivated_SelectParcelByLine = false;
                    isActivated_SelectByLine = false;
                    isActivated_RestrictedStreet = true;
                    return;
                default:
                    return;
            }
        }

        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            if (m_mapControl.CurrentTool != null)
            {
                return;
            }
            else
            {

                if ((isActivated_SelectByLine) || (isActivated_SelectParcelByLine) || isActivated_RestrictedStreet)
                {
                    IActiveView activeView = m_mapControl.ActiveView as IActiveView;

                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

                    IPolyline polyline;

                    if (m_lineFeedback != null)
                    {
                        polyline = m_lineFeedback.Stop();
                        if (polyline != null)
                        {
                            try
                            {
                                IGeometry myGeom = polyline;
                                //Set the spaital reference
                                ISpatialReference SpatialRef = m_mapControl.SpatialReference;
                                myGeom.SpatialReference = SpatialRef;

                                //Set the spaitalFilter
                                ISpatialFilter mySpaitalFilter = new SpatialFilterClass();
                                mySpaitalFilter.Geometry = myGeom;
                                mySpaitalFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                                //Indicate if the shift button is pressed
                                myFeatselect = myParcelLayer as IFeatureSelection;

                                if (e.shift == 0)
                                {
                                    myFeatselect.SelectFeatures(mySpaitalFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                                }
                                else
                                {
                                    myFeatselect.SelectFeatures(mySpaitalFilter, esriSelectionResultEnum.esriSelectionResultAdd, false);
                                }

                                //!!//can change selected symbol with SelectionSymbol and SetSelectionSymbol
                                mySelSet = myFeatselect.SelectionSet;
                                //m_mapControl.ActiveView.FocusMap.SelectByShape(polyline, null, false);

                                int ownerName_index = myParcelLayer.FeatureClass.Fields.FindField("ONAME1");
                                int oid_index = myParcelLayer.FeatureClass.Fields.FindField("OBJECTID");
                                ICursor myCur;
                                mySelSet.Search(null, false, out myCur);

                                IFeatureCursor myFeatureCursor = myCur as IFeatureCursor;

                                if (isActivated_SelectByLine)
                                {
                                    IFeature myFea = myFeatureCursor.NextFeature();
                                    sum_n = 0;
                                    sum_NoOwnername = 0;
                                    //Get a list of selected features

                                    while (myFea != null)
                                    {
                                        string ownerName = Convert.ToString(myFea.get_Value(ownerName_index));

                                        if (ownerName.Trim() == "")
                                        {
                                            mySelSet.RemoveList(1, myFea.OID);
                                            myFeatselect.SelectionChanged();
                                            sum_NoOwnername = sum_NoOwnername + 1;
                                        }

                                        myFea = myFeatureCursor.NextFeature();
                                        sum_n = sum_n + 1;
                                    }
                                    //Export the selected features into a table
                                    IFeatureLayerDefinition myFLD = myParcelLayer as IFeatureLayerDefinition;
                                    IFeatureLayer myNewFL = myFLD.CreateSelectionLayer("Temp", true, null, null);
                                    //Set the new featurelayer to mytable
                                    myTable = myNewFL as ITable;
                                    if (myTable != null)
                                    {
                                        myDataSet = myTable as IDataset;
                                        tableWrapper = new TableWrapper(myTable);
                                    }
                                    if (oldSelectedForm != null)
                                    {
                                        oldSelectedForm.Close();
                                    }

                                    SelectedFeaturesForm newFrm = new SelectedFeaturesForm();
                                    newFrm.Show();
                                    newFrm.TopMost = true;

                                    oldSelectedForm = newFrm;
                                }
                                else if (isActivated_SelectParcelByLine)
                                {

                                    EditHelper.TheSearchWindow.addParcel_WithLSelect(myFeatureCursor);

                                    isActivated_SelectParcelByLine = false;

                                }
                                else if (isActivated_RestrictedStreet)
                                {
                                    EditHelper.TheTRWindow.TrafficRestriction(polyline);
                                    isActivated_RestrictedStreet = false;
                                    m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return;
                            }
                        }

                        //Active the selectedfeature window

                    }
                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                }
                if (e.shift == 0)//0 is false
                {
                    ////m_isMouseDown = false;
                    isActivated_SelectByLine = false;
                }
                m_lineFeedback = null;
            }
        }

        private void ExportToPDF(SearchWindow searchWindow)
        {
            //currentSearchWindow = searchWindow;
            //save file to a directory 
            saveFileDialog1.Filter = "(*.pdf)|*.pdf|(*.jpeg)|*.jpeg|(*.tif)|*.tif|(*.png)|*.png";
            saveFileDialog1.Title = "Export to file";
            saveFileDialog1.InitialDirectory = ConfigurationManager.AppSettings["InitialDirectory"];
            saveFileDialog1.RestoreDirectory = false;
            saveFileDialog1.FileName = PreviousParceladd;
            saveFileDialog1.FilterIndex = 1;

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                tabControl1.SelectedIndex = 0;
                searchWindow.Show();
                searchWindow.TopMost = true;
                return;
            }
        }

        private void ExportToPDF(TRWindow trWindow)
        {

            //save file to a directory 
            saveFileDialog2.Filter = "(*.pdf)|*.pdf|(*.jpeg)|*.jpeg|(*.tif)|*.tif|(*.png)|*.png";
            saveFileDialog2.Title = "Export to file";
            saveFileDialog2.InitialDirectory = ConfigurationManager.AppSettings["InitialDirectory"];
            saveFileDialog2.RestoreDirectory = false;
            saveFileDialog2.FileName = PreviousParceladd.Replace("\\n", " ");
            saveFileDialog2.FilterIndex = 1;

            if (saveFileDialog2.ShowDialog() == DialogResult.Cancel)
            {
                tabControl1.SelectedIndex = 0;
                trWindow.Show();
                trWindow.TopMost = true;
                return;
            }
        }

        private void ActiveLayOutView()
        {
            tabControl1.SelectedIndex = 1;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerHourglass;
            IActiveView myActive = m_pageLayoutControl.ActiveView as IActiveView;

            short Dpi = 0;

            IExport pExport = null;
            string ExportType = null;
            short iScreenResolution = (short)myActive.ScreenDisplay.DisplayTransformation.Resolution;

            if (saveFileDialog1.FileName != string.Empty)
            {

                if (1 == saveFileDialog1.FilterIndex)
                {
                    // EXPORT to PDF
                    Dpi = 200;
                    pExport = new ExportPDFClass();
                    IExportPDF pExport2;
                    pExport2 = new ExportPDFClass();
                    pExport2.EmbedFonts = true;
                    pExport = (IExport)pExport2;
                    pExport.ExportFileName = saveFileDialog1.FileName;
                    pExport.Resolution = Dpi;
                    ExportType = "PDF";

                }
                else if (2 == saveFileDialog1.FilterIndex)
                {
                    //EXPORT TO JPEG
                    Dpi = 200;
                    pExport = new ExportJPEGClass();
                    pExport.ExportFileName = saveFileDialog1.FileName;
                    pExport.Resolution = Dpi;
                    ExportType = "JPEG";

                }

                tagRECT exportRECT;
                tagRECT DisplayBounds;

                DisplayBounds = myActive.ExportFrame;
                IEnvelope docGraphicsExtentEnv = BaseFun.GetGraphicsExtent(myActive);
                IPageLayout myPagelayout = myActive as IPageLayout;

                IUnitConverter pUnitConvertor = new UnitConverter();
                IEnvelope PixelBoundsEnv = new EnvelopeClass();

                //assign the x and y values representing the clipped area to the PixelBounds envelope
                PixelBoundsEnv.XMin = 0;
                PixelBoundsEnv.YMin = 0;
                PixelBoundsEnv.XMax = 8.5 * 200;
                PixelBoundsEnv.YMin = 11 * 200;


                //'assign the x and y values representing the clipped export extent to the exportRECT
                exportRECT.bottom = (int)(pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.YMax, myPagelayout.Page.Units, esriUnits.esriInches) * pExport.Resolution - pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.YMin, myPagelayout.Page.Units, esriUnits.esriInches) * pExport.Resolution);
                exportRECT.left = (int)(PixelBoundsEnv.XMin) + 50;
                exportRECT.top = (int)(PixelBoundsEnv.YMin) + 50;
                exportRECT.right = (int)(pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.XMax, myPagelayout.Page.Units, esriUnits.esriInches) * pExport.Resolution - pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.XMin, myPagelayout.Page.Units, esriUnits.esriInches) * pExport.Resolution);

                //since we're clipping to graphics extent, set the visible bounds.
                IEnvelope docMapExtEnv = docGraphicsExtentEnv;

                pExport.PixelBounds = PixelBoundsEnv;

                int hdc = pExport.StartExporting();
                myActive.Output((int)hdc, (int)pExport.Resolution, ref exportRECT, docMapExtEnv, null);

                pExport.FinishExporting();

                pExport.Cleanup();
                //ExportActiveViewParameterized(300,1,ExportType,@"C:\GIS_TOA\temp\",false);
                //Show the success
                MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                MessageBox.Show("Your ." + ExportType + " was exported successfully!");
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);

                //if ((LogTxt != "OBJECTID,PRINTKEY,STNUM,STNAME,PARCELADD,OWNERADD,OWNERNAME") && (LogTxt.Trim() != ""))
                if (LogTxt.Trim() != "")
                    CreateLogFileForNoOwner(System.IO.Path.GetFileNameWithoutExtension(saveFileDialog1.FileName), LogTxt);
                if (EditHelper.TheSearchWindow != null)
                {
                    EditHelper.TheSearchWindow.Show();
                    EditHelper.TheSearchWindow.TopMost = true;
                }

            }

        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerHourglass;
            IActiveView myActive = m_pageLayoutControl.ActiveView as IActiveView;

            short Dpi = 0;

            IExport pExport = null;
            string ExportType = null;
            short iScreenResolution = (short)myActive.ScreenDisplay.DisplayTransformation.Resolution;

            if (saveFileDialog2.FileName != string.Empty)
            {

                if (1 == saveFileDialog2.FilterIndex)
                {
                    // EXPORT to PDF
                    Dpi = 200;
                    pExport = new ExportPDFClass();
                    IExportPDF pExport2;
                    pExport2 = new ExportPDFClass();
                    pExport2.EmbedFonts = true;
                    pExport = (IExport)pExport2;
                    pExport.ExportFileName = saveFileDialog2.FileName;
                    pExport.Resolution = Dpi;
                    ExportType = "PDF";

                }
                else if (2 == saveFileDialog2.FilterIndex)
                {
                    //EXPORT TO JPEG
                    Dpi = 200;
                    pExport = new ExportJPEGClass();
                    pExport.ExportFileName = saveFileDialog2.FileName;
                    pExport.Resolution = Dpi;
                    ExportType = "JPEG";

                }

                tagRECT exportRECT;
                tagRECT DisplayBounds;

                DisplayBounds = myActive.ExportFrame;
                IEnvelope docGraphicsExtentEnv = BaseFun.GetGraphicsExtent(myActive);
                IPageLayout myPagelayout = myActive as IPageLayout;

                IUnitConverter pUnitConvertor = new UnitConverter();
                IEnvelope PixelBoundsEnv = new EnvelopeClass();

                //assign the x and y values representing the clipped area to the PixelBounds envelope
                PixelBoundsEnv.XMin = 0;
                PixelBoundsEnv.YMin = 0;
                PixelBoundsEnv.XMax = 8.5 * 200;
                PixelBoundsEnv.YMin = 11 * 200;


                //'assign the x and y values representing the clipped export extent to the exportRECT
                exportRECT.bottom = (int)(pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.YMax, myPagelayout.Page.Units, esriUnits.esriInches) * pExport.Resolution - pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.YMin, myPagelayout.Page.Units, esriUnits.esriInches) * pExport.Resolution);
                exportRECT.left = (int)(PixelBoundsEnv.XMin) + 50;
                exportRECT.top = (int)(PixelBoundsEnv.YMin) + 50;
                exportRECT.right = (int)(pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.XMax, myPagelayout.Page.Units, esriUnits.esriInches) * pExport.Resolution - pUnitConvertor.ConvertUnits(docGraphicsExtentEnv.XMin, myPagelayout.Page.Units, esriUnits.esriInches) * pExport.Resolution);

                //since we're clipping to graphics extent, set the visible bounds.
                IEnvelope docMapExtEnv = docGraphicsExtentEnv;

                pExport.PixelBounds = PixelBoundsEnv;

                int hdc = pExport.StartExporting();
                myActive.Output((int)hdc, (int)pExport.Resolution, ref exportRECT, docMapExtEnv, null);

                pExport.FinishExporting();

                pExport.Cleanup();
                //ExportActiveViewParameterized(300,1,ExportType,@"C:\GIS_TOA\temp\",false);
                //Show the success
                MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                MessageBox.Show("Your ." + ExportType + " was exported successfully!");
                System.Diagnostics.Process.Start(saveFileDialog2.FileName);
                tabControl1.SelectedIndex = 0;
           
                //if ((LogTxt != "OBJECTID,PRINTKEY,STNUM,STNAME,PARCELADD,OWNERADD,OWNERNAME")&&(LogTxt.Trim()!=""))
                if (LogTxt.Trim() != "")
                   CreateLogFileForNoOwner(System.IO.Path.GetFileNameWithoutExtension(saveFileDialog2.FileName), LogTxt);
                if (EditHelper.TheTRWindow != null)
                {
                    EditHelper.TheTRWindow.Show();
                    EditHelper.TheTRWindow.TopMost = true;
                }
            }
        }

        #region setup Font Smoothing for EXPORT
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);

        /* constants used for user32 calls */
        const uint SPI_GETFONTSMOOTHING = 74;
        const uint SPI_SETFONTSMOOTHING = 75;
        const uint SPIF_UPDATEINIFILE = 0x1;
        private void DisableFontSmoothing()
        {
            bool iResult;
            int pv = 0;

            /* call to systemparametersinfo to set the font smoothing value */
            iResult = SystemParametersInfo(SPI_SETFONTSMOOTHING, 0, ref pv, SPIF_UPDATEINIFILE);
        }
        private void EnableFontSmoothing()
        {
            bool iResult;
            int pv = 0;

            /* call to systemparametersinfo to set the font smoothing value */
            iResult = SystemParametersInfo(SPI_SETFONTSMOOTHING, 1, ref pv, SPIF_UPDATEINIFILE);
        }
        private Boolean GetFontSmoothing()
        {
            bool iResult;
            int pv = 0;

            /* call to systemparametersinfo to get the font smoothing value */
            iResult = SystemParametersInfo(SPI_GETFONTSMOOTHING, 0, ref pv, 0);

            if (pv > 0)
            {
                //pv > 0 means font smoothing is ON.
                return true;
            }
            else
            {
                //pv == 0 means font smoothing is OFF.
                return false;
            }
        }
        #endregion  setup Font Smoothing for EXPORT
        private void ExportActiveViewParameterized(long iOutputResolution, long lResampleRatio, string ExportType, string sOutputDir, Boolean bClipToGraphicsExtent)
        {

            /* EXPORT PARAMETER: (iOutputResolution) the resolution requested.
             * EXPORT PARAMETER: (lResampleRatio) Output Image Quality of the export.  The value here will only be used if the export
             * object is a format that allows setting of Output Image Quality, i.e. a vector exporter.
             * The value assigned to ResampleRatio should be in the range 1 to 5.
             * 1 corresponds to "Best", 5 corresponds to "Fast"
             * EXPORT PARAMETER: (ExportType) a string which contains the export type to create.
             * EXPORT PARAMETER: (sOutputDir) a string which contains the directory to output to.
             * EXPORT PARAMETER: (bClipToGraphicsExtent) Assign True or False to determine if export image will be clipped to the graphic 
             * extent of layout elements.  This value is ignored for data view exports
             */

            /* Exports the Active View of the document to selected output format. */

            // using predefined static member
            IActiveView docActiveView = this.axPageLayoutControl1.ActiveView;
            IExport docExport;
            IPrintAndExport docPrintExport;
            IOutputRasterSettings RasterSettings;
            string sNameRoot;
            bool bReenable = false;

            if (GetFontSmoothing())
            {
                /* font smoothing is on, disable it and set the flag to reenable it later. */
                bReenable = true;
                DisableFontSmoothing();
                if (GetFontSmoothing())
                {
                    //font smoothing is NOT successfully disabled, error out.
                    return;
                }
                //else font smoothing was successfully disabled.
            }

            // The Export*Class() type initializes a new export class of the desired type.
            if (ExportType == "PDF")
            {
                docExport = new ExportPDFClass();
            }
            else if (ExportType == "EPS")
            {
                docExport = new ExportPSClass();
            }
            else if (ExportType == "AI")
            {
                docExport = new ExportAIClass();
            }
            else if (ExportType == "BMP")
            {

                docExport = new ExportBMPClass();
            }
            else if (ExportType == "TIFF")
            {
                docExport = new ExportTIFFClass();
            }
            else if (ExportType == "SVG")
            {
                docExport = new ExportSVGClass();
            }
            else if (ExportType == "PNG")
            {
                docExport = new ExportPNGClass();
            }
            else if (ExportType == "GIF")
            {
                docExport = new ExportGIFClass();
            }
            else if (ExportType == "EMF")
            {
                docExport = new ExportEMFClass();
            }
            else if (ExportType == "JPEG")
            {
                docExport = new ExportJPEGClass();
            }
            else
            {
                MessageBox.Show("Unsupported export type " + ExportType + ", defaulting to EMF.");
                ExportType = "EMF";
                docExport = new ExportEMFClass();
            }

            docPrintExport = new PrintAndExportClass();

            //set the name root for the export
            sNameRoot = "ExportActiveViewSampleOutput";

            //set the export filename (which is the nameroot + the appropriate file extension)
            docExport.ExportFileName = sOutputDir + sNameRoot + "." + docExport.Filter.Split('.')[1].Split('|')[0].Split(')')[0];

            //Output Image Quality of the export.  The value here will only be used if the export
            // object is a format that allows setting of Output Image Quality, i.e. a vector exporter.
            // The value assigned to ResampleRatio should be in the range 1 to 5.
            // 1 corresponds to "Best", 5 corresponds to "Fast"

            // check if export is vector or raster
            if (docExport is IOutputRasterSettings)
            {
                // for vector formats, assign the desired ResampleRatio to control drawing of raster layers at export time   
                RasterSettings = (IOutputRasterSettings)docExport;
                RasterSettings.ResampleRatio = (int)lResampleRatio;

                // NOTE: for raster formats output quality of the DISPLAY is set to 1 for image export 
                // formats by default which is what should be used
            }

            docPrintExport.Export(docActiveView, docExport, iOutputResolution, bClipToGraphicsExtent, null);

            MessageBox.Show("Finished exporting " + sOutputDir + sNameRoot + "." + docExport.Filter.Split('.')[1].Split('|')[0].Split(')')[0] + ".", "Export Active View Sample");

            if (bReenable)
            {
                /* reenable font smoothing if we disabled it before */
                EnableFontSmoothing();
                bReenable = false;
                if (!GetFontSmoothing())
                {
                    //error: cannot reenable font smoothing.
                    MessageBox.Show("Unable to reenable Font Smoothing", "Font Smoothing error");
                }
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //IRgbColor pColor = new RgbColorClass();
            //pColor.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.OrangeRed);
            //ISimpleFillSymbol sfs = new SimpleFillSymbolClass();
            //sfs.Outline.Width = 0;
            //sfs.Style = esriSimpleFillStyle.esriSFSSolid;
            //sfs.Color = pColor;

            //ISimpleRenderer render = new SimpleRendererClass();
            //render.Symbol = sfs as ISymbol;

            //IMap eMap = m_mapControl.ActiveView.FocusMap;
            //IFeatureLayer eFeatureLayer = Helper.FindMyFeatureLayer(eMap, "Parcels");
            //IFeatureLayerDefinition eFeaturelayerDef = eFeatureLayer as IFeatureLayerDefinition;
            //IGeoFeatureLayer eNewFeatureLayer = eFeaturelayerDef.CreateSelectionLayer("NewLayer", true, "", "") as IGeoFeatureLayer;

            //eNewFeatureLayer.Renderer = render as IFeatureRenderer;
            //ILayerEffects LF = eNewFeatureLayer as ILayerEffects;
            //LF.Transparency = 70;
            //m_mapControl.ActiveView.FocusMap.AddLayer(eNewFeatureLayer);
            //m_mapControl.ActiveView.FocusMap.MoveLayer(eNewFeatureLayer, -1);
            //m_mapControl.Refresh();

        }
        public static void zoomToGeometry(IGeometry geometry)
        {
            IEnvelope envelope = new EnvelopeClass();
            envelope = geometry.Envelope;
            envelope.Expand(2, 2, true);
            m_mapControl.ActiveView.Extent = envelope;
            m_mapControl.ActiveView.Refresh();
            //myActive.ScreenDisplay.UpdateWindow();
        }
        private void Flash(IGeometry geometry)
        {

            ISimpleFillSymbol iFillSymbol;
            ISymbol iSymbol;
            IRgbColor iRgbColor;
            iFillSymbol = new SimpleFillSymbol();
            iFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            iFillSymbol.Outline.Width = 12;

            iRgbColor = new RgbColor();
            iRgbColor.RGB = System.Drawing.Color.FromArgb(180, 180, 180).ToArgb();
            iFillSymbol.Color = iRgbColor;

            iSymbol = (ISymbol)iFillSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotCopyPen;
            IScreenDisplay iScreenDisplay = m_mapControl.ActiveView.ScreenDisplay;
            m_mapControl.FlashShape(geometry, 3, 200, iSymbol);
        }

        private void parcelNotificationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void trafficRestrictionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        public void CreateLogFileForNoOwner(string title, string log)
        {
            string initialfolder = ConfigurationManager.AppSettings["InitialDirectory"];
            string logfolder = initialfolder + @"\" + "logfile";
            string date = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
            string newfiledirectory = logfolder + @"\" + date;
            DirectoryInfo dir = new DirectoryInfo(newfiledirectory);
            if (dir.Exists == false)
            {
                Directory.CreateDirectory(newfiledirectory);
            }

            string newfilepath = newfiledirectory + @"\" + title + ".txt";
            FileInfo f = new FileInfo(newfilepath);
            if (f.Exists)
            {
                string time = String.Format("{0:hh_mm_ss}", DateTime.Now);
                title = title + "_" + time;
                newfilepath = newfiledirectory + @"\" + title + ".txt";
            }
            File.Delete(newfilepath);
            if (!File.Exists(newfilepath))
            {
                File.Create(newfilepath).Dispose();
                using (TextWriter tw = new StreamWriter(newfilepath))
                {
                    tw.Write(log);
                    tw.Close();
                }
            }

        }

    }
}
