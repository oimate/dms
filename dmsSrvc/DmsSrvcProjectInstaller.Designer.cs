namespace dmsSrvc
{
    partial class DmsSrvcProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DmsSrvcProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.DmsSrvcInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // DmsSrvcProcessInstaller
            // 
            this.DmsSrvcProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.DmsSrvcProcessInstaller.Password = null;
            this.DmsSrvcProcessInstaller.Username = null;
            // 
            // DmsSrvcInstaller
            // 
            this.DmsSrvcInstaller.Description = "desc";
            this.DmsSrvcInstaller.DisplayName = "Dms Service";
            this.DmsSrvcInstaller.ServiceName = "DmsSrvc";
            // 
            // DmsSrvcProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DmsSrvcProcessInstaller,
            this.DmsSrvcInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller DmsSrvcProcessInstaller;
        private System.ServiceProcess.ServiceInstaller DmsSrvcInstaller;
    }
}