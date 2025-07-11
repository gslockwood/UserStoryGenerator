﻿using System.Reflection;

namespace UserStoryGenerator.View
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            if( !string.IsNullOrEmpty(AssemblyDescription) )
                this.textBoxDescription.Text = AssemblyDescription.Replace(@"\n", Environment.NewLine);
        }

        #region Assembly Attribute Accessors

        public static string? AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if( attributes.Length > 0 )
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if( titleAttribute.Title != "" )
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public static string? AssemblyVersion
        {
            get
            {
                Version? temp = Assembly.GetExecutingAssembly().GetName().Version;
                if( temp == null )
                    return "";
                else
                    return temp.ToString();

            }
        }

        public static string? AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if( attributes.Length == 0 )
                {
                    return "";
                }
                return ( (AssemblyDescriptionAttribute)attributes[0] ).Description;
            }
        }

        public static string? AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if( attributes.Length == 0 )
                {
                    return "";
                }
                return ( (AssemblyProductAttribute)attributes[0] ).Product;
            }
        }

        public static string? AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if( attributes.Length == 0 )
                {
                    return "";
                }
                return ( (AssemblyCopyrightAttribute)attributes[0] ).Copyright;
            }
        }

        public static string? AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if( attributes.Length == 0 )
                {
                    return "";
                }
                return ( (AssemblyCompanyAttribute)attributes[0] ).Company;
            }
        }
        #endregion
    }
}
