using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;

namespace AssetsManagementEF.Web {
    public class Global : System.Web.HttpApplication {
        public Global() {
            InitializeComponent();
        }
        protected void Application_Start(Object sender, EventArgs e) {
			SecurityAdapterHelper.Enable();
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
#if EASYTEST
            DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = true;
#endif
        }
        protected void Session_Start(Object sender, EventArgs e) {
		    Tracing.Initialize();
            AssetsManagementEFAspNetApplication xafapp = new AssetsManagementEFAspNetApplication();
            xafapp.LinkNewObjectToParentImmediately = true;
            WebApplication.SetInstance(Session, new AssetsManagementEFAspNetApplication());
            DevExpress.ExpressApp.Web.Templates.DefaultVerticalTemplateContentNew.ClearSizeLimit();
            //DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
            //WebApplication.Instance.SwitchToNewStyle();

            #region generalsetting
            string temp = "";

            temp = ConfigurationManager.AppSettings["EmailSend"].ToString();
            AssetsManagementEF.Module.GeneralSettings.EmailSend = false;
            if (temp.ToUpper() == "Y" || temp.ToUpper() == "YES" || temp.ToUpper() == "TRUE" || temp == "1")
                AssetsManagementEF.Module.GeneralSettings.EmailSend = true;

            AssetsManagementEF.Module.GeneralSettings.EmailHost = ConfigurationManager.AppSettings["EmailHost"].ToString();
            AssetsManagementEF.Module.GeneralSettings.EmailPort = ConfigurationManager.AppSettings["EmailPort"].ToString();
            AssetsManagementEF.Module.GeneralSettings.Email = ConfigurationManager.AppSettings["Email"].ToString();
            AssetsManagementEF.Module.GeneralSettings.EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();
            AssetsManagementEF.Module.GeneralSettings.EmailName = ConfigurationManager.AppSettings["EmailName"].ToString();

            temp = ConfigurationManager.AppSettings["EmailSSL"].ToString();
            AssetsManagementEF.Module.GeneralSettings.EmailSSL = false;
            if (temp.ToUpper() == "Y" || temp.ToUpper() == "YES" || temp.ToUpper() == "TRUE" || temp == "1")
                AssetsManagementEF.Module.GeneralSettings.EmailSSL = true;

            temp = ConfigurationManager.AppSettings["EmailUseDefaultCredential"].ToString();
            AssetsManagementEF.Module.GeneralSettings.EmailUseDefaultCredential = false;
            if (temp.ToUpper() == "Y" || temp.ToUpper() == "YES" || temp.ToUpper() == "TRUE" || temp == "1")
                AssetsManagementEF.Module.GeneralSettings.EmailUseDefaultCredential = true;


            temp = ConfigurationManager.AppSettings["B1Post"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1Post = false;
            if (temp.ToUpper() == "Y" || temp.ToUpper() == "YES" || temp.ToUpper() == "TRUE" || temp == "1")
                AssetsManagementEF.Module.GeneralSettings.B1Post = true;

            AssetsManagementEF.Module.GeneralSettings.B1UserName = ConfigurationManager.AppSettings["B1UserName"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1Password = ConfigurationManager.AppSettings["B1Password"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1Server = ConfigurationManager.AppSettings["B1Server"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1CompanyDB = ConfigurationManager.AppSettings["B1CompanyDB"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1License = ConfigurationManager.AppSettings["B1License"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1Sld = ConfigurationManager.AppSettings["B1Sld"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1DbServerType = ConfigurationManager.AppSettings["B1DbServerType"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1Language = ConfigurationManager.AppSettings["B1Language"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1DbUserName = ConfigurationManager.AppSettings["B1DbUserName"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1DbPassword = ConfigurationManager.AppSettings["B1DbPassword"].ToString();

            AssetsManagementEF.Module.GeneralSettings.B1PRseries = int.Parse(ConfigurationManager.AppSettings["B1PRseries"].ToString());

            AssetsManagementEF.Module.GeneralSettings.B1PRCol = ConfigurationManager.AppSettings["B1PRCol"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1PRNoCol = ConfigurationManager.AppSettings["B1PRNoCol"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1WONoCol = ConfigurationManager.AppSettings["B1WONoCol"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1PRLineIDCol = ConfigurationManager.AppSettings["B1PRLineIDCol"].ToString();

            AssetsManagementEF.Module.GeneralSettings.B1CTCol = ConfigurationManager.AppSettings["B1CTCol"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1CTNoCol = ConfigurationManager.AppSettings["B1CTNoCol"].ToString();
            AssetsManagementEF.Module.GeneralSettings.B1AttachmentPath = ConfigurationManager.AppSettings["B1AttachmentPath"].ToString();
           
            #endregion

            WebApplication.Instance.CustomizeFormattingCulture += Instance_CustomizeFormattingCulture;

            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            if(System.Diagnostics.Debugger.IsAttached && WebApplication.Instance.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                WebApplication.Instance.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
            //WebApplication.Instance.Settings.DefaultVerticalTemplateContentPath = "DefaultVerticalTemplateContent1.ascx";
            //WebApplication.Instance.Settings.LogonTemplateContentPath = "LogonTemplateContent1.ascx";
            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
        }

        private void Instance_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e)
        {

            e.FormattingCulture.NumberFormat.CurrencySymbol = "";
            e.FormattingCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
        }

        protected void Application_BeginRequest(Object sender, EventArgs e) {
        }
        protected void Application_EndRequest(Object sender, EventArgs e) {
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
        }
        protected void Application_Error(Object sender, EventArgs e) {
            ErrorHandling.Instance.ProcessApplicationError();
        }
        protected void Session_End(Object sender, EventArgs e) {
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(Object sender, EventArgs e) {
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
