using GemBox.Spreadsheet;
using GIIS_SHL_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEF_CAL
{
	public partial class Form1 : Form
	{
		public String ServerName = "";
		public String UserName = "";
		public string Password = "";
		public string DBName = "";
		public String SageUserName = "";
		public string SagePassword = "";
		public string SageDBName = "";

		DateTime startPrdMonth;
		DateTime endFslyear;
		DataTable def_Ac_tbl;
		DataTable def_tbl_Hdata;
		DataTable Cmb_data_dt;
		string connectionstring = "";
		dynamic person = new ExpandoObject();
		public Form1()
		{
			InitializeComponent();
			GetServerConfig();
			cmbFiscYr.Items.Add(DateTime.Now.Year - 1);
			cmbFiscYr.Items.Add(DateTime.Now.Year);
			cmbFiscYr.Items.Add(DateTime.Now.Year + 1);
			getglaccount();
			Cmb_data_dt = new DataTable();
			Cmb_data_dt.Columns.Add("Prd", typeof(string));
			Cmb_data_dt.Columns.Add("Months", typeof(string));
		}
		public void GetServerConfig()
		{
			ReadWriteXML xml1 = new ReadWriteXML();
			try
			{
				bool conStatus = xml1.ReadXML();
				if (conStatus == false)
				{
					//obLOg.WriteLog("Config File not found.", "  Date" + System.DateTime.Now.ToString("MM-dd-yyyy"));
				}
				else
				{
					ServerName = xml1.ServerName;
					UserName = xml1.UserName;
					Password = xml1.Password;
					DBName = xml1.DBName;
					SageUserName = xml1.SageUserName;
					SagePassword = xml1.SagePassword;
					SageDBName = xml1.SageDBName;
					connectionstring = "Data Source="+ ServerName + "; Initial Catalog="+ DBName + "; User ID="+ UserName + "; Password="+ Password + ";";
				}
			}
			catch (Exception ex)
			{
				//obLOg.WriteLog("Geting Configuration Details Failed :" + ex.Message, "");
			}
		}
		public void getglaccount()
		{
			System.Data.SqlClient.SqlConnection conn;
			System.Data.SqlClient.SqlCommand cmd;
			
			////string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
			//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";

			conn = new System.Data.SqlClient.SqlConnection(connectionstring);
			conn.Open();
			string Querystring = "select (RTRIM(VALUE)+'>'+RTRIM(ACCTDESC)) VALUE,RTRIM(VDESC) VDESC,ACCTDESC from CSOPTFD opf join GLAMF mf on mf.ACCTFMTTD = opf.VALUE  where opf.OPTFIELD = 'DEFREV' ";
			cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
			cmd.CommandTimeout = 180;
			cmd.CommandType = CommandType.Text;
			cmd.ExecuteNonQuery();
			using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter())
			{

				DataRow dr;
				cmd.Connection = conn;
				sda.SelectCommand = cmd;
				using (def_Ac_tbl = new DataTable())
				{
					sda.Fill(def_Ac_tbl);
					dr = def_Ac_tbl.NewRow();
					dr.ItemArray = new object[] { "--Select GL Account--", 0 };
					def_Ac_tbl.Rows.InsertAt(dr, 0);

				}
			}
			DataTable toactt = def_Ac_tbl.Copy();
			cmbglAccount.DisplayMember = "VALUE";
			cmbglAccount.ValueMember = "VDESC";
			cmbglAccount.DataSource = def_Ac_tbl;

			cmbglAccountTo.DisplayMember = "VALUE";
			cmbglAccountTo.ValueMember = "VDESC";
			cmbglAccountTo.DataSource = toactt;
		}

		public void getglaccountDesc(string Glacc)
		{
			System.Data.SqlClient.SqlConnection conn;
			System.Data.SqlClient.SqlCommand cmd;
			DataTable def_tbl;
			////string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
			//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";

			conn = new System.Data.SqlClient.SqlConnection(connectionstring);
			conn.Open();
			string Querystring = "select ACCTFMTTD,ACCTDESC from GLAMF where ACCTFMTTD='" + Glacc + "' ";
			cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
			cmd.CommandTimeout = 180;
			cmd.CommandType = CommandType.Text;
			cmd.ExecuteNonQuery();
			using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter())
			{
				cmd.Connection = conn;
				sda.SelectCommand = cmd;
				using (def_tbl = new DataTable())
				{
					sda.Fill(def_tbl);
				}
			}
			label7.Text = def_tbl.Rows[0]["ACCTDESC"].ToString();
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			FillGrid();
		}
		public void FillGrid()
		{
			System.Data.SqlClient.SqlConnection conn;
			System.Data.SqlClient.SqlCommand cmd;
			System.Data.SqlClient.SqlDataAdapter sda=null;
			////string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
			//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";
			string[] actFrom= cmbglAccount.Text.Split('>');
			string[] actTo = cmbglAccountTo.Text.Split('>');
			string[] from = cmbFisPerFrom.Text.Split('-');
			string[] to = cmbFisPerTo.Text.Split('-');
			conn = new System.Data.SqlClient.SqlConnection(connectionstring);
			conn.Open();
			string Querystring_edit = "select DEFIDH,GACCTID,GFACCTID,DEFACCTID DEFACNo,DEFMODE MODE,GFISYR,GFISPRD,GBATCHNO,GENTNO,GPOTDETNO,GJRNLDT,GFUNCTAMT,GSRCAMT,GLDRLSRC,GLDRLNK,GLDRAPP,ARINVBATCH,ARINVENTRYNO,DAYENDNO,ARINVNO," +
			   "  C1ROWN,INVNO,INVDATE,BILLTO,DLINENO,ITEM,ITEMDESC,INVNIQ,ROW1,convert(varchar, FROMDATE, 112) FROMDT,convert(varchar, TODATE, 112)  TODT,DEFBATCHNO,DEFENTNO,DEFDATE,INSERTEDON,VALFLAG " +
				" from XADEFH where(GACCTID >= '"+ actFrom[0] + "' and GACCTID<= '"+ actTo[0] + "') and GFISYR = '"+ cmbFiscYr.Text + "' and(GFISPRD >= "+from[0]+" and GFISPRD <= "+to[0]+") and ValFlag = 2 ";
			//string Querystring = "GSTMAS.[dbo].[SP_DEFFEREL_Mode]";
			string Querystring = "WITH CTE1 ( a1,b2,c3,d4,e5,f6,g7,h8,i9,j10,k11,l12,m13,n14,o15, p16, q17, r18) AS " +
" ( Select GLPOST.ACCTID GACCTID1, GLAMF.ACCTFMTTD GACCTFMTTD2, GLPOST.FISCALYR GFISCALYR3, GLPOST.FISCALPERD GFISCALPERD4, " +
"             GLPOST.BATCHNBR GLBATCHNBR5, GLPOST.ENTRYNBR GLENTNO6, GLPOST.CNTDETAIL GLCNT7, " +
" GLPOST.JRNLDATE GLDATE8, (GLPOST.TRANSAMT*-1) GLAMTF9,(GLPOST.SCURNAMT*-1) GLAMTS10, GLPOST.DRILSRCTY GLDRLSRC11, GLPOST.DRILLDWNLK GLDRLNK12, " +
" GLPOST.DRILAPP GLDRAPP13,SUBSTRING(CAST(GLPOST.DRILLDWNLK AS VARCHAR),3+(CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),1,1) AS INT)), " +
" CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),2,1) as int)) ARINVBATCH14,CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar)," +
" (CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),1,1) AS INT)+CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),2,1) AS INT)+3), " +
" (LEN(GLPOST.DRILLDWNLK)-(2+(CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),1,1) AS INT)+CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),2,1)   " +
" AS INT))))) AS INT) ARINVENTRYNO15,ARH.DRILLDWNLK DAYENDNO16,ARH.IDINVC ARINVNO17,  " +
" ROW_NUMBER() OVER (PARTITION BY ARH.DRILLDWNLK ORDER BY GLPOST.BATCHNBR, GLPOST.ENTRYNBR, GLPOST.CNTDETAIL ASC) ROWN18  " +
" from GLPOST GLPOST JOIN GLAMF GLAMF ON GLPOST.ACCTID=GLAMF.ACCTID LEFT JOIN ARIBH ARH ON   " +
" SUBSTRING(CAST(GLPOST.DRILLDWNLK AS VARCHAR),3+(CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),1,1) AS INT)),  " +
" CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),2,1) as int))=ARH.CNTBTCH AND CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),  " +
" (CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),1,1) AS INT)+CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),2,1) AS INT)+3),  " +
" (LEN(GLPOST.DRILLDWNLK)-(2+(CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),1,1) AS INT)+CAST(SUBSTRING(CAST(GLPOST.DRILLDWNLK AS varchar),2,1)   " +
" AS INT))))) AS INT)=ARH.CNTITEM  " +
" WHERE GLPOST.ACCTID>=REPLACE(RTRIM('" + actFrom[0] + "'),'-','') and GLPOST.ACCTID<=REPLACE(RTRIM('" + actTo[0] + "'),'-','') and GLPOST.DRILAPP='AR'  AND GLPOST.DRILSRCTY=0 and GLPOST.TRANSAMT < 0   " +
" AND GLPOST.FISCALYR='" + cmbFiscYr.Text + "' and GLPOST.FISCALPERD>=" + from[0] + " and  GLPOST.FISCALPERD<=" + to[0] + "),  " +
" CTE2 (INVNO,INVDATE, BILLTO, DLINENO, ITEM, ITEMDESC, INVNIQ ,ROW1,MODE,FROMDT,TODT )  AS  " +
" (SELECT H1.INVNUMBER,H1.INVDATE, H1.BILNAME, D1.LINENUM, D1.ITEM, D1.[DESC], H1.INVUNIQ  " +
" ,ROW_NUMBER() OVER (PARTITION BY D1.INVUNIQ ORDER BY D1.LINENUM ASC) ROWN1,isnull(md.VALUE,(select DEFVAL from OEOFD where OPTFIELD='defmode')),frm.VALUE,  " +
" tod.VALUE  FROM OEINVH H1 JOIN CTE1 CTJ1 on H1.DAYENDNUM=CTJ1.p16 JOIN ICCATG ICT ON CTJ1.b2=ICT.REVENUACCT  " +
" JOIN OEINVD D1 ON H1.INVUNIQ=D1.INVUNIQ AND ICT.CATEGORY=D1.CATEGORY AND CTJ1.j10=D1.EXTINVMISC  " +
" left join OEINVDO md on md.OPTFIELD ='DEFMODE' and md.INVUNIQ=h1.INVUNIQ and md.LINENUM=d1.LINENUM  " +
" left join OEINVDO frm on frm.OPTFIELD ='DEFFRMD' and md.INVUNIQ=h1.INVUNIQ and frm.LINENUM=d1.LINENUM  " +
" left join OEINVDO tod on tod.OPTFIELD ='DEFTODT' and md.INVUNIQ =h1.INVUNIQ  and tod.LINENUM=d1.LINENUM  " +
" GROUP BY  H1.INVNUMBER,H1.INVDATE,D1.LINENUM, H1.BILNAME, D1.ITEM, D1.[DESC], H1.INVUNIQ, D1.INVUNIQ,md.VALUE,frm.VALUE,tod.VALUE )  " +
" SELECT C2.INVNO,C2.INVDATE, C2.BILLTO, C1.i9 GFUNCTAMT,C2.ITEMDESC,C1.a1 GACCTID, C1.b2 GFACCTID, C1.c3 GFISYR, C1.d4 GFISPRD, C1.e5 GBATCHNO, C1.f6 GENTNO,  " +
" C1.g7 GPOTDETNO, C1.h8 GJRNLDT, C1.j10 GSRCAMT, C1.k11 GLDRLSRC, C1.l12 GLDRLNK,   " +
" C1.m13 GLDRAPP, C1.n14 ARINVBATCH, C1.o15 ARINVENTRYNO, C1.p16 DAYENDNO, C1.q17 ARINVNO, C1.r18 C1ROWN,c2.MODE,c2.FROMDT,c2.TODT,cs.VDESC DEFACNo,  " +
" C2.DLINENO, C2.ITEM,  C2.INVNIQ, C2.ROW1 FROM CTE1 C1 JOIN CTE2 C2 ON C1.q17=C2.INVNO AND C1.r18=C2.ROW1  " +
" join CSOPTFD cs on cs.VALUE=c1.b2 and cs.OPTFIELD='DEFREV' join XADEFH x on c1.e5<>x.GBATCHNO and c1.f6<>x.GENTNO ";
			string editstring = "";
			if (chKbx_EditedRec.Checked == false)
			{
				editstring = "";
				cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
				cmd.CommandTimeout = 180;
				cmd.CommandType = CommandType.Text;

				//cmd.Parameters.AddWithValue("@acId", SqlDbType.VarChar).Value = actFrom[0];
				//cmd.Parameters.AddWithValue("@acIdto", SqlDbType.VarChar).Value = actTo[0];//00
				//cmd.Parameters.AddWithValue("@year", SqlDbType.VarChar).Value = cmbFiscYr.Text;
				//cmd.Parameters.AddWithValue("@fromPrd", SqlDbType.Int).Value = from[0];
				//cmd.Parameters.AddWithValue("@toPrd", SqlDbType.Int).Value = to[0];
				cmd.ExecuteNonQuery();
				 sda = new System.Data.SqlClient.SqlDataAdapter(cmd);
				sda.SelectCommand = cmd;
			}
			else
			{
				editstring = "EDITINSERT";
				cmd = new System.Data.SqlClient.SqlCommand(Querystring_edit, conn);
				cmd.CommandTimeout = 180;
				cmd.CommandType = CommandType.Text;
				cmd.ExecuteNonQuery();
				sda = new System.Data.SqlClient.SqlDataAdapter(cmd);
				sda.SelectCommand = cmd;
			}
			
			//cmd.Connection = conn;
			
			using (def_tbl_Hdata = new DataTable())
			{
				sda.Fill(def_tbl_Hdata);
			}
			lblRecordRead.Text = def_tbl_Hdata.Rows.Count.ToString();
	        if(def_tbl_Hdata.Rows.Count==0)
			{
				MessageBox.Show("Data not fount!");
				return;
			}
			int ID = 0;
			foreach (DataRow dr in def_tbl_Hdata.Rows)
			{
				
				if(editstring == "EDITINSERT")
					ID = Convert.ToInt32(dr["DEFIDH"].ToString());
				int defdate_count = 0;
				int glac_count = 0;
				decimal Defamount = 0;
				string GlAc = dr["GACCTID"].ToString();
				string GlFAc = dr["GFACCTID"].ToString();
				string DEFACNo = dr["DEFACNo"].ToString();
			    string mode=	dr["MODE"].ToString().Trim();
		        string fromdate = dr["FROMDT"].ToString();
	            string todate = dr["TODT"].ToString();
				
				string GFISYR = dr["GFISYR"].ToString();
				string GFISPRD = dr["GFISPRD"].ToString();
				string GBATCHNO = dr["GBATCHNO"].ToString();
				string GENTNO = dr["GENTNO"].ToString().Trim();
				string GPOTDETNO = dr["GPOTDETNO"].ToString();
				string GJRNLDT = dr["GJRNLDT"].ToString();

				string GSRCAMT = dr["GSRCAMT"].ToString();
				string GLDRLSRC = dr["GLDRLSRC"].ToString();
				string GLDRLNK = dr["GLDRLNK"].ToString();
				string GLDRAPP = dr["GLDRAPP"].ToString().Trim();
				string ARINVBATCH = dr["ARINVBATCH"].ToString();
				string ARINVENTRYNO = dr["ARINVENTRYNO"].ToString();

				string DAYENDNO = dr["DAYENDNO"].ToString();
				string ARINVNO = dr["ARINVNO"].ToString();
				string C1ROWN = dr["C1ROWN"].ToString();
				string DLINENO = dr["DLINENO"].ToString().Trim();
				string ITEM = dr["ITEM"].ToString();
				string INVNIQ = dr["INVNIQ"].ToString();
				
				string ROW1 = dr["ROW1"].ToString();
				string ITEMDESC = dr["ITEMDESC"].ToString();
				string GFUNCTAMT = dr["GFUNCTAMT"].ToString();
				string BILLTO = dr["BILLTO"].ToString().Trim();
				string INVDATE = dr["INVDATE"].ToString();
				string INVNO = dr["INVNO"].ToString();


				Defamount = Convert.ToDecimal(dr["GFUNCTAMT"].ToString());
				if ((string.IsNullOrWhiteSpace(fromdate)==true) && (string.IsNullOrWhiteSpace(todate) == true))
				{
					defdate_count = defdate_count + 1;
				}
				else if ((string.IsNullOrWhiteSpace(fromdate) == true) && (string.IsNullOrWhiteSpace(todate) == false))
				{
					defdate_count = defdate_count + 1;
				}
				else if ((string.IsNullOrWhiteSpace(fromdate) == false) && (string.IsNullOrWhiteSpace(todate) == true))
				{
					defdate_count = defdate_count + 1;
				}
				else if (Convert.ToDecimal(fromdate)>Convert.ToDecimal(todate))
				{
					defdate_count = defdate_count + 1;
				}
				 if(getaccountid_status(DEFACNo) ==false)
				{
					glac_count = glac_count + 1;
				}
				if (string.IsNullOrWhiteSpace(mode) == true)
				{
					MessageBox.Show(DefMode());
					mode = DefMode();
				}				

				if (defdate_count==0 && glac_count==0)
				{
					int fiscalperiod = Convert.ToInt16(dr["GFISPRD"].ToString());
					DataRow[] ofPrd = Cmb_data_dt.Select("prd = "+ fiscalperiod);
					int monthofPrd =Convert.ToUInt16(ofPrd[0]["months"].ToString());
					int curretyr = Convert.ToInt32(cmbFiscYr.Text);
					string frm = fromdate.Insert(4, "-");
					string frm1 = frm.Insert(7, "-");
					string tod = todate.Insert(4, "-");
					string to1 = tod.Insert(7, "-");
					var NoofDay =Convert.ToDateTime(to1)- Convert.ToDateTime(frm1);
					var ss = NoofDay.Days +1;
					var amtofday = Defamount / ss;
					int PrdMonthYear = Convert.ToInt16(curretyr.ToString());
					DateTime fromdt = Convert.ToDateTime(frm1.TrimEnd());
					int i_year = curretyr;
					DateTime endofFslyear = endFslyear;
					int identity;
					if (editstring != "EDITINSERT")
					{
						identity = Createheader(GlAc, GlFAc, DEFACNo, mode, fromdate, todate, GFISYR, GFISPRD, GBATCHNO, GENTNO, GPOTDETNO, GJRNLDT,
					   GSRCAMT, GLDRLSRC, GLDRLNK, GLDRAPP, ARINVBATCH, ARINVENTRYNO, DAYENDNO, ARINVNO, C1ROWN, DLINENO, ITEM, INVNIQ, ROW1, ITEMDESC, GFUNCTAMT, BILLTO, INVDATE, INVNO, 1);
					}
					else
						identity = ID;

					if (mode == "YRLY")
					{
						try
						{
							for (int i = curretyr; Defamount > 0; i++)
							{
								decimal Periodamount = 0;
								#region MONTHLY ....
								/*
								for (int j = fiscalperiod; j<=12; j++)
								{
									if (monthofPrd > 12)
									{
										monthofPrd = 1;
										i_year++;
									}	
										PrddaysMonth = i_year.ToString() + '-' + monthofPrd.ToString().PadLeft(2,'0') + '-' + DateTime.DaysInMonth(i_year, monthofPrd).ToString();
									int kk = 0;
									if (fromdt <= Convert.ToDateTime(PrddaysMonth))
									{
										var days = Convert.ToDateTime(PrddaysMonth) - fromdt;
										var prdAmount = (Convert.ToInt16(days.Days) + 1) * amtofday;
										if (Defamount <= prdAmount)
										{
											if (Defamount >= 1)
											{
												Periodamount = Defamount;
												InsertDetailLn(GlAc, GlFAc, Convert.ToDecimal(fromdt.ToString("yyyyMMdd")), Periodamount, i.ToString(), j.ToString(), fromdt.ToString("yyyy-MM-dd").ToString(), PrddaysMonth);
											}
											j = 13;
											Defamount = 0;
										}
										else
										{
											Defamount = Defamount - prdAmount;
											Periodamount = prdAmount;	
										}
										if (fromdt.Day > 1 && fromdt.Day < 31)
										{
											fromdt = fromdt.AddDays(fromdt.Day + 1);
										} else
										if ((Convert.ToInt16(days.Days) + 1) > 31)
										{
											fromdt = fromdt.AddMonths(2);
										}
										else
										fromdt = fromdt.AddMonths(1);
										//double ddd = DateTime.DaysInMonth(fromdt.Year, fromdt.Month);
										//PrddaysMonth = fromdt.AddDays(ddd-1).ToString();
										kk++;
									}
									else { kk = 0; }
									if(j!=13 && kk>0)
										InsertDetailLn(GlAc, GlFAc, Convert.ToDecimal(fromdt.ToString("yyyyMMdd")), Periodamount, i.ToString(), j.ToString(), fromdt.ToString("yyyy-MM-dd").ToString(), PrddaysMonth);

									monthofPrd++;
									if (j == 12)
									{
										fiscalperiod = 1;
									}
								}
								*/
								#endregion
								int kk = 0;
								if (fromdt <= endofFslyear)
								{
									var days = endofFslyear - fromdt;
									var prdAmount = (Convert.ToInt16(days.Days) + 1) * amtofday;
									if (Defamount <= prdAmount)
									{
										if (Defamount >= 1)
										{
											Periodamount = Defamount;
											InsertDetailLn(identity, GlAc, GlFAc, Convert.ToDecimal(fromdt.ToString("yyyyMMdd")), Defamount, i.ToString(), "0", fromdt.ToString("yyyy-MM-dd").ToString(), endofFslyear.ToString("yyyy-MM-dd").ToString());
										}
										Defamount = 0;
									}
									else
									{
										Defamount = Defamount - prdAmount;
										Periodamount = prdAmount;
										kk++;
									}
								}
								else
								{
									kk = 0;
								}
								if (kk > 0)
								{
									InsertDetailLn(identity, GlAc, GlFAc, Convert.ToDecimal(fromdt.ToString("yyyyMMdd")), Periodamount, i.ToString(), "0", fromdt.ToString("yyyy-MM-dd").ToString(), endofFslyear.ToString("yyyy-MM-dd").ToString());
									fromdt = endofFslyear.AddDays(1);
								}
								endofFslyear = endofFslyear.AddYears(1);
							}
						}
						catch (Exception)
						{

							throw;
						}
					}
					else if (mode == "MNTHLY")
					{
						try
						{
							for (int i = curretyr; Defamount > 0; i++)
							{
								decimal Periodamount = 0;
								string PrddaysMonth = "";
								#region MONTHLY ....							
								for (int j = fiscalperiod; j <= 12; j++)
								{
									if (monthofPrd > 12)
									{
										monthofPrd = 1;
										i_year++;
									}
									PrddaysMonth = i_year.ToString() + '-' + monthofPrd.ToString().PadLeft(2, '0') + '-' + DateTime.DaysInMonth(i_year, monthofPrd).ToString();
									int kk = 0;
									if (fromdt <= Convert.ToDateTime(PrddaysMonth))
									{
										var days = Convert.ToDateTime(PrddaysMonth) - fromdt;
										var prdAmount = (Convert.ToInt16(days.Days) + 1) * amtofday;
										if (Defamount <= prdAmount)
										{
											if (Defamount >= 1)
											{
												Periodamount = Defamount;
												InsertDetailLn(identity, GlAc, GlFAc, Convert.ToDecimal(fromdt.ToString("yyyyMMdd")), Periodamount, i.ToString(), j.ToString(), fromdt.ToString("yyyy-MM-dd").ToString(), PrddaysMonth);
											}
											j = 13;
											Defamount = 0;
										}
										else
										{
											Defamount = Defamount - prdAmount;
											Periodamount = prdAmount;
										}
										if (fromdt.Day > 1 && fromdt.Day < 31)
										{
											fromdt = fromdt.AddDays(fromdt.Day + 1);
										}
										else
										if ((Convert.ToInt16(days.Days) + 1) > 31)
										{
											fromdt = fromdt.AddMonths(2);
										}
										else
											fromdt = fromdt.AddMonths(1);
										kk++;
									}
									else { kk = 0; }
									if (j != 13 && kk > 0)
										InsertDetailLn(identity, GlAc, GlFAc, Convert.ToDecimal(fromdt.ToString("yyyyMMdd")), Periodamount, i.ToString(), j.ToString(), fromdt.ToString("yyyy-MM-dd").ToString(), PrddaysMonth);

									monthofPrd++;
									if (j == 12)
									{
										fiscalperiod = 1;
									}
								}
								#endregion
							}
						}
						catch (Exception)
						{

							throw;
						}
						
					}
					UpdateInvHeader(ID);
				}
				else
				{
				int identity=	Createheader(GlAc, GlFAc, DEFACNo, mode, fromdate, todate, GFISYR, GFISPRD, GBATCHNO, GENTNO, GPOTDETNO, GJRNLDT,
					GSRCAMT, GLDRLSRC, GLDRLNK, GLDRAPP, ARINVBATCH, ARINVENTRYNO, DAYENDNO, ARINVNO, C1ROWN, DLINENO, ITEM, INVNIQ, ROW1, ITEMDESC, GFUNCTAMT, BILLTO, INVDATE, INVNO,0); 
				}
			}
			
		}
		public int Createheader(string GlAc, string GlFAc, string DEFACNo, string mode, string fromdate, string todate, string GFISYR, string GFISPRD, string GBATCHNO, string GENTNO, string GPOTDETNO, string GJRNLDT, 
					string GSRCAMT, string GLDRLSRC, string GLDRLNK, string GLDRAPP, string ARINVBATCH, string ARINVENTRYNO, string DAYENDNO, string ARINVNO, string C1ROWN, string DLINENO, string ITEM, string INVNIQ, string ROW1, string ITEMDESC, string GFUNCTAMT, string BILLTO, string INVDATE, string INVNO,int flag)
		{
			//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";
			using (SqlConnection con = new SqlConnection(connectionstring))
			{
				int newID;
				var qry = "INSERT INTO XADEFH(GACCTID,GFACCTID,DEFACCTID,DEFMODE,GFISYR,GFISPRD,GBATCHNO,GENTNO,GPOTDETNO,GJRNLDT,GFUNCTAMT,GSRCAMT,"+
                     "GLDRLSRC,GLDRLNK,GLDRAPP,ARINVBATCH,ARINVENTRYNO,DAYENDNO,ARINVNO,C1ROWN,INVNO,INVDATE,BILLTO,DLINENO,"+
                     "ITEM,ITEMDESC,INVNIQ,ROW1,fromdate,todate,DefbatchNo,defentNo,defdate,InsertedOn,ValFlag)"+
					" VALUES(@GlAc, @GlFAc, @DEFACNo, @mode, @GFISYR, @GFISPRD, @GBATCHNO, @GENTNO, @GPOTDETNO, @GJRNLDT, "+
					"  @GFUNCTAMT,@GSRCAMT, @GLDRLSRC, @GLDRLNK, @GLDRAPP, @ARINVBATCH, @ARINVENTRYNO, @DAYENDNO, @ARINVNO, @C1ROWN, @INVNO, @INVDATE, @BILLTO, @DLINENO, @ITEM, @ITEMDESC, @INVNIQ, @ROW1, @fromdate, @todate,@DefbatchNo,@defentNo,@defdate,@InsertedOn,@ValFlag); " +
					"SELECT CAST(scope_identity() AS int)";

				using (SqlCommand cmd = new SqlCommand(qry, con))
				{
					cmd.Parameters.AddWithValue("@GlAc", GlAc);
					cmd.Parameters.AddWithValue("@GlFAc", GlFAc);
					cmd.Parameters.AddWithValue("@DEFACNo", DEFACNo);
					cmd.Parameters.AddWithValue("@mode", mode);
					cmd.Parameters.AddWithValue("@fromdate", fromdate);
					cmd.Parameters.AddWithValue("@todate", todate);
					cmd.Parameters.AddWithValue("@GFISYR", GFISYR);
					cmd.Parameters.AddWithValue("@GFISPRD", GFISPRD);
					cmd.Parameters.AddWithValue("@GBATCHNO", GBATCHNO);
					cmd.Parameters.AddWithValue("@GENTNO", GENTNO);
					cmd.Parameters.AddWithValue("@GPOTDETNO", GPOTDETNO);
					cmd.Parameters.AddWithValue("@GJRNLDT", GJRNLDT);
					cmd.Parameters.AddWithValue("@GSRCAMT", GSRCAMT);
					cmd.Parameters.AddWithValue("@GLDRLSRC", GLDRLSRC);
					cmd.Parameters.AddWithValue("@GLDRLNK", GLDRLNK);
					cmd.Parameters.AddWithValue("@GLDRAPP", GLDRAPP);
					cmd.Parameters.AddWithValue("@ARINVBATCH", ARINVBATCH);
					cmd.Parameters.AddWithValue("@ARINVENTRYNO", ARINVENTRYNO);
					cmd.Parameters.AddWithValue("@DAYENDNO", DAYENDNO);
					cmd.Parameters.AddWithValue("@ARINVNO", ARINVNO);
					cmd.Parameters.AddWithValue("@C1ROWN", C1ROWN);
					cmd.Parameters.AddWithValue("@DLINENO", DLINENO);
					cmd.Parameters.AddWithValue("@ITEM", ITEM);
					cmd.Parameters.AddWithValue("@INVNIQ", INVNIQ);
					cmd.Parameters.AddWithValue("@ROW1", ROW1);
					cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
					cmd.Parameters.AddWithValue("@GFUNCTAMT", GFUNCTAMT);
					cmd.Parameters.AddWithValue("@BILLTO", BILLTO);
					cmd.Parameters.AddWithValue("@INVDATE", INVDATE);
					cmd.Parameters.AddWithValue("@INVNO", INVNO);
					//@fromdate, @todate,@DefbatchNo,@defentNo,@defdate,@InsertedOn,@ValFlag
					//cmd.Parameters.AddWithValue("@fromdate", fromdate);
					cmd.Parameters.AddWithValue("@defentNo", "");
					cmd.Parameters.AddWithValue("@DefbatchNo", "");
					cmd.Parameters.AddWithValue("@defdate", "");
					cmd.Parameters.AddWithValue("@InsertedOn", DateTime.Now);
					cmd.Parameters.AddWithValue("@ValFlag", flag);
					con.Open();
					newID = (int)cmd.ExecuteScalar();

					if (con.State == System.Data.ConnectionState.Open) con.Close();
					return newID;
				}
			}
		}
		public Boolean getaccountid_status(string ACCT)
		{
			Boolean returnvalue = false;
			try
			{
				System.Data.SqlClient.SqlConnection connR;
				System.Data.SqlClient.SqlCommand cmdR;
				String sQueryDeff;
				DataTable dtCheckamt;
				sQueryDeff = "";
				dtCheckamt = new DataTable();//Select * from [CTLDAT].dbo.GLAMF  WHERE ACCTFMTTD ='40101-07' AND ACSEGVAL08=''
				//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";
				sQueryDeff = "Select * from [GSTMAS].dbo.GLAMF where  ACCTFMTTD='" + ACCT + "' ";
				System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
				connR = new System.Data.SqlClient.SqlConnection(connectionstring);
				connR.Open();
				cmdR = new System.Data.SqlClient.SqlCommand(sQueryDeff, connR);
				cmdR.CommandTimeout = 180;
				cmdR.CommandType = CommandType.Text;
				sda.SelectCommand = cmdR;
				sda.Fill(dtCheckamt);
				if (dtCheckamt.Rows.Count > 0)
				{
					returnvalue = true;
					//ojbLog.WriteLog(logfilename, "Loan GL Account exist in Sage.....");
				}
				else
				{ //ojbLog.WriteLog(logfilename, "GL Account-" + DisSageAccId.Trim() + " does not exist in Sage....."); 
				}
			}
			catch (Exception ex)
			{
				returnvalue = false;
				//ojbLog.WriteLog(logfilename, ex.Message);
				//ojbLog.WriteLog(Disb_LogFile_failed, "Getaccountid_status function failed..." + ex.Message);
			}
			return returnvalue;
		}
		public string DefMode()
		{
			string returnvalue = "";
			try
			{
				System.Data.SqlClient.SqlConnection connR;
				System.Data.SqlClient.SqlCommand cmdR;
				String sQueryDeff;
				DataTable dtCheckamt;
				sQueryDeff = "";
				dtCheckamt = new DataTable();//Select * from [CTLDAT].dbo.GLAMF  WHERE ACCTFMTTD ='40101-07' AND ACSEGVAL08=''
				//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";
				sQueryDeff = "select DEFVAL from OEOFD where OPTFIELD='defmode' ";
				System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
				connR = new System.Data.SqlClient.SqlConnection(connectionstring);
				connR.Open();
				cmdR = new System.Data.SqlClient.SqlCommand(sQueryDeff, connR);
				cmdR.CommandTimeout = 180;
				cmdR.CommandType = CommandType.Text;
				sda.SelectCommand = cmdR;
				sda.Fill(dtCheckamt);
				if (dtCheckamt.Rows.Count > 0)
				{
					returnvalue = dtCheckamt.Rows[0]["DEFVAL"].ToString();
					//ojbLog.WriteLog(logfilename, "Loan GL Account exist in Sage.....");
				}
				else
				{ //ojbLog.WriteLog(logfilename, "GL Account-" + DisSageAccId.Trim() + " does not exist in Sage....."); 
				}
			}
			catch (Exception ex)
			{
				returnvalue = ex.ToString();
				//ojbLog.WriteLog(logfilename, ex.Message);
				//ojbLog.WriteLog(Disb_LogFile_failed, "Getaccountid_status function failed..." + ex.Message);
			}
			return returnvalue;
		}
		private void btnDownload_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You want to download CSV file ?", "Defferal ", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				try
				{
					string filename = OpenSavefileDialog();
					//ToCSV(def_tbl_Hdata, filename);
					Export(def_tbl_Hdata, filename);
				}
				catch (Exception ex)
				{
					
				}
			}
			else if (dialogResult == DialogResult.No)
			{
				return;
			}
		}
		private void Export(DataTable def_tbl_Hdata,string filename)
		{
			if (def_tbl_Hdata.Rows.Count > 0)
			{
				SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
				//Exporting to Excel
				//string folderPath = "C:\\Excel\\";
				//if (!Directory.Exists(folderPath))
				//{
				//	Directory.CreateDirectory(folderPath);
				//}
				//using (XLWorkbook wb = new XLWorkbook())
				//{
				//	wb.Worksheets.Add(def_tbl_Hdata, "Customers");
				//	wb.SaveAs(folderPath + "DataGridViewExport.xlsx");
				//}
				var workbook = new ExcelFile();
				var worksheet = workbook.Worksheets.Add("DataTable to Sheet");
				//worksheet.Cells[0, 0].Value = "DataTable insert example:";

				// Insert DataTable to an Excel worksheet.
				worksheet.InsertDataTable(def_tbl_Hdata,
					new InsertDataTableOptions()
					{
						ColumnHeaders = true,
						StartRow = 0
					});

				workbook.Save(filename);

				//Microsoft.Office.Interop.Excel.ApplicationClass XcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
				//XcelApp.Application.Workbooks.Add(Type.Missing);
				//for (int i = 1; i < def_tbl_Hdata.Columns.Count + 1; i++)
				//{
				//	XcelApp.Cells[1, i] = def_tbl_Hdata.Columns[i - 1].ColumnName;
				//}
				//for (int i = 0; i < def_tbl_Hdata.Rows.Count; i++)
				//{
				//	for (int j = 0; j < def_tbl_Hdata.Columns.Count; j++)
				//	{
				//		XcelApp.Cells[i + 2, j + 1] = def_tbl_Hdata.Rows[i][j].ToString();
				//	}
				//}
				//XcelApp.Columns.AutoFit();
				//XcelApp.Visible = true;
			}
		}
		public void ToCSV(DataTable dtDataTable, string strFilePath)
		{
			StreamWriter sw = new StreamWriter(strFilePath, false);

			System.Data.SqlClient.SqlConnection conn;
			System.Data.SqlClient.SqlCommand cmd;
			////string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
			//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";
			
			conn = new System.Data.SqlClient.SqlConnection(connectionstring);
			conn.Open();
			string Querystring = "select h.DEFIDH,h.GFACCTID,h.DEFACCTID,h.DEFMODE,h.GFISYR,h.GFISPRD,h.GBATCHNO,h.GENTNO,h.GPOTDETNO,h.GJRNLDT,h.GFUNCTAMT,h.INVNO,h.INVDATE,h.BILLTO,h.DLINENO,h.ITEM,h.ITEMDESC,h.INVNIQ,h.fromdate HeaderFromDate,h.todate HeaderToDate, "+
                                  " d.DEFIDD,d.GACCTID,d.DEFACCTID,d.GJRNLDT,d.GBATCHNO,d.GENTNO,d.GPOTDETNO,d.FROMDATE DetFromDate, d.TODATE DetToDate, d.GFUNCTAMT,d.FISYEAR,d.FISPRD from XADEFH h  join XADEFD d on d.DEFIDH = h.DEFIDH ";
			
			cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
			cmd.CommandTimeout = 180;
			cmd.CommandType = CommandType.Text;
			cmd.ExecuteNonQuery();
			System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(cmd);
			//cmd.Connection = conn;
			sda.SelectCommand = cmd;
			DataTable def_Exl_Down;
			using (def_Exl_Down = new DataTable())
			{
				sda.Fill(def_Exl_Down);
			}


			//headers    
			for (int i = 0; i < def_Exl_Down.Columns.Count; i++)
			{
				sw.Write(def_Exl_Down.Columns[i]);
				if (i < def_Exl_Down.Columns.Count - 1)
				{
					sw.Write(",");
				}
			}
			sw.Write(sw.NewLine);
			foreach (DataRow dr in def_Exl_Down.Rows)
			{
				for (int i = 0; i < def_Exl_Down.Columns.Count; i++)
				{
					if (!Convert.IsDBNull(dr[i]))
					{
						string value = dr[i].ToString();
						if (value.Contains(','))
						{
							value = String.Format("\"{0}\"", value);
							sw.Write(value);
						}
						else
						{
							sw.Write(dr[i].ToString());
						}
					}
					if (i < def_Exl_Down.Columns.Count - 1)
					{
						sw.Write(",");
					}
				}
				sw.Write(sw.NewLine);
			}
			sw.Close();
		}
		public string OpenSavefileDialog()
		{
			string Filename = null;
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "CSV files (*.csv)|*.csv|Excel Files|*.xls;*.xlsx";
			//saveFileDialog.Filter = "csv File|*.csv";
			saveFileDialog.Title = "Save Report";
			DialogResult dialogResult = saveFileDialog.ShowDialog();

			if (dialogResult == DialogResult.OK)
			{
				Filename = saveFileDialog.FileName;
			}
			return Filename;


		}
		private void UpdateInvHeader(int id)
		{
			//objInv.updateEditedField(txtIDCUST.Text, dtp_invdate.Value, dtpAsofdate.Value, dtp_invduedate.Value, dd, txtinvno.Text);
			//dgv_student.DataSource = std_val_tb.Select("AccpacFlag in ('1','2')");
			//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";

			using (SqlConnection conn = new SqlConnection(connectionstring))
			{
				using (SqlCommand command = new SqlCommand("UPDATE [XADEFH]  SET [VALFLAG]= 1 WHERE [DEFIDH]= "+id+"", conn))
				{
					try
					{
						conn.Open();
						command.ExecuteNonQuery();						
						command.CommandTimeout = 3000;						
						//MessageBox.Show("Successfully update.");
					}
					catch (Exception ex)
					{
						// Handle exception properly
					}
					finally
					{
						conn.Close();
					}
				}
			}
			//this.Close();
		}
		public void InsertDetailLn(int id,string GACCTID, string DEFACCTID, decimal GJRNLDT,decimal GFUNCTAMT,string FISYEAR,string FISPRD,string fromDate,string Todate)
		{
			try
			{
				System.Data.SqlClient.SqlConnection conn;
				System.Data.SqlClient.SqlCommand cmd;
				//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";
				conn = new SqlConnection(connectionstring);
				conn.Open();
				 cmd = new SqlCommand("insert into XADEFD (DEFIDH,GACCTID,DEFACCTID,GJRNLDT,GFUNCTAMT,FISYEAR,FISPRD,FROMDATE,TODATE) values("+id+",'" + GACCTID + "', '" + DEFACCTID + "',"+GJRNLDT+", '" + GFUNCTAMT + "', '" + FISYEAR + "', '"+ FISPRD + "', '" + fromDate + "', '" + Todate + "')", conn);
				cmd.ExecuteNonQuery();
				//MessageBox.Show("Data Inserted Successfully.");
				conn.Close();

			}
			catch (Exception ex)
			{
				//obLOg.WriteLog1("update student table Flag Exception=" + ex.Message, "STUDENT");
			}
		}
		private void cmbFiscYr_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillChkPeriod(cmbFiscYr.Text, SageDBName);
		}
		public Boolean FillChkPeriod(string Years, string companyid)
		{
			Boolean strreturn = false;
			try
			{
				string CRED = SageUserName + ":" + SagePassword;
				var authenticationBytes = Encoding.ASCII.GetBytes(CRED);
				using (HttpClient confClient = new HttpClient())
				{
					confClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
						   Convert.ToBase64String(authenticationBytes));
					confClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//172.16.21.14
					HttpResponseMessage message = confClient.GetAsync("http://localhost/Sage300WebApi/v1.0/-/" + companyid + "/CS/CSFiscalCalendars('" + Years + "')").Result;
					if (message.IsSuccessStatusCode)
					{
						//obLOg.WriteLog1("FiscalPrd  validated  ", "RECIEPT");
						dynamic Response = message.Content.ReadAsStringAsync();
						dynamic deserialized = JsonConvert.DeserializeObject(Response.Result.ToString());
						endFslyear = (Convert.ToDateTime(deserialized.FiscalPeriod12EndDate.ToString()));
						startPrdMonth = (Convert.ToDateTime(deserialized.FiscalPeriod1StartDate.ToString()));
						cmbFisPerFrom.Items.Insert(0, "01-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod1StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(1, "02-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod2StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(2, "03-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod3StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(3, "04-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod4StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(4, "05-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod5StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(5, "06-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod6StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(6, "07-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod7StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(7, "08-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod8StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(8, "09-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod9StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(9, "10-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod10StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(10, "11-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod11StartDate.ToString()).Month));
						cmbFisPerFrom.Items.Insert(11, "12-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod12StartDate.ToString()).Month));

						Cmb_data_dt.Rows.Add("01", Convert.ToDateTime(deserialized.FiscalPeriod1StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("02", Convert.ToDateTime(deserialized.FiscalPeriod2StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("03", Convert.ToDateTime(deserialized.FiscalPeriod3StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("04", Convert.ToDateTime(deserialized.FiscalPeriod4StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("05", Convert.ToDateTime(deserialized.FiscalPeriod5StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("06", Convert.ToDateTime(deserialized.FiscalPeriod6StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("07", Convert.ToDateTime(deserialized.FiscalPeriod7StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("08", Convert.ToDateTime(deserialized.FiscalPeriod8StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("09", Convert.ToDateTime(deserialized.FiscalPeriod9StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("10", Convert.ToDateTime(deserialized.FiscalPeriod10StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("11", Convert.ToDateTime(deserialized.FiscalPeriod11StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("12", Convert.ToDateTime(deserialized.FiscalPeriod12StartDate.ToString()).Month);
						Cmb_data_dt.Rows.Add("13", Convert.ToDateTime(deserialized.FiscalPeriod12EndDate.ToString()).Month);

						cmbFisPerTo.Items.Insert(0, "12-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod12StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(1, "11-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod11StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(2, "10-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod10StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(3, "09-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod9StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(4, "08-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod8StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(5, "07-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod7StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(6, "06-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod6StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(7, "05-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod5StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(8, "04-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod4StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(9, "03-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod3StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(10, "02-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod2StartDate.ToString()).Month));
						cmbFisPerTo.Items.Insert(11, "01-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToDateTime(deserialized.FiscalPeriod1StartDate.ToString()).Month));

						}
					//DataTable dd=  cmbFisPerTo.
				}
			}
			catch (Exception ex)
			{
				//obLOg.WriteLog1("FiscalPrd  exception   " + ex.Message, "RECIEPT");
			}

			return strreturn;
		}
		public void FillFailedRecord()
		{
			System.Data.SqlClient.SqlConnection conn;
			System.Data.SqlClient.SqlCommand cmd;
			////string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
			//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";

			conn = new System.Data.SqlClient.SqlConnection(connectionstring);
			conn.Open();
			string Querystring = "select DEFIDH,GFACCTID,DEFACCTID,DEFMODE,GFISYR,GFISPRD,convert(varchar, fromdate, 23) fromdate,convert(varchar, todate, 23) todate,GBATCHNO,GENTNO,GPOTDETNO,GJRNLDT,GFUNCTAMT,INVNO,INVDATE, " +
								 //" BILLTO,DLINENO,ITEM,ITEMDESC,INVNIQ from XADEFH where ValFlag = 0 and INVDATE BETWEEN '"+dtp_fromDate.Value+ "' and '" + dtp_todate.Value + "'";
			" BILLTO,DLINENO,ITEM,ITEMDESC,INVNIQ from XADEFH where ValFlag = 0 and INVDATE >='" + dtp_fromDate.Text.Replace("-","") + "' and INVDATE <='" + dtp_todate.Text.Replace("-", "") + "'";
			cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
			cmd.CommandTimeout = 180;
			cmd.CommandType = CommandType.Text;
			cmd.ExecuteNonQuery();
			System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(cmd);
			//cmd.Connection = conn;
			sda.SelectCommand = cmd;
			DataTable def_Exl_Down;
			using (def_Exl_Down = new DataTable())
			{
				sda.Fill(def_Exl_Down);
			}
			if(def_Exl_Down.Rows.Count==0)
			{ MessageBox.Show("Data not found!"); return; }
			dgv_editfield.AutoGenerateColumns = false;
			dgv_editfield.DataSource = def_Exl_Down;
			
		}
		int click = 0;
		DateTimePicker dtpFrom;
		DateTimePicker dtpTo;
		private void dgv_editfield_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			// determine if click was on our date column

			if (e.RowIndex < 0)
				return;

			// determine if click was on our date column
			//if (dgv_editfield.CurrentCell.Value.ToString() != "")
			//{

			if (dgv_editfield.Columns[e.ColumnIndex].DataPropertyName == "DEFMODE")
			{
				DataGridViewComboBoxCell mode = new DataGridViewComboBoxCell();

				mode.Items.Add("YRLY");
				mode.Items.Add("MNTHLY");
				dgv_editfield[e.ColumnIndex, e.RowIndex] = mode;
			}
				if (dgv_editfield.Columns[e.ColumnIndex].DataPropertyName == "fromdate")
				{
					if (click > 0)
						dtpTo.Visible = false;
					// initialize DateTimePicker
					dtpFrom = new DateTimePicker();
					dtpFrom.Format = DateTimePickerFormat.Custom;
					dtpFrom.CustomFormat = "yyyy-MM-dd";
					//dtpFrom.MaxDate = DateTime.Now;
					dtpFrom.Visible = true;



					// set size and location
					var rect = dgv_editfield.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
					dtpFrom.Size = new Size(rect.Width, rect.Height);
					dtpFrom.Location = new Point(rect.X, rect.Y);
				    dtpFrom.Text = dgv_editfield.CurrentCell.Value.ToString();
				// attach events
				    dtpFrom.CloseUp += new EventHandler(dtpFrom_CloseUp);
					dtpFrom.TextChanged += new EventHandler(dtpFrom_OnTextChange);
					dtpFrom.Leave += new EventHandler(dtpFrom_OnTextChange);

					dgv_editfield.Controls.Add(dtpFrom);
					click = click + 1;
					//dtpFrom.Value = DateTime.Parse(dataGridView1.CurrentCell.Value.ToString());
					//fromdt = DateTime.Parse(dataGridView1.CurrentCell.Value.ToString());
				}

				if (dgv_editfield.Columns[e.ColumnIndex].DataPropertyName == "Todate")
				{
					// initialize DateTimePicker

					if (click > 0)
						dtpFrom.Visible = false;
					dtpTo = new DateTimePicker();
					dtpTo.Format = DateTimePickerFormat.Custom;
					dtpTo.CustomFormat = "yyyy-MM-dd";
					//dtpTo.MaxDate = DateTime.Now;
					dtpTo.Visible = true;

					// set size and location
					var rect = dgv_editfield.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
					dtpTo.Size = new Size(rect.Width, rect.Height);
					dtpTo.Location = new Point(rect.X, rect.Y);
				    dtpTo.Text =dgv_editfield.CurrentCell.Value.ToString();
					
				// attach events
				    dtpTo.CloseUp += new EventHandler(dtpto_CloseUp);
					dtpTo.TextChanged += new EventHandler(dtpto_OnTextChange);
					dtpTo.Leave += new EventHandler(dtpto_OnTextChange);
				    
					dgv_editfield.Controls.Add(dtpTo);
					click = click + 1;
					//dtpTo.Value = DateTime.Parse(dataGridView1.CurrentCell.Value.ToString());
					//todt = DateTime.Parse(dataGridView1.CurrentCell.Value.ToString());
				}
				
			//}
		}
		// on text change of dtp, assign back to cell
		private void dtp_OnTextChange(object sender, EventArgs e)
		{
			dgv_editfield.CurrentCell.Value = dtpTo.Text.ToString();
			//dtpTo.Visible = false;
			DateTime dtTo = Convert.ToDateTime(dgv_editfield.CurrentRow.Cells["todate"].Value);
			DateTime DtFrom = Convert.ToDateTime(dgv_editfield.CurrentRow.Cells["fromDate"].Value);
			if (dtTo < DtFrom)
			{
				MessageBox.Show("To Date should be greater than From Date.");
				//return;
				dtpTo.Visible = true;

			}
			else
			{ //dtpTo.Visible = false;
			}
		}

		// on close of cell, hide dtp
		void dtp_CloseUp(object sender, EventArgs e)
		{
			dtpTo.Visible = false;
			click = 0;
		}

		// on text change of dtp, assign back to cell
		private void dtpFrom_OnTextChange(object sender, EventArgs e)
		{
			string ddd = dtpFrom.Text;
			dgv_editfield.CurrentCell.Value = ddd;
			//dtpFrom.Visible = false;
		}
		//Todate Picker
		// on close of cell, hide dtp
		void dtpFrom_CloseUp(object sender, EventArgs e)
		{
			dtpFrom.Visible = false;
			click = 0;
		}

		// on text change of dtp, assign back to cell
		private void dtpto_OnTextChange(object sender, EventArgs e)
		{
			dgv_editfield.CurrentCell.Value = dtpTo.Text.ToString();
			//dtpTo.Visible = false;
			DateTime dtTo = Convert.ToDateTime(dgv_editfield.CurrentRow.Cells["todate"].Value);
			DateTime DtFrom = Convert.ToDateTime(dgv_editfield.CurrentRow.Cells["fromDate"].Value);
			if (dtTo < DtFrom)
			{
				MessageBox.Show("To Date should be greater than From Date.");
				//return;
				dtpTo.Visible = true;

			}
			else
			{ //dtpTo.Visible = false;
			}
		}

		// on close of cell, hide dtp
		void dtpto_CloseUp(object sender, EventArgs e)
		{
			dtpTo.Visible = false;
			click = 0;
		}

		private void btnInvUpdate_Click(object sender, EventArgs e)
		{
			//objInv.updateEditedField(txtIDCUST.Text, dtp_invdate.Value, dtpAsofdate.Value, dtp_invduedate.Value, dd, txtinvno.Text);
			//dgv_student.DataSource = std_val_tb.Select("AccpacFlag in ('1','2')");
			//string connectionstring = "Data Source=.; Initial Catalog=GSTMAS; User ID=sa; Password=Erp#12345;";

			DataTable dt = new DataTable();
			dt = (DataTable)dgv_editfield.DataSource;
			if (dt.Rows.Count <= 0)
			{
				MessageBox.Show("Data not found..");
				return;
			}
			foreach (DataRow dr in dt.Rows)
			{
				if (Convert.ToDateTime(dr["TODATE"]) < Convert.ToDateTime(dr["FROMDATE"]))
				{
					MessageBox.Show("To date is less than From Date.");
					return;
				}
			}
			DataTable dtSelectedColumns = dt.DefaultView.ToTable(true, "DEFIDH", "FROMDATE", "TODATE", "DEFMODE");
			using (SqlConnection conn = new SqlConnection(connectionstring))
			{
				using (SqlCommand command = new SqlCommand("CREATE TABLE #TmpTable([DEFIDH] int ,[FROMDATE] varchar(10),[TODATE] varchar(10),[DEFMODE] varchar(10))", conn))
				{
					try
					{
						conn.Open();
						command.ExecuteNonQuery();

						using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
						{
							bulkcopy.BulkCopyTimeout = 6600;
							bulkcopy.DestinationTableName = "#TmpTable";
							bulkcopy.WriteToServer(dtSelectedColumns);
							bulkcopy.Close();
						}
						command.CommandTimeout = 3000;
						command.CommandText = "UPDATE P SET P.[FROMDATE]= T.[FROMDATE],P.[TODATE]= T.[TODATE],P.[DEFMODE]= T.[DEFMODE],P.[VALFLAG]= 2 FROM [XADEFH] AS P INNER JOIN #TmpTable AS T ON P.[DEFIDH] = T.[DEFIDH]  ;DROP TABLE #TmpTable;";
						command.ExecuteNonQuery();
						MessageBox.Show("Successfully update.");
					}
					catch (Exception ex)
					{
						// Handle exception properly
					}
					finally
					{
						conn.Close();
					}
				}
			}
			//this.Close();
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string dd = tabControl1.SelectedIndex.ToString();
			if (dd == "0")   //campus
			{
			}
			else if (dd == "1")   //campus
			{
			}
			else if (dd == "2")   //campus
			{
				//FillFailedRecord();
			}
		}

		private void btnEditHeaderOK_Click(object sender, EventArgs e)
		{
			FillFailedRecord();
		}
	}
}
