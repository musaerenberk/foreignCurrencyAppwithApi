namespace Udemy.Doviz.ApiServis
{
    partial class ProjectInstaller
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
            this.UdemyDovizKurServisi = new System.ServiceProcess.ServiceProcessInstaller();
            this.udemydovizservis = new System.ServiceProcess.ServiceInstaller();
            // 
            // UdemyDovizKurServisi
            // 
            this.UdemyDovizKurServisi.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.UdemyDovizKurServisi.Password = null;
            this.UdemyDovizKurServisi.Username = null;
            // 
            // udemydovizservis
            // 
            this.udemydovizservis.Description = "Udemy eğitimi içinde çekilen bölüm için hazırlandı";
            this.udemydovizservis.DisplayName = "Döviz Uygulaması Servisi";
            this.udemydovizservis.ServiceName = "Döviz Uygulaması Servisi";
            this.udemydovizservis.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.UdemyDovizKurServisi,
            this.udemydovizservis});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller UdemyDovizKurServisi;
        private System.ServiceProcess.ServiceInstaller udemydovizservis;
    }
}