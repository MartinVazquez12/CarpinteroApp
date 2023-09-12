namespace Carpintero405708App.Presentacion
{
    partial class FrmReporteProductos
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tPRODUCTOSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetCarpinteria = new Carpintero405708App.Reportes.DataSetCarpinteria();
            this.dataSetCarpinteriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.t_PRODUCTOSTableAdapter = new Carpintero405708App.Reportes.DataSetCarpinteriaTableAdapters.T_PRODUCTOSTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.tPRODUCTOSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetCarpinteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetCarpinteriaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.BackColor = System.Drawing.SystemColors.Control;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.tPRODUCTOSBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Carpintero405708App.Reportes.ReportViewerCarpinteria.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(2, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(668, 408);
            this.reportViewer1.TabIndex = 0;
            // 
            // tPRODUCTOSBindingSource
            // 
            this.tPRODUCTOSBindingSource.DataMember = "T_PRODUCTOS";
            this.tPRODUCTOSBindingSource.DataSource = this.dataSetCarpinteria;
            // 
            // dataSetCarpinteria
            // 
            this.dataSetCarpinteria.DataSetName = "DataSetCarpinteria";
            this.dataSetCarpinteria.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataSetCarpinteriaBindingSource
            // 
            this.dataSetCarpinteriaBindingSource.DataSource = this.dataSetCarpinteria;
            this.dataSetCarpinteriaBindingSource.Position = 0;
            // 
            // t_PRODUCTOSTableAdapter
            // 
            this.t_PRODUCTOSTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReporteProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(672, 433);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmReporteProductos";
            this.Text = "Reporte Productos";
            this.Load += new System.EventHandler(this.FrmReporteProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tPRODUCTOSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetCarpinteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetCarpinteriaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dataSetCarpinteriaBindingSource;
        private Reportes.DataSetCarpinteria dataSetCarpinteria;
        private System.Windows.Forms.BindingSource tPRODUCTOSBindingSource;
        private Reportes.DataSetCarpinteriaTableAdapters.T_PRODUCTOSTableAdapter t_PRODUCTOSTableAdapter;
    }
}