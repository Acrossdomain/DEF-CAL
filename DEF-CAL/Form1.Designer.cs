namespace DEF_CAL
{
	partial class Form1
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tab_PrepareBatch = new System.Windows.Forms.TabPage();
			this.lblinvalideDefAccount = new System.Windows.Forms.Label();
			this.lblinvalideRecord = new System.Windows.Forms.Label();
			this.lblRecordRead = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chKbx_EditedRec = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbglAccountTo = new System.Windows.Forms.ComboBox();
			this.btnDownload = new System.Windows.Forms.Button();
			this.btnGo = new System.Windows.Forms.Button();
			this.cmbFiscYr = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbFisPerTo = new System.Windows.Forms.ComboBox();
			this.cmbFisPerFrom = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbglAccount = new System.Windows.Forms.ComboBox();
			this.cmbType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tab_PostEntries = new System.Windows.Forms.TabPage();
			this.tabEditHeader = new System.Windows.Forms.TabPage();
			this.btnEditHeaderOK = new System.Windows.Forms.Button();
			this.dtp_todate = new System.Windows.Forms.DateTimePicker();
			this.dtp_fromDate = new System.Windows.Forms.DateTimePicker();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.btnEditUpdate = new System.Windows.Forms.Button();
			this.dgv_editfield = new System.Windows.Forms.DataGridView();
			this.INVNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.INVDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BILLTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GFUNCTAMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fromdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Todate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DEFMODE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GACCTID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ACDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DEFACCTID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DEFACDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FiscalYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FiscalPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GLBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GLEntNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabControl1.SuspendLayout();
			this.tab_PrepareBatch.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabEditHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgv_editfield)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tab_PrepareBatch);
			this.tabControl1.Controls.Add(this.tab_PostEntries);
			this.tabControl1.Controls.Add(this.tabEditHeader);
			this.tabControl1.Location = new System.Drawing.Point(2, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(881, 440);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tab_PrepareBatch
			// 
			this.tab_PrepareBatch.Controls.Add(this.lblinvalideDefAccount);
			this.tab_PrepareBatch.Controls.Add(this.lblinvalideRecord);
			this.tab_PrepareBatch.Controls.Add(this.lblRecordRead);
			this.tab_PrepareBatch.Controls.Add(this.label9);
			this.tab_PrepareBatch.Controls.Add(this.label8);
			this.tab_PrepareBatch.Controls.Add(this.label7);
			this.tab_PrepareBatch.Controls.Add(this.panel1);
			this.tab_PrepareBatch.Location = new System.Drawing.Point(4, 22);
			this.tab_PrepareBatch.Name = "tab_PrepareBatch";
			this.tab_PrepareBatch.Padding = new System.Windows.Forms.Padding(3);
			this.tab_PrepareBatch.Size = new System.Drawing.Size(873, 414);
			this.tab_PrepareBatch.TabIndex = 0;
			this.tab_PrepareBatch.Text = "Prepare Batch";
			this.tab_PrepareBatch.UseVisualStyleBackColor = true;
			// 
			// lblinvalideDefAccount
			// 
			this.lblinvalideDefAccount.AutoSize = true;
			this.lblinvalideDefAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblinvalideDefAccount.Location = new System.Drawing.Point(215, 159);
			this.lblinvalideDefAccount.Name = "lblinvalideDefAccount";
			this.lblinvalideDefAccount.Size = new System.Drawing.Size(14, 13);
			this.lblinvalideDefAccount.TabIndex = 25;
			this.lblinvalideDefAccount.Text = "0";
			// 
			// lblinvalideRecord
			// 
			this.lblinvalideRecord.AutoSize = true;
			this.lblinvalideRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblinvalideRecord.Location = new System.Drawing.Point(215, 139);
			this.lblinvalideRecord.Name = "lblinvalideRecord";
			this.lblinvalideRecord.Size = new System.Drawing.Size(14, 13);
			this.lblinvalideRecord.TabIndex = 24;
			this.lblinvalideRecord.Text = "0";
			// 
			// lblRecordRead
			// 
			this.lblRecordRead.AutoSize = true;
			this.lblRecordRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRecordRead.Location = new System.Drawing.Point(215, 121);
			this.lblRecordRead.Name = "lblRecordRead";
			this.lblRecordRead.Size = new System.Drawing.Size(14, 13);
			this.lblRecordRead.TabIndex = 23;
			this.lblRecordRead.Text = "0";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(10, 159);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(189, 13);
			this.label9.TabIndex = 22;
			this.label9.Text = "Total  Invalid deferral accounts:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(10, 139);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(204, 13);
			this.label8.TabIndex = 21;
			this.label8.Text = "Total Invalid deferral date records:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(10, 121);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 13);
			this.label7.TabIndex = 20;
			this.label7.Text = "Total Records read:";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.panel1.Controls.Add(this.chKbx_EditedRec);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.cmbglAccountTo);
			this.panel1.Controls.Add(this.btnDownload);
			this.panel1.Controls.Add(this.btnGo);
			this.panel1.Controls.Add(this.cmbFiscYr);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.cmbFisPerTo);
			this.panel1.Controls.Add(this.cmbFisPerFrom);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.cmbglAccount);
			this.panel1.Controls.Add(this.cmbType);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(868, 87);
			this.panel1.TabIndex = 0;
			// 
			// chKbx_EditedRec
			// 
			this.chKbx_EditedRec.AutoSize = true;
			this.chKbx_EditedRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chKbx_EditedRec.Location = new System.Drawing.Point(512, 51);
			this.chKbx_EditedRec.Name = "chKbx_EditedRec";
			this.chKbx_EditedRec.Size = new System.Drawing.Size(183, 17);
			this.chKbx_EditedRec.TabIndex = 31;
			this.chKbx_EditedRec.Text = "Process edited records only";
			this.chKbx_EditedRec.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(560, 19);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(22, 13);
			this.label6.TabIndex = 30;
			this.label6.Text = "To";
			// 
			// cmbglAccountTo
			// 
			this.cmbglAccountTo.FormattingEnabled = true;
			this.cmbglAccountTo.Location = new System.Drawing.Point(585, 15);
			this.cmbglAccountTo.Name = "cmbglAccountTo";
			this.cmbglAccountTo.Size = new System.Drawing.Size(276, 21);
			this.cmbglAccountTo.TabIndex = 29;
			// 
			// btnDownload
			// 
			this.btnDownload.Location = new System.Drawing.Point(782, 47);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(64, 23);
			this.btnDownload.TabIndex = 27;
			this.btnDownload.Text = "Download";
			this.btnDownload.UseVisualStyleBackColor = true;
			this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(701, 48);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(64, 23);
			this.btnGo.TabIndex = 26;
			this.btnGo.Text = "Go";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// cmbFiscYr
			// 
			this.cmbFiscYr.FormattingEnabled = true;
			this.cmbFiscYr.Location = new System.Drawing.Point(69, 50);
			this.cmbFiscYr.Name = "cmbFiscYr";
			this.cmbFiscYr.Size = new System.Drawing.Size(90, 21);
			this.cmbFiscYr.TabIndex = 25;
			this.cmbFiscYr.SelectedIndexChanged += new System.EventHandler(this.cmbFiscYr_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(7, 54);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "FISC YR:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(384, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(22, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "To";
			// 
			// cmbFisPerTo
			// 
			this.cmbFisPerTo.FormattingEnabled = true;
			this.cmbFisPerTo.Location = new System.Drawing.Point(414, 49);
			this.cmbFisPerTo.Name = "cmbFisPerTo";
			this.cmbFisPerTo.Size = new System.Drawing.Size(92, 21);
			this.cmbFisPerTo.TabIndex = 22;
			// 
			// cmbFisPerFrom
			// 
			this.cmbFisPerFrom.FormattingEnabled = true;
			this.cmbFisPerFrom.Location = new System.Drawing.Point(294, 50);
			this.cmbFisPerFrom.Name = "cmbFisPerFrom";
			this.cmbFisPerFrom.Size = new System.Drawing.Size(83, 21);
			this.cmbFisPerFrom.TabIndex = 21;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(189, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(103, 13);
			this.label4.TabIndex = 20;
			this.label4.Text = "FISC PRD  From:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(173, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(119, 13);
			this.label2.TabIndex = 19;
			this.label2.Text = "Revenue A\\C From:";
			// 
			// cmbglAccount
			// 
			this.cmbglAccount.FormattingEnabled = true;
			this.cmbglAccount.Location = new System.Drawing.Point(293, 16);
			this.cmbglAccount.Name = "cmbglAccount";
			this.cmbglAccount.Size = new System.Drawing.Size(265, 21);
			this.cmbglAccount.TabIndex = 18;
			// 
			// cmbType
			// 
			this.cmbType.FormattingEnabled = true;
			this.cmbType.Items.AddRange(new object[] {
            "Revenue",
            "Expense"});
			this.cmbType.Location = new System.Drawing.Point(70, 17);
			this.cmbType.Name = "cmbType";
			this.cmbType.Size = new System.Drawing.Size(89, 21);
			this.cmbType.TabIndex = 17;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(26, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "Type:";
			// 
			// tab_PostEntries
			// 
			this.tab_PostEntries.Location = new System.Drawing.Point(4, 22);
			this.tab_PostEntries.Name = "tab_PostEntries";
			this.tab_PostEntries.Padding = new System.Windows.Forms.Padding(3);
			this.tab_PostEntries.Size = new System.Drawing.Size(873, 414);
			this.tab_PostEntries.TabIndex = 1;
			this.tab_PostEntries.Text = "Post Entries";
			this.tab_PostEntries.UseVisualStyleBackColor = true;
			// 
			// tabEditHeader
			// 
			this.tabEditHeader.Controls.Add(this.btnEditHeaderOK);
			this.tabEditHeader.Controls.Add(this.dtp_todate);
			this.tabEditHeader.Controls.Add(this.dtp_fromDate);
			this.tabEditHeader.Controls.Add(this.label10);
			this.tabEditHeader.Controls.Add(this.label11);
			this.tabEditHeader.Controls.Add(this.btnEditUpdate);
			this.tabEditHeader.Controls.Add(this.dgv_editfield);
			this.tabEditHeader.Location = new System.Drawing.Point(4, 22);
			this.tabEditHeader.Name = "tabEditHeader";
			this.tabEditHeader.Size = new System.Drawing.Size(873, 414);
			this.tabEditHeader.TabIndex = 2;
			this.tabEditHeader.Text = "EditHeader";
			this.tabEditHeader.UseVisualStyleBackColor = true;
			// 
			// btnEditHeaderOK
			// 
			this.btnEditHeaderOK.Location = new System.Drawing.Point(700, 4);
			this.btnEditHeaderOK.Name = "btnEditHeaderOK";
			this.btnEditHeaderOK.Size = new System.Drawing.Size(57, 26);
			this.btnEditHeaderOK.TabIndex = 30;
			this.btnEditHeaderOK.Text = "Go";
			this.btnEditHeaderOK.UseVisualStyleBackColor = true;
			this.btnEditHeaderOK.Click += new System.EventHandler(this.btnEditHeaderOK_Click);
			// 
			// dtp_todate
			// 
			this.dtp_todate.CustomFormat = "yyyy-MM-dd";
			this.dtp_todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_todate.Location = new System.Drawing.Point(600, 8);
			this.dtp_todate.MinDate = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
			this.dtp_todate.Name = "dtp_todate";
			this.dtp_todate.Size = new System.Drawing.Size(94, 20);
			this.dtp_todate.TabIndex = 29;
			// 
			// dtp_fromDate
			// 
			this.dtp_fromDate.CustomFormat = "yyyy-MM-dd";
			this.dtp_fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_fromDate.Location = new System.Drawing.Point(457, 8);
			this.dtp_fromDate.MaxDate = new System.DateTime(2023, 2, 19, 0, 0, 0, 0);
			this.dtp_fromDate.MinDate = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
			this.dtp_fromDate.Name = "dtp_fromDate";
			this.dtp_fromDate.Size = new System.Drawing.Size(109, 20);
			this.dtp_fromDate.TabIndex = 28;
			this.dtp_fromDate.Value = new System.DateTime(2023, 2, 19, 0, 0, 0, 0);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(572, 14);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(22, 13);
			this.label10.TabIndex = 27;
			this.label10.Text = "To";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(369, 11);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(84, 13);
			this.label11.TabIndex = 24;
			this.label11.Text = "Invoice Date:";
			// 
			// btnEditUpdate
			// 
			this.btnEditUpdate.Location = new System.Drawing.Point(782, 4);
			this.btnEditUpdate.Name = "btnEditUpdate";
			this.btnEditUpdate.Size = new System.Drawing.Size(82, 26);
			this.btnEditUpdate.TabIndex = 6;
			this.btnEditUpdate.Text = "Update";
			this.btnEditUpdate.UseVisualStyleBackColor = true;
			this.btnEditUpdate.Click += new System.EventHandler(this.btnInvUpdate_Click);
			// 
			// dgv_editfield
			// 
			this.dgv_editfield.AllowUserToAddRows = false;
			this.dgv_editfield.AllowUserToDeleteRows = false;
			this.dgv_editfield.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv_editfield.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.INVNO,
            this.INVDATE,
            this.BILLTO,
            this.ITEMDESC,
            this.GFUNCTAMT,
            this.fromdate,
            this.Todate,
            this.DEFMODE1,
            this.GACCTID,
            this.ACDESC,
            this.DEFACCTID,
            this.DEFACDESC,
            this.FiscalYear,
            this.FiscalPeriod,
            this.GLBatchNo,
            this.GLEntNo});
			this.dgv_editfield.Location = new System.Drawing.Point(3, 32);
			this.dgv_editfield.Name = "dgv_editfield";
			this.dgv_editfield.Size = new System.Drawing.Size(867, 379);
			this.dgv_editfield.TabIndex = 5;
			this.dgv_editfield.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_editfield_CellClick);
			// 
			// INVNO
			// 
			this.INVNO.DataPropertyName = "INVNO";
			this.INVNO.HeaderText = "Invoice No";
			this.INVNO.Name = "INVNO";
			this.INVNO.ReadOnly = true;
			// 
			// INVDATE
			// 
			this.INVDATE.DataPropertyName = "INVDATE";
			this.INVDATE.HeaderText = "Invoice date";
			this.INVDATE.Name = "INVDATE";
			this.INVDATE.ReadOnly = true;
			// 
			// BILLTO
			// 
			this.BILLTO.DataPropertyName = "BILLTO";
			this.BILLTO.HeaderText = "Bill to ";
			this.BILLTO.Name = "BILLTO";
			this.BILLTO.ReadOnly = true;
			// 
			// ITEMDESC
			// 
			this.ITEMDESC.DataPropertyName = "ITEMDESC";
			this.ITEMDESC.HeaderText = "Item Desc";
			this.ITEMDESC.Name = "ITEMDESC";
			this.ITEMDESC.ReadOnly = true;
			// 
			// GFUNCTAMT
			// 
			this.GFUNCTAMT.DataPropertyName = "GFUNCTAMT";
			this.GFUNCTAMT.HeaderText = "Amount";
			this.GFUNCTAMT.Name = "GFUNCTAMT";
			this.GFUNCTAMT.ReadOnly = true;
			// 
			// fromdate
			// 
			this.fromdate.DataPropertyName = "fromdate";
			this.fromdate.HeaderText = "From date";
			this.fromdate.MaxInputLength = 10;
			this.fromdate.Name = "fromdate";
			this.fromdate.ReadOnly = true;
			// 
			// Todate
			// 
			this.Todate.DataPropertyName = "Todate";
			this.Todate.HeaderText = "To date";
			this.Todate.MaxInputLength = 10;
			this.Todate.Name = "Todate";
			// 
			// DEFMODE1
			// 
			this.DEFMODE1.DataPropertyName = "DEFMODE";
			this.DEFMODE1.HeaderText = "Def. mode";
			this.DEFMODE1.Name = "DEFMODE1";
			// 
			// GACCTID
			// 
			this.GACCTID.DataPropertyName = "GACCTID";
			this.GACCTID.HeaderText = "Account Id";
			this.GACCTID.Name = "GACCTID";
			this.GACCTID.ReadOnly = true;
			// 
			// ACDESC
			// 
			this.ACDESC.HeaderText = "Account description";
			this.ACDESC.Name = "ACDESC";
			this.ACDESC.ReadOnly = true;
			// 
			// DEFACCTID
			// 
			this.DEFACCTID.HeaderText = "Def. Account Id";
			this.DEFACCTID.Name = "DEFACCTID";
			this.DEFACCTID.ReadOnly = true;
			// 
			// DEFACDESC
			// 
			this.DEFACDESC.HeaderText = "Def. Account desc";
			this.DEFACDESC.Name = "DEFACDESC";
			this.DEFACDESC.ReadOnly = true;
			// 
			// FiscalYear
			// 
			this.FiscalYear.HeaderText = "Fiscal Year";
			this.FiscalYear.Name = "FiscalYear";
			this.FiscalYear.ReadOnly = true;
			// 
			// FiscalPeriod
			// 
			this.FiscalPeriod.HeaderText = "FiscalPeriod";
			this.FiscalPeriod.Name = "FiscalPeriod";
			this.FiscalPeriod.ReadOnly = true;
			// 
			// GLBatchNo
			// 
			this.GLBatchNo.HeaderText = "GL Batch No";
			this.GLBatchNo.Name = "GLBatchNo";
			this.GLBatchNo.ReadOnly = true;
			// 
			// GLEntNo
			// 
			this.GLEntNo.HeaderText = "GL Ent No";
			this.GLEntNo.Name = "GLEntNo";
			this.GLEntNo.ReadOnly = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(882, 482);
			this.Controls.Add(this.tabControl1);
			this.MinimumSize = new System.Drawing.Size(800, 500);
			this.Name = "Form1";
			this.Text = "Form1";
			this.tabControl1.ResumeLayout(false);
			this.tab_PrepareBatch.ResumeLayout(false);
			this.tab_PrepareBatch.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tabEditHeader.ResumeLayout(false);
			this.tabEditHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgv_editfield)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tab_PrepareBatch;
		private System.Windows.Forms.TabPage tab_PostEntries;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbglAccountTo;
		private System.Windows.Forms.Button btnDownload;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.ComboBox cmbFiscYr;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cmbFisPerTo;
		private System.Windows.Forms.ComboBox cmbFisPerFrom;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbglAccount;
		private System.Windows.Forms.ComboBox cmbType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblinvalideDefAccount;
		private System.Windows.Forms.Label lblinvalideRecord;
		private System.Windows.Forms.Label lblRecordRead;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TabPage tabEditHeader;
		private System.Windows.Forms.DataGridView dgv_editfield;
		private System.Windows.Forms.Button btnEditUpdate;
		private System.Windows.Forms.CheckBox chKbx_EditedRec;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.DateTimePicker dtp_todate;
		private System.Windows.Forms.DateTimePicker dtp_fromDate;
		private System.Windows.Forms.Button btnEditHeaderOK;
		private System.Windows.Forms.DataGridViewTextBoxColumn INVNO;
		private System.Windows.Forms.DataGridViewTextBoxColumn INVDATE;
		private System.Windows.Forms.DataGridViewTextBoxColumn BILLTO;
		private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
		private System.Windows.Forms.DataGridViewTextBoxColumn GFUNCTAMT;
		private System.Windows.Forms.DataGridViewTextBoxColumn fromdate;
		private System.Windows.Forms.DataGridViewTextBoxColumn Todate;
		private System.Windows.Forms.DataGridViewTextBoxColumn DEFMODE1;
		private System.Windows.Forms.DataGridViewTextBoxColumn GACCTID;
		private System.Windows.Forms.DataGridViewTextBoxColumn ACDESC;
		private System.Windows.Forms.DataGridViewTextBoxColumn DEFACCTID;
		private System.Windows.Forms.DataGridViewTextBoxColumn DEFACDESC;
		private System.Windows.Forms.DataGridViewTextBoxColumn FiscalYear;
		private System.Windows.Forms.DataGridViewTextBoxColumn FiscalPeriod;
		private System.Windows.Forms.DataGridViewTextBoxColumn GLBatchNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn GLEntNo;
	}
}

