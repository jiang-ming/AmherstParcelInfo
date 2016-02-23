using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ArcDataBinding;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using System.Runtime.InteropServices;
using System.Linq;
using Microsoft.VisualBasic;


namespace AmherstParcelInfo
{
    public partial class SearchWindow : Form
    {
        #region Declare private variables

        private IFeatureLayer myLayer;
        private IFeatureLayer tempLayer;
        private IGraphicsContainer myGC;
        private IActiveView myActive;
        private IFeatureClass myFC;

        private BindingSource binding = new BindingSource();
        private IEnvelope envLayout;
        //public static List<int> MyOIDlist = new List<int>();
        private List<parcel> selectedParcellist = new List<parcel>();
        public string LogTxt;



        #endregion Declare private variables

        #region Declare public variables

        public delegate void ExportToPDFEventHandler(SearchWindow searchWindow);
        public event ExportToPDFEventHandler ExportToPDF;

        public delegate void ActiveMapLayoutViewEventHandler();
        public event ActiveMapLayoutViewEventHandler ActiveMapLayoutView;


        //public static ISelectionSet myCurrentSelectionSet;
        //public static ISpatialFilter myExportToDBFSpatialFilter;
        public bool boolSelectByClickMapChecked = false;
        #endregion Declare Public varibales
        public SearchWindow()
        {
            InitializeComponent();

            //Initial buffer distance combo box
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance100);
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance200);
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance400);
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance600);
            this.cmbBufferDistance.Items.Add(ApplicationConstants.ConstantBufferDistance800);
            this.cmbBufferDistance.SelectedIndex = 3;

            //Initial layoutscale combo box
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScaleAuto);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale100);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale200);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale400);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale800);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale1000);
            this.cmbLayoutScale.Items.Add(ApplicationConstants.ConstantLayoutScale1200);
            this.cmbLayoutScale.SelectedIndex = 1;

            listView1.GridLines = true;
            listView1.Columns.Add("OBJECTID", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("PrintKey", 130, HorizontalAlignment.Left);
            listView1.Columns.Add("SBL", 120, HorizontalAlignment.Left);
            listView1.Visible = true;

            // set the maps view as the active view
            myActive = MainForm.m_mapControl.ActiveView.FocusMap as IActiveView;
            myGC = MainForm.m_mapControl.ActiveView.FocusMap as IGraphicsContainer;

            myLayer = MainForm.myParcelLayer;
            //////////!!!////tempLayer = MainForm.myTempLayer;
            myFC = myLayer.FeatureClass;
        }

        private void SearchWindow_Load(object sender, EventArgs e)
        {
            //set up "myLayer" and indexes for the fields
            IFeatureClass featureclass = myLayer.FeatureClass;

            dataGridView1.ReadOnly = true;
            //dataGridView1.SortedColumn = dataGridView1.Columns["STNUM"];
            cmbSearchBy.SelectedIndex = 0;

            binding.DataSource = selectedParcellist;
            dataGridView3.DataSource = binding;
            dataGridView3.CurrentCell = null;
            MainForm.m_mapControl.CurrentTool = null;
            MainForm.m_pageLayoutControl.CurrentTool = null;
            EditHelper.TheTRWindow = null;
            EditHelper.TheSearchWindow = this;
        }

        private void SearchWindow_MouseClick(object sender, MouseEventArgs e)
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
                // get the layers feature class and set up a query filter
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



        private void ckb_MultiSel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckb_MultiSel.Checked == false)
                {
                    //btnUnselect.Enabled = false;
                    ckbMapClickSelect.Checked = false;
                    //ckbMapClickSelect.Enabled = false;

                    dataGridView1.ClearSelection();
                    #region delete the contents in the datagridview2 and listview1 and zoom to full
                    lblAdjacentParcelNum.ResetText();
                    lblNoOwnerNameParcelNum.ResetText();

                    //MyOIDlist.Clear();
                    selectedParcellist.Clear();
                    lbl_info.Text = string.Empty;
                    dataGridView2.Rows.Clear();
                    listView1.Items.Clear();

                    if (myGC != null)
                    {
                        myGC.DeleteAllElements();
                    }
                    MainForm.m_mapControl.ActiveView.Refresh();
                    dataGridView3.Rows.Clear();
                    binding.ResetBindings(false);

                    #endregion delete the contents in the datagridview2 and listview1 and zoom to full

                }
                else if (ckb_MultiSel.Checked == true)
                {
                    //btnUnselect.Enabled = true;
                    //ckbMapClickSelect.Enabled = true;

                }
                CheckLSelect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ckbMapClickSelect_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbMapClickSelect.Checked == false)
            {
                MainForm.isActivated_MapClickParcelSelect = false;
            }
            else
            {
                MainForm.isActivated_MapClickParcelSelect = true;
            }
            CheckLSelect();
        }

        private void btnUnselect_Click(object sender, EventArgs e)
        {

            lblAdjacentParcelNum.ResetText();
            lblNoOwnerNameParcelNum.ResetText();


            //MyOIDlist.Clear();
            selectedParcellist.Clear();
            lbl_info.Text = string.Empty;
            dataGridView2.Rows.Clear();
            listView1.Items.Clear();

            if (myGC != null)
            {
                myGC.DeleteAllElements();
            }
            MainForm.m_mapControl.ActiveView.Refresh();

            binding.ResetBindings(false);

        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (selectedParcellist.Count > 0)
            {

                string mapTitle = string.Empty;
                if (selectedParcellist.Count == 1)
                {
                    //OBJECTID,SHAPE,Printkey,SWIS, SBL,STNUM,STNUMVAL,STNAME,UNIT,STCODE,FRONTAGE
                    string stName = Convert.ToString(dataGridView3.Rows[0].Cells[dataGridView3.Columns["STNAME"].DisplayIndex].Value);
                    string stNum = Convert.ToString(dataGridView3.Rows[0].Cells[dataGridView3.Columns["STNUM"].DisplayIndex].Value);


                    if (stName.Trim() != "" && stNum.Trim() != "")
                        mapTitle = stNum + " " + stName;
                    else
                        mapTitle = Interaction.InputBox("Map title can not be generated from corresponding fields.\nPlease type it here:", "Input Title Manually", "tempMap");
                }
                else
                {

                    mapTitle = Interaction.InputBox("You have selected more than one parcel.\nPlease type your customized title here for the output pdf:", "Input Title For Multiple Parcels", "tempMultiParcelMap");
                }
                if (mapTitle.Trim() == "")
                {
                    MessageBox.Show("Export Failed! \nPlease type a title after hit export button!");
                    this.Show();
                    return;
                }
                else
                {
                    if (ActiveMapLayoutView != null)
                    {
                        ActiveMapLayoutView();
                    }
                    //Prepare the Layout Template
                    var tupleResize = new Tuple<List<string>, double>(null, 0);
                    tupleResize = BaseFun.TextResize(mapTitle, 20, 230, 20,1);
                    EditHelper.Titlesize = tupleResize.Item2;
                    DataTableToDBF.FillMapLayoutElements(mapTitle, cmbLayoutScale.SelectedItem.ToString(), cmbBufferDistance.SelectedItem.ToString());

                    if (ExportToPDF != null)
                        this.Hide();

                    ExportToPDF(this);

                }
            }
            else
            {
                MessageBox.Show("No parcel is selected!");
                return;
            }
            this.Show();
        }


        private void btnTable_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "DBF database files(*.dbf)|*.dbf|Excel 97-2003 Workbook (*.xls)|*.xls";
                saveFileDialog.Title = "Export to Table(.dbf or .xls)";
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
                MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                return;
            }
            this.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblNoOwnerNameParcelNum.Visible = !lblNoOwnerNameParcelNum.Visible;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                #region Reset the label and the datagridview
                txtParcel.ResetText();

                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();

                lblAdjacentParcelNum.ResetText();
                lblNoOwnerNameParcelNum.ResetText();
                lbl_info.ResetText();


                //MyOIDlist.Clear();
                selectedParcellist.Clear();
                binding.ResetBindings(false);
                listView1.Items.Clear();

                if (myGC != null)
                {
                    myGC.DeleteAllElements();
                }
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

        private void btnLSelect_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm.m_mapControl.CurrentTool = null;
            MainForm.m_pageLayoutControl.CurrentTool = null;
            MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;

            if (ckbMapClickSelect.Checked)
            {
                boolSelectByClickMapChecked = true;
            }
            ckbMapClickSelect.Checked = false;
            MainForm.switchSelectTools("isActivated_SelectParcelByLine");
        }

        /// ?????????????????????

        //private void btnRestrictedStreet_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    MainForm.m_mapControl.CurrentTool = null;
        //    MainForm.m_pageLayoutControl.CurrentTool = null;
        //    MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        //    MainForm.switchSelectTools("isActivated_RestrictedStreet");
        //}

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int Myselect = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                if (OIDContained(Myselect))
                {

                }

                else
                {
                    if ((!ckb_MultiSel.Checked) && (selectedParcellist.Count > 0))
                    {
                        binding.Clear();
                        //MyOIDlist.Clear();
                        selectedParcellist.Clear();
                    }
                    int[] oidListTemp = new int[1];
                    oidListTemp[0] = Myselect;
                    IFeatureCursor ListCursor = myLayer.FeatureClass.GetFeatures(oidListTemp, false);
                    IPolygon polygon = ListCursor.NextFeature().ShapeCopy as IPolygon;
                    addParcel(Myselect,
                        dataGridView1.Rows[e.RowIndex].Cells[MainForm.ixPrintkey].Value.ToString(),
                        dataGridView1.Rows[e.RowIndex].Cells[MainForm.ixSTNAME].Value.ToString(),
                        dataGridView1.Rows[e.RowIndex].Cells[MainForm.ixSTNUM].Value.ToString(),
                        dataGridView1.Rows[e.RowIndex].Cells[MainForm.ixParcelAdd].Value.ToString(),
                        polygon
                        );
                }
                refreshSelectedParcels(selectedParcellist);
                binding.ResetBindings(false);

            }
        }
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int removeINT = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value);
                removeParcelAtOID(removeINT);
            }
            binding.ResetBindings(false);
            refreshSelectedParcels(selectedParcellist);
        }

        private void SearchWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myGC != null && myActive != null)
            {
                myGC.DeleteAllElements();
                myActive.Refresh();
            }
            EditHelper.TheSearchWindow = null;
        }


        #region local private method---------------------------------------------------------------------------------------------------
        private void addParcel(int oid, string printkey, string stname, string stnum, string parceladd, IPolygon shape)
        {
            //MyOIDlist.Add(oid);
            selectedParcellist.Add(new parcel()
            {
                OID = oid,
                PrintKey = printkey,
                STNAME = stname,
                STNUM = stnum,
                ParcelAdd = parceladd,
                Shape = shape
            });
        }
        private void removeParcelAtOID(int oid)
        {
            //MyOIDlist.Remove(oid);
            for (int i = 0; i < selectedParcellist.Count(); i++)
            {
                if (selectedParcellist[i].OID == oid)
                {
                    selectedParcellist.Remove(selectedParcellist[i]);
                    return;
                }
            }
        }
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
        //            //string name = Convert.ToString(ParcelFeature.get_Value(ixONAME));
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

        //    int n = dataGridView2.ColumnCount;
        //    for (int i = 0; i < n; i++)
        //    {
        //        dataGridView2.Columns[i].Visible = false;
        //        if (dataGridView2.Columns[i].Name == "Printkey" | dataGridView2.Columns[i].Name == "OwnerAdd" | dataGridView2.Columns[i].Name == "SBL")
        //        {
        //            dataGridView2.Columns[i].Visible = true;
        //        }
        //    }

        //    pFLD1.DefinitionExpression = "";
        //    myFeatselect.Clear();
        //    dataGridView2.Refresh();
        //}
        //private void FillMapLayoutElements(string customizedTitle)
        //{
        //    if (ActiveMapLayoutView != null)
        //    {
        //        ActiveMapLayoutView();
        //    }

        //    IActiveView myLayoutActive = MainForm.m_pageLayoutControl.PageLayout as IActiveView;
        //    IGraphicsContainer myGC = MainForm.m_pageLayoutControl.PageLayout as IGraphicsContainer;
        //    IMap myMap = myLayoutActive.FocusMap;
        //    IMapFrame myMF = (IMapFrame)myGC.FindFrame(myMap);
        //    if (cmbLayoutScale.SelectedItem.ToString() == "Auto")
        //    {
        //        if (envLayout != null)
        //        {
        //            //myLayoutActive. = envLayout;
        //        }
        //    }
        //    else { myLayoutActive.FocusMap.MapScale = Convert.ToDouble(cmbLayoutScale.SelectedItem.ToString()) * 12; }

        //    myLayoutActive.Refresh();

        //    #region Delete old elements
        //    //Search the myGC and clear the last elements for layout
        //    myGC.Reset();
        //    IElement Element_all = myGC.Next();

        //    while (Element_all != null)
        //    {
        //        if (Element_all is ITextElement)
        //        {
        //            ITextElement tempElemet = Element_all as ITextElement;
        //            if (tempElemet.Text == MainForm.PreviousParceladd)
        //            {
        //                myGC.DeleteElement(Element_all);
        //                myGC.Reset();
        //            }

        //            else if (tempElemet.Text == MainForm.PreviousBufferadd)
        //            {
        //                myGC.DeleteElement(Element_all);
        //                myGC.Reset();
        //            }

        //        }
        //        else if (Element_all is IMapSurroundFrame)
        //        {
        //            myGC.DeleteElement(Element_all);
        //            myGC.Reset();
        //        }
        //        Element_all = myGC.Next();
        //    }

        //    #endregion delete old elements

        //    # region Add a scale text
        //    // CREATE THE ENVELOPE TO DEFINE THE LOCATION OF THE 
        //    // MAP SURROUND (AN ENVELOPE IN THE LOWER LEFT 
        //    IEnvelope pEnv1 = new EnvelopeClass();
        //    pEnv1.PutCoords(3.5, 0.5, 4, 0.8);

        //    //When creating a map surround, need to speciy the item UID 
        //    UID pID = new UIDClass();
        //    pID.Value = "esriCarto.ScaleText";

        //    // CREATE THE SURROUND FRAME USING THE UID for scale bar 

        //    IMapSurroundFrame pMSF = myMF.CreateSurroundFrame(pID, null);
        //    IMapSurround pMapSurr = pMSF.MapSurround;
        //    pMapSurr.Name = "ScaleText";

        //    // CREATE AN ELEMENT AND ASSIGN IT THE MAP SURROUND FRAME
        //    // AND THE ENVELOPE GEOMETRY
        //    IElement pEE1 = (IElement)pMSF;

        //    IScaleText pScaleText = new ScaleTextClass();
        //    pScaleText = (IScaleText)pMapSurr;
        //    INumericFormat pNumericFormat = new NumericFormatClass();
        //    pNumericFormat.UseSeparator = false;
        //    pNumericFormat.RoundingValue = 2;


        //    pScaleText.NumberFormat = pNumericFormat as INumberFormat;
        //    pScaleText.PageUnits = esriUnits.esriInches;
        //    pScaleText.PageUnitLabel = "inch";


        //    //Assign the Map Units to be feet on the Text Scale
        //    pScaleText.MapUnits = esriUnits.esriFeet;
        //    pScaleText.MapUnitLabel = "feet";

        //    IRgbColor pRGB_scale = new RgbColorClass();
        //    pRGB_scale.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Black);

        //    //pScaleText.Style = esriScaleTextStyleEnum.esriScaleTextCustom;
        //    ITextSymbol stTextSymbol = new TextSymbolClass();
        //    stTextSymbol.Size = 14;
        //    stTextSymbol.Color = pRGB_scale;
        //    pScaleText.Symbol = stTextSymbol;


        //    // ADD THE ELEMENT AND REDRAW MAP
        //    pEE1.Geometry = pEnv1;
        //    myGC.AddElement(pEE1, 0);
        //    myLayoutActive.Refresh();

        //    # endregion Add a scale text

        //    #region Add a north arrow

        //    IEnvelope iEnv_NA = new EnvelopeClass();
        //    iEnv_NA.PutCoords(7.3, 0.14, 8.1, 0.93);
        //    ESRI.ArcGIS.esriSystem.UID uid2 = new UIDClass();
        //    uid2.Value = "esriCarto.markerNorthArrow";

        //    // create a surround 

        //    pMSF = myMF.CreateSurroundFrame(uid2, null);
        //    pMapSurr = pMSF.MapSurround;
        //    pMapSurr.Name = "NorthArrow";

        //    //Add the envelop geometry
        //    IElement pEE2 = (IElement)pMSF;

        //    pEE2.Geometry = iEnv_NA;
        //    myGC.AddElement(pEE2, 0);
        //    myLayoutActive.Refresh();

        //    #endregion Add a north arrow

        //    #region Add text elements

        //    //Add title of the map
        //    ITextElement docTextEle = new TextElementClass();
        //    ESRI.ArcGIS.Geometry.IPoint ptPoint = new PointClass();
        //    ITextSymbol pTextSymbol1 = new TextSymbolClass();
        //    IRgbColor pRGB = new RgbColorClass();
        //    pRGB.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Navy);
        //    pTextSymbol1.Color = pRGB;
        //    pTextSymbol1.Size = 18;
        //    pTextSymbol1.Font.Bold = true;
        //    pTextSymbol1.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
        //    ptPoint.PutCoords(1.92, 10.05);

        //    docTextEle.Text = customizedTitle;
        //    docTextEle.Symbol = pTextSymbol1;

        //    //Add buffer distance to lengend
        //    ITextElement docTextBuf = new TextElementClass();
        //    ESRI.ArcGIS.Geometry.IPoint ptPoint2 = new PointClass();
        //    ITextSymbol pTextSymbol_Buf = new TextSymbolClass();
        //    IRgbColor pRGB_buf = new RgbColorClass();
        //    pRGB_buf.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.OrangeRed);
        //    pTextSymbol_Buf.Color = pRGB_buf;
        //    pTextSymbol_Buf.Size = 7;
        //    pTextSymbol_Buf.Font.Bold = true;
        //    ptPoint2.PutCoords(1, 0.33);

        //    docTextBuf.Text = "Buffer " + cmbBufferDistance.SelectedItem.ToString() + "'";
        //    docTextBuf.Symbol = pTextSymbol_Buf;

        //    IElement docEle = docTextEle as IElement;
        //    IElement docEle3 = docTextBuf as IElement;

        //    docEle.Geometry = ptPoint;
        //    docEle3.Geometry = ptPoint2;

        //    myGC.AddElement(docEle, 0);
        //    myGC.AddElement(docEle3, 0);

        //    MainForm.PreviousParceladd = docTextEle.Text;
        //    MainForm.PreviousBufferadd = docTextBuf.Text;

        //    myLayoutActive.Refresh();

        //    #endregion

        //}
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
        private void addFeatureToSelection(IFeature fea)
        {
            int fOID = Convert.ToInt32(fea.get_Value(MainForm.ixOBJECTID));
            if (OIDContained(fOID))
            {
                removeParcelAtOID(fOID);
            }
            else
            {
                if ((!ckb_MultiSel.Checked) && (selectedParcellist.Count > 0))
                {
                    binding.Clear();
                    //MyOIDlist.Clear();
                    selectedParcellist.Clear();
                }

                addParcel(fOID,
                    fea.get_Value(MainForm.ixPrintkey).ToString(),
                    fea.get_Value(MainForm.ixSTNAME).ToString(),
                    fea.get_Value(MainForm.ixSTNUM).ToString(),
                   fea.get_Value(MainForm.ixParcelAdd).ToString(),
                   fea.ShapeCopy as IPolygon
                    );
            }
        }
        private void cmbBufferDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshSelectedParcels(selectedParcellist);
        }
        private void btnPortion_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 1)
            {

                #region PreEdit

                #endregion PreEdit
                #region PostEdit

                #endregion PostEdit
                int[] oidListTemp = new int[1];
                oidListTemp[0] = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value);
                for (int i = 0; i < selectedParcellist.Count(); i++)
                {
                    if (selectedParcellist[i].OID == oidListTemp[0])
                    {

                        CreateFeature(selectedParcellist[i].Shape, oidListTemp[0]);

                    }
                }

            }


        }
        private void CreateFeature(IPolygon polygon, int objectid)
        {

            IFeatureClass featureclass = tempLayer.FeatureClass;
            int iii = featureclass.FeatureCount(null);
            if (iii != 0)
            {
                ITable table = featureclass as ITable;
                table.DeleteSearchedRows(null);
            }
            IFeature feature = featureclass.CreateFeature();
            feature.Shape = polygon;
            int ixOBJECTID = featureclass.FindField("OBJECTID_1");
            feature.set_Value(ixOBJECTID, objectid);
            feature.Store();
            MainForm.m_mapControl.Refresh();
        }
        private bool OIDContained(int oid)
        {
            IEnumerable<parcel> query = selectedParcellist.Where(n => n.OID == oid);
            if (query.Count() == 1) return true;
            else return false;

        }
        private void CheckLSelect()
        {
            if (ckb_MultiSel.Checked && ckbMapClickSelect.Checked)
            {
                btnLSelect.Enabled = true;
            }
            else
            {
                btnLSelect.Enabled = false;
            }
        }
        #endregion

        #region public methods

        public void refreshSelectedParcels(List<parcel> selectedparcellist)
        {

            // Show the number of the parcels selected
            String totalSelectedFeatureNum = Convert.ToString(selectedparcellist.Count);
            this.lbl_info.Text = totalSelectedFeatureNum + " parcels selected!";

            if (selectedparcellist.Count == 0)
            {
                lblAdjacentParcelNum.ResetText();
                lblNoOwnerNameParcelNum.ResetText();

                dataGridView2.Rows.Clear();
                listView1.Items.Clear();

                if (myGC != null)
                    myGC.DeleteAllElements();
                MainForm.m_mapControl.ActiveView.Refresh();
            }
            else
            {
                //Send the selected parcels into one geometryBag
                IGeometryBag geometryBag = new GeometryBagClass();
                IGeometryCollection geometryCollection = (IGeometryCollection)geometryBag;
                IGeoDataset pGeoDataset = (IGeoDataset)myLayer.FeatureClass;
                //IFeatureSelection pSelectionFeatures = (IFeatureSelection)myLayer;
                ISpatialReference spRef = pGeoDataset.SpatialReference;
                geometryBag.SpatialReference = spRef;

                #region Union Selected Parcels

                IEnvelope pEnv = new EnvelopeClass();
                IGeometry pShp;

                object missingType = Type.Missing;
                foreach (parcel parcel in selectedparcellist)
                {
                    geometryCollection.AddGeometry(parcel.Shape, ref missingType, ref missingType);
                }
                ITopologicalOperator unionPolygon = new PolygonClass();
                unionPolygon.ConstructUnion(geometryCollection as IEnumGeometry);
                IPolygon pUnionPolygon = unionPolygon as IPolygon;
                pEnv = pUnionPolygon.Envelope;
                pEnv.Expand(1.2, 1.2, true);
                pShp = pUnionPolygon as IGeometry;
                #endregion Union Selected Parcels

                #region Prepare Symbols for the selected parcels and the buffer area
                // Symbol for the selected parcels, dark green/green
                IRgbColor selectFillColor = new RgbColorClass();
                selectFillColor.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Green);
                //0111//selectFillColor.Transparency = (byte)127;
                IRgbColor outlineColor = new RgbColorClass();
                outlineColor.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.DarkGreen);

                ISimpleLineSymbol simpyline_outline = new SimpleLineSymbolClass();
                simpyline_outline.Color = outlineColor;
                simpyline_outline.Width = 2;

                ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
                fillSymbol.Color = selectFillColor;
                fillSymbol.Outline = simpyline_outline;
                fillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;

                //Symbol for the buffer area, orangered.
                IRgbColor selectColor1 = new RgbColorClass();
                selectColor1.RGB = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.OrangeRed);

                ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbolClass();
                simpleLineSymbol.Width = 3;
                simpleLineSymbol.Color = selectColor1;
                simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDash;

                ISimpleFillSymbol fillSymbol1 = new SimpleFillSymbolClass();
                fillSymbol1.Outline = simpleLineSymbol;
                fillSymbol1.Style = esriSimpleFillStyle.esriSFSNull;
                #endregion Prepare Symbols for the selected parcels and the buffer area

                #region Create buffer element

                ESRI.ArcGIS.Geometry.ITopologicalOperator topologicalOperator;
                topologicalOperator = (ITopologicalOperator)pShp;
                IFillShapeElement pPolygon_buf = new PolygonElementClass();
                pPolygon_buf.Symbol = fillSymbol1;
                IElement element_Buf;
                element_Buf = new PolygonElementClass();
                element_Buf = pPolygon_buf as IElement;
                element_Buf.Geometry = topologicalOperator.Buffer(Convert.ToDouble(cmbBufferDistance.SelectedItem.ToString()));
                #endregion Create buffer element
                //Adjacent Parcels By Buffer current selected parcel
                BaseFun.selectAdjacentParcelsByBuffer(myLayer, element_Buf, listView1, lblAdjacentParcelNum, lblNoOwnerNameParcelNum, timer1, bindingSource2, dataGridView2);
                #region Add buffer element and selected parcels into GC
                if (myGC != null)
                {
                    myGC.DeleteAllElements();
                    myGC.AddElement(element_Buf, 0);
                    foreach (parcel parcel in selectedparcellist)
                    {
                        IFillShapeElement pPolygonEle = new PolygonElementClass();
                        IElement pEle_shape = pPolygonEle as IElement;
                        pPolygonEle.Symbol = fillSymbol;
                        pEle_shape.Geometry = parcel.Shape as IGeometry;
                        myGC.AddElement(pEle_shape, 0);
                    }
                }
                #endregion Add buffer element and selected parcels into GC

                #region Avoid the overlay of the annotation
                //Avoid the overlay of the annotation
                IGraphicsLayer pGraLayer = myActive.FocusMap.BasicGraphicsLayer;
                IBarrierProperties pBarrierProp = pGraLayer as IBarrierProperties;
                pBarrierProp.Weight = (int)esriBasicOverposterWeight.esriMediumWeight;
                myActive.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                #endregion Avoid the overlay of the annotation

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

                #region zoom extent of the map control to the buffer area
                envLayout.Expand(1.2, 1.2, true);
                myActive.Extent = envLayout;
                myActive.Refresh();
                myActive.ScreenDisplay.UpdateWindow();

                //Change Color of cell (-1,-1)
                //dataGridView1[1, 1].Style.BackColor = Color.Red;

                #endregion
            }
        }
        public void addParcel_WithMapSelect(IFeature fea)
        {
            addFeatureToSelection(fea);
            refreshSelectedParcels(selectedParcellist);
            binding.ResetBindings(false);
        }
        public void addParcel_WithLSelect(IFeatureCursor fcursor)
        {
            IFeature fea = fcursor.NextFeature();
            while (fea != null)
            {
                addFeatureToSelection(fea);
                fea = fcursor.NextFeature();
            }
            ckbMapClickSelect.Checked = boolSelectByClickMapChecked;
            refreshSelectedParcels(selectedParcellist);
            binding.ResetBindings(false);
            this.Show();
            MainForm.m_mapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }


        #endregion

        #region Flash selected rows
        //Paused -- %% Flash selcted rows in the qua view
        private void Flash(IGeometry geometry)
        {
            IEnvelope env = new EnvelopeClass();
            env.PutCoords(1088650, 1087852, 1101584, 1098790);
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
            IScreenDisplay iScreenDisplay = MainForm.m_mapControl.ActiveView.ScreenDisplay;
            MainForm.m_mapControl.FlashShape(geometry, 3, 200, iSymbol);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView1.Rows[e.RowIndex].Selected)
                {
                    //dataGridView1.Rows[e.RowIndex].Selected = false;
                }
                else
                {
                    quadViewUnselect();
                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                    }
                }
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView2.Rows[e.RowIndex].Selected)
                {
                    //dataGridView2.Rows[e.RowIndex].Selected = false;
                }
                else
                {
                    quadViewUnselect();
                    if (dataGridView2.Rows.Count > 0)
                    {
                        dataGridView2.Rows[e.RowIndex].Selected = true;
                    }
                }
            }

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView3.Rows[e.RowIndex].Selected)
                {
                    //dataGridView3.Rows[e.RowIndex].Selected = false;
                }
                else
                {
                    quadViewUnselect();
                    if (dataGridView3.Rows.Count > 0)
                    {
                        dataGridView3.Rows[e.RowIndex].Selected = true;
                    }
                }
            }
        }

        private void quadViewUnselect()
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();

        }
        #endregion Flash selected rows

        private void cmbBufferDistance_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            refreshSelectedParcels(selectedParcellist);
        }

        private void ckbMapClickSelect_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }








    }
}
