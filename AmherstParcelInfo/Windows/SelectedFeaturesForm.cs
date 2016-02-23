using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace AmherstParcelInfo
{
    public partial class SelectedFeaturesForm : Form
    {

        public SelectedFeaturesForm()
        {
            InitializeComponent();
        }

        private void SelectedFeaturesForm_Load(object sender, EventArgs e)
        {
            lbl_selectedNo.Text = "The selected parcel No. is " + MainForm.sum_n.ToString() + "\r\n" + "The number of parcels without ownername is " + MainForm.sum_NoOwnername.ToString();

            dataGridView1.DataSource = MainForm.tableWrapper;
            bindingSource1.DataSource = MainForm.tableWrapper;

            //Disable some columns
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Refresh();
        }

        private void btn_dbfExport_Click(object sender, EventArgs e)
        {
            try
            {
                //show the open file dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "(*.dbf)|*.dbf";
                saveFileDialog.Title = "Export to DBF";
                saveFileDialog.InitialDirectory = ConfigurationManager.AppSettings["InitialDirectory"];
                saveFileDialog.RestoreDirectory = false;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ITableName pInTablename = MainForm.myDataSet.FullName as ITableName;
                    IDatasetName pInDsName = pInTablename as IDatasetName;

                    IWorkspaceFactory pWokespaceFact = new ShapefileWorkspaceFactory();
                    string FilePath_output = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                    IWorkspace pWksp = pWokespaceFact.OpenFromFile(FilePath_output, 0);
                    IDataset pWksPdataset = pWksp as IDataset;
                    IWorkspaceName pWkSpName = pWksPdataset.FullName as IWorkspaceName;
                    IDatasetName pOutDsName = new TableNameClass();
                    string FileName_output = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                    string FileFullPath = saveFileDialog.FileName;

                    FileInfo f_name = new FileInfo(saveFileDialog.FileName);
                    if (f_name.Exists == true)
                    {
                        File.Delete(FileFullPath);
                    }

                    pOutDsName.Name = FileName_output;
                    pOutDsName.WorkspaceName = pWkSpName;

                    if (MainForm.myDataSet != null)
                    {
                        // export to table

                        IFeatureDataConverter2 myFDC = new FeatureDataConverterClass();

                        myFDC.ConvertTable(pInDsName, null, MainForm.mySelSet, pOutDsName, MainForm.myTable.Fields, "", 0, 0);


                        //Save a hidden copy of the dbf
                        #region Hidden folder and file
                        //Hidden the copy folder and file
                        try
                        {
                            string ExtFile = System.IO.Path.GetExtension(saveFileDialog.FileName);
                            string FileName = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName) + "_hidden";
                            string FilePath = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
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

                            string newFilePath = FilePath + @"\" + datea + @"\" + FileName + ExtFile;
                            FileInfo f = new FileInfo(newFilePath);
                            if (f.Exists == true)
                            {
                                string timea = string.Format("{0:hh_mm_ss}", DateTime.Now);
                                FileName = FileName + "_" + timea;
                                newFilePath = FilePath + @"\" + datea + @"\" + FileName + ExtFile;
                            }
                            else
                            {

                            }
                            File.Delete(newFilePath);
                            File.Copy(saveFileDialog.FileName, newFilePath, true);
                            f = new FileInfo(newFilePath);
                            f.Attributes = FileAttributes.Hidden;

                        }
                        catch
                        {
                            MessageBox.Show("Double copy is not allowed, which was not expected");
                        }

                        #endregion Hidden folder and file

                        //Show export successfully and open the dbf automatically 
                        MessageBox.Show("Your *dbf was exported successfully!");

                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                    else
                    {

                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            lbl_selectedNo.Text = "No Parcels selected!";
            MainForm.myFeatselect.Clear();
            MainForm.myFeatselect.SelectionChanged();
            MainForm.m_mapControl.ActiveView.Refresh();

        }
    }
}