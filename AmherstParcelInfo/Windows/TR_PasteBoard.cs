using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;

namespace AmherstParcelInfo
{
    public partial class TR_PasteBoard : Form
    {
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        private bool alreadyfocustxtdescription = false;
        //private double startfont = 18;
        //private double fontsize;
        public TR_PasteBoard()
        {
            InitializeComponent();
        }

        private void TR_PasteBoard_Load(object sender, EventArgs e)
        {
            this.TITLE = "Parking Restriction";
            
            txtDescription.Text = "";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Active control must be MapControl!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            var tupleResize = new Tuple<List<string>, double>(null, 0);
            string[] originalParas = txtTitle.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (originalParas.Count() > 2)
            {
                MessageBox.Show("Title has over 2 lines, please retype.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                txtTitle.Text = "";
                txtTitle.Focus();
                return;
            }
            tupleResize = BaseFun.TextResize(txtTitle.Text, 20, 230, 20,2);
            List<string> titlelines = tupleResize.Item1;
            List<string> titlelinesOut = new List<string>();

            if (titlelines != null)
            {
                titlelinesOut.Clear();
                foreach (string line in titlelines)
                {
                    titlelinesOut.Add(line.Trim());
                }
                EditHelper.TheTRWindow.mapTitle = string.Join(@"\n", titlelinesOut);
                EditHelper.Titlesize = tupleResize.Item2;
            }


            var tupleResizeDes = new Tuple<List<string>, double>(null, 0);
            tupleResizeDes = BaseFun.TextResize(txtDescription.Text, 42, 230, 10,20);
            List<string> lines = tupleResizeDes.Item1;
            if (lines != null)
                EditHelper.TheTRWindow.Description = string.Join(@"\n", lines);
            else
                return;
            EditHelper.Descriptionsize = tupleResizeDes.Item2;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region When txtDescription get focus, all text will be selected

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            alreadyfocustxtdescription = false;
        }
        private void txtDescription_MouseUp(object sender, MouseEventArgs e)
        {
            // Web browsers like Google Chrome select the text on mouse up.
            // They only do it if the textbox isn't already focused,
            // and if the user hasn't selected all text.
            if (!alreadyfocustxtdescription && this.txtDescription.SelectionLength == 0)
            {
                alreadyfocustxtdescription = true;
                this.txtDescription.SelectAll();
            }
        }
        private void txtDescription_GotFocus(object sender, EventArgs e)
        {
            if (MouseButtons == MouseButtons.None)
            {
                // Select all text only if the mouse isn't down.
                // This makes tabbing to the textbox give focus.
                this.txtDescription.SelectAll();
                alreadyfocustxtdescription = true;
            }
        }

        #endregion When txtDescription get focus, all text will be selected

        //private double startFontSize(string TitleOrDes)
        //{
        //    if (TitleOrDes == "Title")
        //        return FontSizeTit;
        //    else if (TitleOrDes == "Description")
        //        return FontSizeDes;
        //    else return 0;

        //}
        //public List<string> WrapText(string text, double pixels, string fontFamily, double emSizeMin, string TitleorDes)
        //{
        //    double fontsize;
        //    fontsize = startFontSize(TitleorDes);
        //    if (fontsize == 0)
        //        return null;
        //    while (fontsize >= emSizeMin)
        //    {
        //        string[] originalParas = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        //        List<string> wrappedLines = new List<string>();
        //        StringBuilder actualLine = new StringBuilder();
        //        double actualWidth = 0;
        //        double spacelength = (new FormattedText(" ",
        //            CultureInfo.CurrentCulture,
        //            System.Windows.FlowDirection.LeftToRight,
        //            new Typeface(fontFamily),
        //            fontsize, Brushes.Black)).WidthIncludingTrailingWhitespace;
        //        foreach (var paragraph in originalParas)
        //        {
        //            string[] originalLines = paragraph.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        //            foreach (var item in originalLines)
        //            {
        //                FormattedText formatted = new FormattedText(item + " ", CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight,
        //                    new Typeface(fontFamily), fontsize, Brushes.Black);
        //                actualLine.Append(item + " ");

        //                //test
        //                actualWidth += formatted.WidthIncludingTrailingWhitespace;

        //                if (actualWidth > pixels)
        //                {
        //                    string newline = actualLine.ToString();
        //                    newline =newline.Substring(0, newline.Length - (item + " ").Length);
        //                    wrappedLines.Add(newline);

        //                    actualLine.Clear();
        //                    actualWidth = 0;
        //                    actualLine.Append(item + " ");
        //                }
        //            }
        //            if (actualLine.Length > 0)
        //            {
        //                string whitespace = new string(' ', ((int)((pixels - actualWidth) / spacelength)));
        //                wrappedLines.Add(actualLine.Append(whitespace).ToString());
        //                actualLine.Clear();
        //                actualWidth = 0;
        //            }
        //        }
        //        if (wrappedLines.Count() > MaxRowCount(fontsize, TitleorDes))
        //            fontsize -= 1;
        //        else
        //        {
        //            if (TitleorDes == "Title")
        //                titleSize = fontsize;
        //            else if (TitleorDes == "Description")
        //                desSize = fontsize;
        //            return wrappedLines;
        //        }

        //        if (MaxRowCount(fontsize, TitleorDes) == 0)
        //            break;
        //    }
        //    MessageBox.Show("Error::TR_PosteBoard:Description is too long!");
        //    return null;

        //}
        //private int MaxRowCount(double fontsize, string type)
        //{
        //    if (type == "Description")
        //    {
        //        if (fontsize == 7)
        //            return 4;
        //        else if (fontsize == 6)
        //            return 5;
        //        else if (fontsize == 5)
        //            return 7;
        //        else
        //            return 0;
        //    }

        //    else if (type == "Title")
        //    {
        //        if (fontsize > 12)
        //            return 1;
        //        else if (fontsize >= 6)
        //            return 2;
        //        else
        //            return 0;
        //    }
        //    else return 0;
        //}
    }
}
