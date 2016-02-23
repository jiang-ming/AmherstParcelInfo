using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AmherstParcelInfo
{
    class EditHelper
    {
        private static EditHelper instance;
        private MainForm m_mainform = null;
        private bool m_editorFormOpen;
        private TRWindow m_trwindow = null;
        private double titlesize = 18;
        private double descriptionsize = 7;
        private SearchWindow m_searchwindow = null;

        private EditHelper() { }
        public static MainForm TheMainForm
        {
            get
            {
                if (instance != null)
                {
                    return instance.m_mainform;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (instance == null)
                {
                    instance = new EditHelper();
                }
                instance.m_mainform = value;
            }
        }
        public static TRWindow TheTRWindow
        {
            get
            {
                if (instance != null)
                {
                    return instance.m_trwindow;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (instance == null)
                {
                    instance = new EditHelper();
                }
                instance.m_trwindow = value;
            }
        }
        public static SearchWindow TheSearchWindow
        {
            get
            {
                if (instance != null)
                {
                    return instance.m_searchwindow;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (instance == null)
                {
                    instance = new EditHelper();
                }
                instance.m_searchwindow = value;
            }
        }
        public static double Titlesize
        {
            get
            {
                if (instance != null)
                {
                    return instance.titlesize;
                }
                else
                {
                    return 18;
                }
            }
            set
            {
                if (instance == null)
                {
                    instance = new EditHelper();
                }
                instance.titlesize = value;
            }
        }
        public static double Descriptionsize
        {
            get
            {
                if (instance != null)
                {
                    return instance.descriptionsize;
                }
                else
                {
                    return 7;
                }
            }
            set
            {
                if (instance == null)
                {
                    instance = new EditHelper();
                }
                instance.descriptionsize = value;
            }
        }
    }
}
