using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ArcDataBinding;

namespace AmherstParcelInfo
{

    public partial class TRWindow : Form
    {
        #region Declare private variables

        private IFeatureLayer myLayer;
        //private IFeatureLayer tempLayer;
        private IGraphicsContainer myGC;
        private IActiveView myActive;
        private IFeatureClass myFC;
        private int ixOBJECTID, ixPrintkey, ixSTNUM, ixSTNAME, ixParcelAdd, ixONAME, ixSBL, ixOWNERADD;
        private BindingSource binding = new BindingSource();
        private IEnvelope envLayout;

        private IPolyline PL;
        private Dictionary<string, Color> dicRestrictionType = new Dictionary<string, Color>();
        //public static List<int> MyOIDlist = new List<int>();
        ////////private List<parcel> selectedParcellist = new List<parcel>();
        //string LogTxt;
        #endregion Declare private variables

        #region Declare public variables

        public delegate void ExportToPDFEventHandler(TRWindow searchWindow);
        public event ExportToPDFEventHandler ExportToPDF;
        public delegate void ActiveMapLayoutViewEventHandler();
        public event ActiveMapLayoutViewEventHandler ActiveMapLayoutView;

        public ISelectionSet myCurrentSelectionSet;
        public ISpatialFilter myExportToDBFSpatialFilter;
        public string LogTxt = "";
        public string Description = "";
        public string mapTitle = "";
        public string RType_L, RType_R;
        public Color RColor_L, RColor_R;



        #endregion Declare Public varibales

        #region Declare properties

        #endregion Declare properties

        public TRWindow()
        {
            InitializeComponent();

            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance100);
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance200);
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance400);
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance600);
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance800);
            this.cmbBufferDistance.SelectedIndex = 3;

            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScaleAuto);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale100);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale200);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale400);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale800);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale1000);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale1200);
            this.cmbLayoutScale.SelectedIndex = 1;

            dicRestrictionType.Add("Restriction Area", Color.Red);
            dicRestrictionType.Add("Two Hour Parking Zone", Color.Yellow);
            dicRestrictionType.Add("Restriction Removal", Color.Green);
            dicRestrictionType.Add("Null", Color.White);

            var keys = dicRestrictionType.Keys.ToArray();
            foreach (var key in keys)
            {
                cmbLeft.Items.Add(key.ToString());
                cmbRight.Items.Add(key.ToString());
            }
            cmbLeft.SelectedIndex = 0;
            cmbRight.SelectedIndex = 1;
            cmbLanes.SelectedIndex = 1;


            myActive = MainForm.m_mapControl.ActiveView.FocusMap as IActiveView;
            myGC = MainForm.m_mapControl.ActiveView.FocusMap as IGraphicsContainer;

            myLayer = MainForm.myParcelLayer;
            //////////!!!////tempLayer = MainForm.myTempLayer;
            myFC = myLayer.FeatureClass;

        }

        private void TRWindow_Load(object sender, EventArgs e)
        {

            IFeatureClass featureclass = myLayer.FeatureClass;
            ixPrintkey = featureclass.FindField("Printkey");
            ixSTNAME = featureclass.FindField("STNAME");
            ixSTNUM = featureclass.FindField("STNUM");
            ixParcelAdd = featureclass.FindField("ParcelAdd");
            ixOBJECTID = featureclass.FindField("OBJECTID");
            ixONAME = featureclass.FindField("ONAME1");
            ixSBL = featureclass.FindField("SBL");
            ixOWNERADD = featureclass.FindField("OWNERADD");

            dataGridView1.ReadOnly = true;
            //dataGridView1.SortedColumn = dataGridView1.Columns["STNUM"];
            cmbSearchBy.SelectedIndex = 0;

            MainForm.m_mapControl.CurrentTool = null;
            MainForm.m_pageLayoutControl.CurrentTool = null;
            EditHelper.TheTRWindow = this;
            EditHelper.TheSearchWindow = null;
        }

        private void TRWindow_MouseClick(object sender, MouseEventArgs e)
        {
            MainForm.m_mapControl.CurrentTool = null;
            MainForm.m_pageLayoutControl.CurrentTool = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Clean redundant spaces
            string[] searchtestlist = txtParcel.Text.Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (searchtestlist.Count() < txtParcel.Text.Trim().Split(new string[] { " " }, StringSplitOptions.None).Count())
            {
                txtParcel.Text = String.Join(" ", searchtestlist);
            }
            string myString = txtParcel.Text;
            string myStringUpper = myString.ToUpper();
            string myField;

            // if myString is empty or not 
            if (string.IsNullOrEmpty(myString))
            {
                MessageBox.Show("please enter the keyword for seaching!");
                return;
            }
            else
            {
                #region Search by Parceladd or SBL or Printkey

                if (cmbSearchBy.Text == "Parcel Address")
                {
                    myField = "ParcelAdd LIKE '%";
                }
                else if (cmbSearchBy.Text == "Owner Name")
                {
                    myField = "ONAME1 LIKE '%";
                    string[] nameexchange = myStringUpper.Split(' ');
                    string lastfirstname = "";
                    if (nameexchange.Count() == 2)
                    {
                        lastfirstname = nameexchange[1] + " " + nameexchange[0];
                        myField = myField + lastfirstname + "%' OR ONAME1 LIKE '%";
                    }
                    //string[] names = nameexchange.Trim().Replace(" ")


                }
                else if (cmbSearchBy.Text == "Printkey")
                {
                    myField = "Printkey LIKE '%";
                }
                else
                {
                    myField = "ParcelAdd LIKE '%";
                }

                string myQuery = myField + myStringUpper + "%'";
                //// get the layers feature class and set up a query filter
                //IFeatureClass pFC = myLayer.FeatureClass;

                //IQueryFilter pQF = new QueryFilterClass();
                //pQF.WhereClause = myQuery;

                //// search for the name and put reslut in a cursor
                //IFeatureCursor pFcur = pFC.Search(pQF, true);

                // create a castable to set up a temporary definition query for this layer
                IFeatureLayerDefinition pFLD = (IFeatureLayerDefinition)myLayer;
                pFLD.DefinitionExpression = myQuery;

                // Use the ESRI tablewapper class to pass it the featurelayer
                TableWrapper myWrapper = new TableWrapper((ITable)pFLD);

                // assign the data source converted by tablewrapper to the datagridview and bindingsource
                dataGridView1.DataSource = myWrapper;
                bindingSource1.DataSource = myWrapper;

                // Read the visiable columns from config file
                string configColumnsName = ConfigurationManager.AppSettings["ParcelLayerVisableColumnNames"].ToString().ToLower();
                if (!string.IsNullOrEmpty(configColumnsName))
                {
                    List<string> visiableColumnsNameList = configColumnsName.Split(',').Select(p => p.Trim()).ToList();
                    //Disable some columns
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (visiableColumnsNameList.IndexOf(column.Name.Trim().ToLower()) < 0)
                        {
                            dataGridView1.Columns[column.Index].Visible = false;
                        }
                    }
                }

                dataGridView1.Refresh();
                // clear the query
                pFLD.DefinitionExpression = "";
                #endregion Search by Parceladd or sbl or printkey
            }
        }


        private void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                #region Reset the label and the datagridview
                txtParcel.ResetText();

                dataGridView1.Rows.Clear();
                //////dataGridView2.Rows.Clear();

                //////lblAdjacentParcelNum.ResetText();
                //////lblNoOwnerNameParcelNum.ResetText();
                //////lbl_info.ResetText();


                //MyOIDlist.Clear();
                //////selectedParcellist.Clear();
                //////binding.ResetBindings(false);
                //////listView1.Items.Clear();

                //////if (myGC != null)
                //////{
                //////    myGC.DeleteAllElements();
                //////}
                MainForm.m_mapControl.ActiveView.Refresh();
                //if (myLayer.AreaOfInterest != null)
                //{
                //    myActive = MainForm.m_mapControl.ActiveView.FocusMap as IActiveView;
                //    myActive.Extent = myLayer.AreaOfInterest;
                //    myActive.Refresh();
                //}
                #endregion Reset the label and the datagridview
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //Find the Parcel 
                int Myselect = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                int[] oidListTemp = new int[1];
                oidListTemp[0] = Myselect;
                IFeatureCursor ListCursor = myLayer.FeatureClass.GetFeatures(oidListTemp, false);
                IPolygon polygon = ListCursor.NextFeature().ShapeCopy as IPolygon;
                //Zoom to the parcel

                IEnvelope env = new EnvelopeClass();
                env = polygon.Envelope;
                env.Expand(5, 5, true);
                MainForm.zoomToGeometry(env);

                ISimpleFillSymbol sfs = new SimpleFillSymbolClass();
                sfs = BaseFun.GenerateSFSByColor(System.Drawing.Color.LemonChiffon,2);
                IFillShapeElement ss = new PolygonElementClass();





                ss.Symbol = sfs;
                IElement eless = ss as IElement;
                eless.Geometry = polygon;
                if (myGC != null)
                {
                    myGC.DeleteAllElements();
                    myGC.AddElement(eless, 0);
                }
                myActive.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }

        private void btnRestrictedStreet_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditHelper.TheMainForm.Focus();
            MainForm.m_mapControl.CurrentTool = null;
            MainForm.m_pageLayoutControl.CurrentTool = null;
            MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            MainForm.switchSelectTools("isActivated_RestrictedStreet");
        }

        private void ckbBothSide_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbBothSide.Checked)
            {
                //RType = "Both";
                cmbRight.Enabled = false;
                cmbRight.SelectedIndex = cmbLeft.SelectedIndex;
                this.cmbLeft.SelectedIndexChanged += new System.EventHandler(this.cmbLeft_SelectedIndexChanged1);
            }
            else
            {
                //RType = "Single";
                cmbRight.Enabled = true;
                this.cmbLeft.SelectedIndexChanged -= new System.EventHandler(this.cmbLeft_SelectedIndexChanged1);
            }
        }

        private void cmbRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnColorR.BackColor = dicRestrictionType[cmbRight.SelectedItem.ToString()];
        }
        private void cmbLeft_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnColorL.BackColor = dicRestrictionType[cmbLeft.SelectedItem.ToString()];
        }
        private void cmbLeft_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cmbRight.SelectedIndex = cmbLeft.SelectedIndex;
        }
        private void cmbLeft_Leave(object sender, EventArgs e)
        {
            if ((cmbLeft.Text.ToLower() == "null") || (cmbLeft.Text.Trim() == ""))
            {
                btnColorL.BackColor = Color.White;
                cmbLeft.Text = "Null";
            }
        }
        private void cmbRight_Leave(object sender, EventArgs e)
        {
            if ((cmbRight.Text.ToLower() == "null") || (cmbRight.Text.Trim() == ""))
            {
                btnColorR.BackColor = Color.White;
                cmbRight.Text = "Null";
            }
        }

        private void btnColorL_BackColorChanged(object sender, EventArgs e)
        {
            if (cmbLeft.Text.ToLower() == "null")
            {

                btnColorL.Font = new Font(btnColorL.Font.FontFamily, 6);
                btnColorL.Text = "NULL";
            }
            else
            {
                btnColorL.Font = new Font(btnColorL.Font.FontFamily, (float)8.25);
                btnColorL.Text = "L";
            }
        }
        private void btnColorR_BackColorChanged(object sender, EventArgs e)
        {
            if (cmbRight.Text.ToLower() == "null")
            {

                btnColorR.Font = new Font(btnColorL.Font.FontFamily, 6);
                btnColorR.Text = "NULL";
            }
            else
            {
                btnColorR.Font = new Font(btnColorL.Font.FontFamily, (float)8.25);
                btnColorR.Text = "R";
            }
        }
        private void btnColorL_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnColorL.BackColor = colorDialog1.Color;
            }

        }
        private void btnColorR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnColorR.BackColor = colorDialog1.Color;
            }
        }
        private void btnMap_Click(object sender, EventArgs e)
        {
    
            if (PL != null)
            {
                RType_L = cmbLeft.Text;
                RType_R = cmbRight.Text;
                RColor_L = btnColorL.BackColor;
                RColor_R = btnColorR.BackColor;

                TR_PasteBoard pasteboard = new TR_PasteBoard();
                pasteboard.StartPosition = FormStartPosition.CenterScreen;
                pasteboard.TopMost = true;
                pasteboard.ShowDialog();
                this.TopMost = false;

                if (mapTitle.Trim() == "")
                {
                    MessageBox.Show("Export Failed! \nPlease type a title after hit export button!");
                    return;
                }
                else
                {
                    if (Description.Trim() == "")
                    {
                        MessageBox.Show("Export Failed! \nPlease type the description for the map.");
                        return;
                    }
                    if (ActiveMapLayoutView != null)
                    {
                        ActiveMapLayoutView();
                    }
                    //prepare for legend text and color;
                    //Prepare legend text for left
                    RType_L = cmbLeft.Text;
                    RType_R = cmbRight.Text;
                    RColor_L = btnColorL.BackColor;
                    RColor_R = btnColorR.BackColor;
                    DataTableToDBF.FillMapLayoutElements(mapTitle, cmbLayoutScale.SelectedItem.ToString(), cmbBufferDistance.SelectedItem.ToString());
                    if (ExportToPDF != null)
                        this.Hide();
                    ExportToPDF(this);
                }
            }
            this.Show();
        }
        private void btnTable_Click(object sender, EventArgs e)
        {
            if (PL != null)
            {
                try
                {
                    this.Hide();
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "DBF database files(*.dbf)|*.dbf|Excel 97-2003 Workbook (*.xls)|*.xls";
                    saveFileDialog.Title = "Export to Table (.dbf or .xlsx)";
                    saveFileDialog.InitialDirectory = ConfigurationManager.AppSettings["InitialDirectory"];
                    saveFileDialog.RestoreDirectory = false;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (saveFileDialog.FileName != string.Empty)
                        {
                            if (saveFileDialog.CheckPathExists == false)
                            {
                                lbl_info.Text = "No existing path!";
                                return;
                            }
                            MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                            if (saveFileDialog.FilterIndex == 1)
                            {
                                DataTableToDBF.ExportToDBF(saveFileDialog.FileName);
                            }
                            else
                            {
                                DataTableToDBF.ExportToExcel(dataGridView2, saveFileDialog.FileName);
                                lbl_info.Text = "Your .xls was exported successfully!";
                            }
                            MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                this.Show();
            }
            //try
            //{
            //    if (PL != null)
            //    {
            //        if (rdbExcel.Checked)
            //        {
            //            this.Hide();
            //            DataTableToDBF.ExportToExcel(dataGridView2);
            //            lbl_info.Text = "Your .xls was exported successfully!";
            //        }
            //        else if (rdbDBF.Checked)
            //        {

            //            //show the open file dialog
            //            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //            saveFileDialog.Filter = "(*.dbf)|*.dbf";
            //            saveFileDialog.Title = "Export to Dbase";
            //            saveFileDialog.InitialDirectory = ConfigurationManager.AppSettings["InitialDirectory"];
            //            saveFileDialog.RestoreDirectory = false;
            //            saveFileDialog.ShowDialog();

            //            if (saveFileDialog.FileName != string.Empty)
            //            {
            //                if (saveFileDialog.CheckPathExists == false)
            //                {
            //                    lbl_info.Text = "No existing path!";
            //                    return;
            //                }
            //                this.Hide();
            //                DataTableToDBF.ExportToDBF(saveFileDialog.FileName);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No parcel is selected! Please specify the Street");
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    this.Show();
            //    this.TopMost = true;
            //    return;
            //}
            //this.Show();
            //this.TopMost = true;
        }
        private void cmbBufferDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PL != null)
                TrafficRestriction(PL);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Stop();
            lblNoOwnerNameParcelNum.Visible = !lblNoOwnerNameParcelNum.Visible;
        }
        private void TRWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            EditHelper.TheTRWindow = null;
            if (myGC != null && myActive != null)
            {
                myGC.DeleteAllElements();
                myActive.Refresh();

            }
        }

        #region Public Methods
        public void TrafficRestriction(IPolyline polyline)
        {
            PL = polyline;
            if (myGC != null)
                myGC.DeleteAllElements();
            ILineElement pPLEle = new LineElementClass();
            IElement pEle_shape = pPLEle as IElement;
            pPLEle.Symbol = BaseFun.GenerateSLS(24, 255, 0, 0);
            pEle_shape.Geometry = PL as IGeometry;
            //myGC.AddElement(pEle_shape, 0);
            ITopologicalOperator topologicalOperator;
            topologicalOperator = (ITopologicalOperator)PL;

            IPolyline PLLeft = new PolylineClass();
            IPolyline PLRight = new PolylineClass();


            if (cmbLeft.Text.ToLower() != "null")
            {
                PLLeft = ConstructOffset(PL, -20);
                ITopologicalOperator topologicalOperatorL;
                topologicalOperatorL = (ITopologicalOperator)PLLeft;
                IFillShapeElement pPolygon_streetL = new PolygonElementClass();
                pPolygon_streetL.Symbol = BaseFun.GenerateSFSByColor(btnColorL.BackColor,0.3);
                IElement element_streetL = new PolygonElementClass();
                element_streetL = pPolygon_streetL as IElement;
                element_streetL.Geometry = topologicalOperatorL.Buffer(Convert.ToInt32(cmbLanes.Text) * 4);
                myGC.AddElement(element_streetL, 0);
            }

            if (cmbRight.Text.ToLower() != "null")
            {
                PLRight = ConstructOffset(PL, 20);

                ITopologicalOperator topologicalOperatorR;
                topologicalOperatorR = (ITopologicalOperator)PLRight;
                IFillShapeElement pPolygon_streetR = new PolygonElementClass();
                pPolygon_streetR.Symbol = BaseFun.GenerateSFSByColor(btnColorR.BackColor,0.3);
                IElement element_streetR = new PolygonElementClass();
                element_streetR = pPolygon_streetR as IElement;
                element_streetR.Geometry = topologicalOperatorR.Buffer(Convert.ToInt32(cmbLanes.Text) * 4);
                myGC.AddElement(element_streetR, 0);
            }

            //}

            //bufferarea 

            IRgbColor selectColor1 = new RgbColorClass();
            selectColor1.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.OrangeRed);

            ISimpleFillSymbol fillSymbol1 = new SimpleFillSymbolClass();
            ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbolClass();
            simpleLineSymbol.Width = 3;
            simpleLineSymbol.Color = selectColor1;
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDash;
            fillSymbol1.Outline = simpleLineSymbol;
            fillSymbol1.Style = esriSimpleFillStyle.esriSFSNull;


            IFillShapeElement pPolygon_buf = new PolygonElementClass();
            pPolygon_buf.Symbol = fillSymbol1;

            IElement element_Buf;
            element_Buf = new PolygonElementClass();
            element_Buf = pPolygon_buf as IElement;

            element_Buf.Geometry = topologicalOperator.Buffer(Convert.ToInt32(cmbBufferDistance.Text));
            myGC.AddElement(element_Buf, 0);
            //selectAdjacentParcelsByBuffer(element_Buf);

            //
            BaseFun.selectAdjacentParcelsByBuffer(myLayer, element_Buf, listView1, lblAdjacentParcelNum, lblNoOwnerNameParcelNum, timer1, bindingSource2, dataGridView2);
            MainForm.zoomToGeometry(element_Buf.Geometry.Envelope);
            #region get recommended scale for combo-box
            envLayout = element_Buf.Geometry.Envelope;
            double recScaleH = envLayout.Height / 9.8164 * 12;
            double recScaleW = envLayout.Width / 8.1431 * 12;
            double recScale;
            if (recScaleH >= recScaleW)
                recScale = recScaleH;
            else
                recScale = recScaleW;
            ChangeRecommendedScale(recScale);
            #endregion get recommended scale for combo-box

            #region Avoid the overlay of the annotation
            //Avoid the overlay of the annotation
            IGraphicsLayer pGraLayer = myActive.FocusMap.BasicGraphicsLayer;
            IBarrierProperties pBarrierProp = pGraLayer as IBarrierProperties;
            pBarrierProp.Weight = (int)esriBasicOverposterWeight.esriMediumWeight;
            myActive.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            #endregion Avoid the overlay of the annotation
            this.Show();
            MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }
        #endregion Public Methods

        #region Local Methods

        //private void selectAdjacentParcelsByBuffer(IElement myBufferElement)
        //{
        //    // Spatial filter
        //    IGeometry queryGeometry = myBufferElement.Geometry;
        //    ISpatialFilter spatialFilter = new SpatialFilterClass();
        //    spatialFilter.Geometry = queryGeometry;
        //    spatialFilter.GeometryField = myFC.ShapeFieldName;
        //    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
        //    //spatialFilter.SubFields = "OwnerAdd, Printkey, SBL, OBJECTID";

        //    //Execute the query and iterate through the cursor
        //    using (ComReleaser comReleaser = new ComReleaser())
        //    {
        //        IFeatureCursor ParcelCursor = myFC.Search(spatialFilter, false);
        //        comReleaser.ManageLifetime(ParcelCursor);
        //        IFeature ParcelFeature = null;
        //        ParcelFeature = ParcelCursor.NextFeature();
        //        int featureNum = 0;
        //        int emptyNum = 0;
        //        string queryTotal = ""; //query to get parcels without oname

        //        //Empty the listview
        //        listView1.Items.Clear();
        //        LogTxt = "OBJECTID,PRINTKEY,STNUM,STNAME,PARCELADD,OWNERADD,OWNERNAME";
        //        while (ParcelFeature != null)
        //        {
        //            string SBL = Convert.ToString(ParcelFeature.get_Value(ixSBL));
        //            string OBJECTID = Convert.ToString(ParcelFeature.get_Value(ixOBJECTID));

        //            string printkey = Convert.ToString(ParcelFeature.get_Value(ixPrintkey));
        //            string stnum = Convert.ToString(ParcelFeature.get_Value(ixSTNUM));
        //            string stname = Convert.ToString(ParcelFeature.get_Value(ixSTNAME));
        //            string parceladd = Convert.ToString(ParcelFeature.get_Value(ixParcelAdd));
        //            string owneradd = Convert.ToString(ParcelFeature.get_Value(ixOWNERADD));
        //            string oname = Convert.ToString(ParcelFeature.get_Value(ixONAME));
        //            if (oname.Trim() == "")
        //            {
        //                queryTotal = queryTotal + "Printkey ='" + printkey + "' or ";
        //                ListViewItem lv = new ListViewItem();
        //                lv.SubItems[0].Text = OBJECTID;
        //                lv.SubItems.Add(printkey);
        //                lv.SubItems.Add(SBL);
        //                listView1.Items.Add(lv);
        //                emptyNum = emptyNum + 1;

        //                LogTxt = LogTxt + "\r\n" + OBJECTID + "," + printkey + "," + stnum + "," + stname + "," + parceladd + "," + owneradd + "," + oname + ",";
        //            }

        //            featureNum = featureNum + 1;
        //            ParcelFeature = ParcelCursor.NextFeature();
        //        }

        //        double ratio = emptyNum / featureNum;
        //        if (ratio > 0.2)
        //        {
        //            MessageBox.Show("More then 20 percent empty parcel found. Please Contact with BrianB!");
        //        }

        //        lblAdjacentParcelNum.Text = Convert.ToString(featureNum);
        //        lblNoOwnerNameParcelNum.Text = Convert.ToString(emptyNum);

        //        if (emptyNum != 0)
        //        {
        //            timer1.Start();
        //            timer1.Interval = 200;
        //        }
        //        else
        //        {
        //            timer1.Stop();
        //            lblNoOwnerNameParcelNum.Visible = true;
        //        }

        //        //Select the features intersect with the buffer area
        //        IFeatureSelection myFeatselect = myLayer as IFeatureSelection;
        //        myFeatselect.SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

        //        //MainForm.myExportToDBFSpatialFilter = spatialFilter;

        //        fillAdjancetParcelsGridView(queryTotal, myFeatselect);
        //    }
        //}
        //private void fillAdjancetParcelsGridView(string querySubtract, IFeatureSelection myFeatselect)
        //{
        //    if (querySubtract != string.Empty)
        //    {
        //        IQueryFilter myNewFilter = new QueryFilterClass();
        //        querySubtract = querySubtract.Substring(0, querySubtract.Length - 3);
        //        myNewFilter.WhereClause = querySubtract;
        //        myFeatselect.SelectFeatures(myNewFilter, esriSelectionResultEnum.esriSelectionResultSubtract, false);
        //    }

        //    MainForm.myCurrentSelectionSet = myFeatselect.SelectionSet;

        //    IFeatureLayerDefinition pFLD1 = (IFeatureLayerDefinition)myLayer;

        //    IFeatureLayer myNewFL = pFLD1.CreateSelectionLayer(myLayer.Name, true, null, null);

        //    ITable myTable = myNewFL as ITable;

        //    // Use the ESRI tablewapper class to pass it the featurelayer
        //    TableWrapper myWrapper1 = new TableWrapper(myTable);

        //    // assign the data source converted by tablewrapper to the datagridview and bindingsource
        //    bindingSource2.DataSource = myWrapper1;
        //    dataGridView2.DataSource = myWrapper1;
        //    string configColumnsName = ConfigurationManager.AppSettings["ParcelLayerVisableColumnNames"].ToString().ToLower();
        //    int n = dataGridView2.ColumnCount;
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

        //    pFLD1.DefinitionExpression = "";
        //    myFeatselect.Clear();
        //    dataGridView2.Refresh();
        //}
        private IPolyline ConstructOffset(IPolyline inPolyline, double offset)
        {
            if (inPolyline == null || inPolyline.IsEmpty)
            {
                return null;
            }
            object Missing = Type.Missing;
            object offsetEnum = esriConstructOffsetEnum.esriConstructOffsetSimple;

            IConstructCurve constructCurve = new PolylineClass();
            constructCurve.ConstructOffset(inPolyline, offset, ref offsetEnum, ref Missing);

            return constructCurve as IPolyline;
        }

        private void ChangeRecommendedScale(double currentScale)
        {
            cmbLayoutScale.SelectedIndex = 0;
            if (currentScale <= 1200)
            {
                cmbLayoutScale.SelectedIndex = 1;
            }
            else if (currentScale <= 2400)
            {
                cmbLayoutScale.SelectedIndex = 2;
            }
            else if (currentScale <= 4800)
            {
                cmbLayoutScale.SelectedIndex = 3;
            }
            else if (currentScale <= 9600)
            {
                cmbLayoutScale.SelectedIndex = 4;
            }
            else if (currentScale <= 12000)
            {
                cmbLayoutScale.SelectedIndex = 5;
            }
            else if (currentScale <= 14400)
            {
                cmbLayoutScale.SelectedIndex = 6;
            }

        }

        #endregion Local Methods



        #region Unused Methods
        //private void Flash(IGeometry geometry)
        //{
        //    ISimpleFillSymbol iFillSymbol;
        //    ISymbol iSymbol;
        //    IRgbColor iRgbColor;
        //    iFillSymbol = new SimpleFillSymbol();
        //    iFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
        //    iFillSymbol.Outline.Width = 12;

        //    iRgbColor = new RgbColor();
        //    iRgbColor.RGB = System.Drawing.Color.FromArgb(180, 180, 180).ToArgb();
        //    iFillSymbol.Color = iRgbColor;

        //    iSymbol = (ISymbol)iFillSymbol;
        //    iSymbol.ROP2 = esriRasterOpCode.esriROPNotCopyPen;
        //    IScreenDisplay iScreenDisplay = MainForm.m_mapControl.ActiveView.ScreenDisplay;
        //    MainForm.m_mapControl.FlashShape(geometry, 3, 200, iSymbol);
        //}
        //private ISimpleFillSymbol SetTRSymbol(string leftorright)
        //{
        //    //ISimpleFillSymbol SFS1_2Hour = BaseFun.GenerateSFS(255, 255, 0);
        //    //ISimpleFillSymbol SFS2_Restriction = BaseFun.GenerateSFS(255, 0, 0);
        //    //ISimpleFillSymbol SFS3_Null = BaseFun.GenerateSFS(0, 255, 0);
        //    //ISimpleFillSymbol SFS1_2Hour = BaseFun.GenerateSFSByColor(testcolor);
        //    //ISimpleFillSymbol SFS2_Restriction = BaseFun.GenerateSFSByColor(255, 0, 0);
        //    //ISimpleFillSymbol SFS3_Null = BaseFun.GenerateSFSByColor(0, 255, 0);
        //    switch (leftorright)
        //    {
        //        case "Left":
        //            return BaseFun.GenerateSFSByColor(btnColorL.BackColor);
        //        case "Right":
        //            return BaseFun.GenerateSFSByColor(btnColorR.BackColor);
        //        default:
        //            return null;
        //    }
        //}
        #endregion Unused Methods
    }
}
