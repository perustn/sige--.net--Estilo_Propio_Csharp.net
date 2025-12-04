using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Janus.Windows.GridEX;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using NPOI.SS.UserModel;




namespace Estilo_Propio_Csharp
{
	class ClsHelper
	{
		public bool EjecutarOperacion(string strSQL)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(VariablesGenerales.pConnect))
				{
					SqlCommand bd = new SqlCommand(strSQL, conn);
					conn.Open();
					bd.ExecuteNonQuery();
				}
				return true;
			}
			catch (SqlException xSQLErr)
			{
				MessageBox.Show(xSQLErr.Message, "SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception xErr)
			{
				MessageBox.Show(xErr.Message, "SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return false;
		}
	

		public DataTable DevuelveDatos(string strSQL, string CCONNECT)
		{
			SqlDataAdapter oDa = new SqlDataAdapter();
			SqlConnection oCn = new SqlConnection();
			DataTable oDt = new DataTable("HPdata");
			try
			{

				oCn = new SqlConnection(CCONNECT);
				oDa = new SqlDataAdapter(strSQL, CCONNECT);

				oDa.Fill(oDt);
				return oDt;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		public object DevuelveDato(string strSQL, string CCONNECT, bool bolSWMostrarMsgDeError = true, string strErrSQL = "")
		{
			SqlConnection oCn = null/* TODO Change to default(_) if this is not a reference type */;
			SqlCommand oCmd = new SqlCommand();
			object dato;
			try
			{
				oCn = new SqlConnection(CCONNECT);
				oCn.Open();
				oCmd = new SqlCommand();
				{
					var withBlock = oCmd;
					withBlock.Connection = oCn;
					withBlock.CommandType = CommandType.Text;
					withBlock.CommandText = strSQL;
					dato = withBlock.ExecuteScalar();
				}
			}
			catch (SqlException xSQLErr)
			{
				dato = null;
				strErrSQL = xSQLErr.Message;
				if (bolSWMostrarMsgDeError)
					MessageBox.Show(xSQLErr.Message);
			}
			catch (Exception xErr)
			{
				MessageBox.Show(xErr.Message);
				dato = null;
			}
			finally
			{
				oCmd.Dispose();
				if (oCn.State == ConnectionState.Open)
					oCn.Close();
				oCn.Dispose();
			}
			return dato;
		}

		public DataTable EjecutaOperacionRetornaDatos2(string strSQL, string CCONNECT)
		{
			SqlDataAdapter oDa;
			SqlConnection oCn;
			try
			{
				oCn = new SqlConnection(CCONNECT);
				oDa = new SqlDataAdapter(strSQL, CCONNECT);
				DataTable oDt = new DataTable("HPdata");
				oDa.Fill(oDt);
				//EjecutaOperacionRetornaDatos2 = oDt;
				return oDt;
			}
			catch (Exception xErr)
			{
				MessageBox.Show(xErr.Message);
				return null;
			}
		}

		public void CheckLayoutGridEx(GridEX poGrid,
										string strDataTable = "",
										string strJanusTable = "")
		{
			DataSet oDataSet = new DataSet();
			DataTable oDataTable = new DataTable();
			GridEXTable oGridEXTable = new GridEXTable();
			Janus.Windows.GridEX.GridEXColumn oColumnGrid = new GridEXColumn();
			bool bFound;

			if (poGrid.DataSource == null || poGrid.RootTable == null)
			{
				return;
			}
			poGrid.CellToolTip = Janus.Windows.GridEX.CellToolTip.TruncatedText;
			poGrid.HideSelection = HideSelection.Highlight;


			if (poGrid.DataSource.GetType() == Type.GetType("DataSet"))
			{

				oDataSet = (DataSet)poGrid.DataSource;
				if (strDataTable.Length == 0)
				{
					oDataTable = oDataSet.Tables[0];
				}
				else
				{
					oDataTable = oDataSet.Tables[strDataTable];
				}
			}
			else
			{
				oDataTable = (DataTable)poGrid.DataSource;
			}


			if (strJanusTable.Length == 0)
			{
				oGridEXTable = poGrid.RootTable;
			}
			else
			{
				oGridEXTable = poGrid.Tables[strJanusTable];
			}

			foreach (DataColumn oDataColumn in oDataTable.Columns)
			{
				bFound = false;
				foreach (GridEXColumn oGridEXColum in oGridEXTable.Columns)
				{
					if (oGridEXColum.FormatString == "C")
					{
						oGridEXColum.FormatString = "";
						oGridEXColum.TextAlignment = TextAlignment.Far;
						oGridEXColum.Trimming = Trimming.NoTrimming;
					}
					if (oDataColumn.ColumnName.ToString().ToUpper() == oGridEXColum.Key.ToString().ToUpper())
					{
						bFound = true;
						break;
					}
				}
				if (!bFound)
				{
					oColumnGrid = new GridEXColumn(oDataColumn.Caption, ColumnType.Text);
					if (oColumnGrid.FormatString == "c")
					{
						oColumnGrid.FormatString = "";
						oColumnGrid.TextAlignment = TextAlignment.Far;
						oColumnGrid.Trimming = Trimming.NoTrimming;
					}
					oGridEXTable.Columns.Insert(oDataColumn.Ordinal, oColumnGrid);

				}
			} //Fin de loop oDataColumn
			poGrid.AlternatingColors = false;
			poGrid.VisualStyle = VisualStyle.Office2010;
			poGrid.OfficeColorScheme = OfficeColorScheme.Blue;

			oGridEXTable.TableHeaderFormatStyle = new GridEXFormatStyle();
			oGridEXTable.TableHeaderFormatStyle.TextAlignment = TextAlignment.Near;
			oGridEXTable.TableHeaderFormatStyle.FontName = "Segou Ui";
			oGridEXTable.TableHeaderFormatStyle.FontSize = 8;
			oGridEXTable.TableHeaderFormatStyle.ForeColor = Color.Blue;
			//oGridEXTable.TableHeaderFormatStyle.FontBold = ;


			oGridEXTable.PreviewRowFormatStyle = new GridEXFormatStyle();
			oGridEXTable.PreviewRowFormatStyle.ForeColor = Color.Blue;
			oGridEXTable.PreviewRowFormatStyle.FontItalic = TriState.False;
			oGridEXTable.PreviewRowFormatStyle.BackColor = Color.LightYellow;


			oGridEXTable.GroupRowFormatStyle = new GridEXFormatStyle();
			oGridEXTable.GroupRowFormatStyle.ForeColor = Color.Blue;
			oGridEXTable.GroupRowFormatStyle.FontName = "TAHOMA";
			oGridEXTable.GroupRowFormatStyle.FontSize = 8;
			oGridEXTable.GroupRowFormatStyle.FontBold = TriState.True;

			oGridEXTable.HeaderFormatStyle = new GridEXFormatStyle();
			oGridEXTable.HeaderFormatStyle.TextAlignment = TextAlignment.Center;
			oGridEXTable.HeaderFormatStyle.FontBold = TriState.True;
			oGridEXTable.HeaderFormatStyle.FontSize = 7;
			oGridEXTable.HeaderFormatStyle.FontName = "VERDANA";

			oGridEXTable.RowFormatStyle = new GridEXFormatStyle();
			oGridEXTable.RowFormatStyle.FontName = "tahoma";
			oGridEXTable.RowFormatStyle.FontSize = 8;

			oGridEXTable.TotalRowFormatStyle = new GridEXFormatStyle();
			oGridEXTable.TotalRowFormatStyle.FontName = "tahoma";
			oGridEXTable.TotalRowFormatStyle.FontSize = 8;
			//oGridEXTable.TotalRowFormatStyle.FontBold = TriState;

			foreach (GridEXColumn oGridEXColumn in oGridEXTable.Columns)
			{
				oGridEXColumn.Caption = oGridEXColumn.Caption.Trim().ToUpper();
			}
		}

		public DataRow ObtenerDr_DeGridEx(GridEX xGridEx)
		{
			try
			{
				if (xGridEx.DataSource as DataTable == null ||
				((DataTable)xGridEx.DataSource).Rows.Count == 0)
				{
					return null;
				}
				else
				{
					GridEXRow oDrGrx = xGridEx.GetRow();
					DataRowView oDrV = (DataRowView)oDrGrx.DataRow;
					DataRow oDr = null;
					if (oDrV != null) oDr = oDrV.Row;

					return oDr;
				}
			}
			catch (Exception xErr)
			{
				DataRow oDr = null;
				MessageBox.Show(xErr.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return oDr;
			}
		}

		public static void SoloNumeros(KeyPressEventArgs pE)
		{
			if (char.IsDigit(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else if (char.IsControl(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else
			{
				pE.Handled = true;
			}
		}

		public object GetFormDesdeOtroProyecto(string nameProy, string tipo, string nameForm)
		{
			try
			{
				string sPath = AppDomain.CurrentDomain.BaseDirectory;
				string sDllName = sPath + @"\" + nameProy + tipo;
                Assembly oDLL = Assembly.LoadFrom(sDllName);
				Type clsForm = oDLL.GetType(nameProy + ".clsForm");
				object objClase = Activator.CreateInstance(clsForm, true);

				((dynamic)objClase).Cod_Empresa = VariablesGenerales.pCodEmpresa;
				((dynamic)objClase).UserName = VariablesGenerales.pUsuario;
				((dynamic)objClase).Cod_Perfil = VariablesGenerales.pCodPerfil;
				((dynamic)objClase).Rutas = sPath;
				((dynamic)objClase).ConnectEmpresa = VariablesGenerales.pConnect;
				((dynamic)objClase).ConnectSeguridad = VariablesGenerales.pConnectSeguridad;
				((dynamic)objClase).ConnectVB60 = VariablesGenerales.pConnectVB6;


				MethodInfo getForm = clsForm.GetMethod("GetForm");
				object oForm = getForm.Invoke(objClase, new object[] { nameForm });
				return oForm;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public static void SoloLetrasONumeros(KeyPressEventArgs pE)
		{
			if (Char.IsLetter(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else if (Char.IsDigit(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else if (Char.IsControl(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else if (Char.IsSeparator(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else
			{
				pE.Handled = true;
			}
		}

		public static void SoloLetras(KeyPressEventArgs pE)
		{
			if (Char.IsLetter(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else if (Char.IsControl(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else if (Char.IsSeparator(pE.KeyChar))
			{
				pE.Handled = false;
			}
			else
			{
				pE.Handled = true;
			}
		}

		private void SoloDecimales(object sender, KeyPressEventArgs e, int cant)
		{
			var txt = (sender as TextBox).Text;
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			{
				e.Handled = true;
			}

			if ((e.KeyChar == '.') && (txt.IndexOf('.') > -1))
			{
				e.Handled = true;
			}

			if (txt.IndexOf('.') > -1)
			{
				var textAfterPoint = txt.Split('.')[1].Trim();
				if (textAfterPoint.Length > 1 && e.KeyChar != (char)Keys.Back)
				{
					e.Handled = true;
				}
			}
		}

		//public string RellenaDeCerosEnIzquierda(string strValor, int intCantidadDeCeros)
		//{
		//	return ("0", intCantidadDeCeros - strValor.Length) + strValor;
		//}

		public string EjecutarOperacionConOutput(string procedimiento, Dictionary<string, object> parametros, string nombreParametroOutput)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(VariablesGenerales.pConnect))
				{
					SqlCommand cmd = new SqlCommand(procedimiento, conn);
					cmd.CommandType = CommandType.StoredProcedure;

					// Agrega todos los parámetros
					foreach (var kvp in parametros)
					{
						cmd.Parameters.AddWithValue(kvp.Key, kvp.Value ?? DBNull.Value);
					}

					// Agrega el parámetro OUTPUT
					SqlParameter paramOut = new SqlParameter(nombreParametroOutput, SqlDbType.VarChar, 500);
					paramOut.Direction = ParameterDirection.Output;
					cmd.Parameters.Add(paramOut);

					conn.Open();
					cmd.ExecuteNonQuery();

					return paramOut.Value.ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}


		public class CellsRange
		{
			public int startRow { get; set; }
			public int startCol { get; set; }
			public int endRow { get; set; }
			public int endCol { get; set; }

			public string sheetName { get; set; }
		}

		public CellsRange getCellsInRange(IWorkbook workbook, string rangeName)
		{
			IName namedRange = workbook.GetName(rangeName);

			if (namedRange == null)
			{
				Console.WriteLine($"El rango con nombre '{rangeName}' no existe.");
				return null;
			}

			// Obtener los detalles del rango
			string sheetName = namedRange.SheetName;
			string reference = namedRange.RefersToFormula; // Ejemplo: "Sheet1!$A$1:$C$3"

			Console.WriteLine($"El rango '{rangeName}' está definido en la hoja '{sheetName}' con referencia '{reference}'");

			// Parsear la referencia para obtener los límites del rango

			string area = reference.Split('!')[1].Replace("$", ""); // Remover símbolos de dólar
			string[] parts = area.Split(':'); // Dividir por el rango (ej: "A1:C3")
			string startCell = parts[0];
			string endCell = startCell;

			if (parts.Length > 1)
			{
				endCell = parts[1];
			}

			// Convertir celdas a índices (Ejemplo: A1 -> fila 0, columna 0)
			CellsRange cells = new CellsRange();
			cells.sheetName = sheetName;
			cells.startRow = int.Parse(startCell.Substring(1)) - 1; // Fila de inicio
			cells.startCol = startCell[0] - 'A';                    // Columna de inicio
			cells.endRow = int.Parse(endCell.Substring(1)) - 1;     // Fila de fin
			cells.endCol = endCell[0] - 'A';                        // Columna de fin

			return cells;
		}

		public object GetCellValue(ICell cell)
		{
			if (cell == null)
			{
				return String.Empty;
			}

			switch (cell?.CellType)
			{
				case CellType.String:
					return cell.StringCellValue;
				case CellType.Numeric:
					if (DateUtil.IsCellDateFormatted(cell))
					{
						return cell.DateCellValue.ToString(); // Valor de fecha
					}
					return cell.NumericCellValue.ToString(); // Valor numérico
				case CellType.Boolean:
					return cell.BooleanCellValue.ToString();

				case CellType.Formula:

					// Manejar celdas con fórmulas según el tipo de resultado
					switch (cell.CachedFormulaResultType)
					{
						case CellType.String:
							return cell.StringCellValue; // Fórmula con resultado de texto
						case CellType.Numeric:
							if (DateUtil.IsCellDateFormatted(cell))
							{
								return cell.DateCellValue.ToString(); // Valor de fecha
							}
							return cell.NumericCellValue.ToString(); // Fórmula con resultado numérico
						case CellType.Boolean:
							return cell.BooleanCellValue.ToString(); // Fórmula con resultado booleano
						default:
							return "Tipo de fórmula desconocido";
					}
				case CellType.Blank:
					return String.Empty;

				case CellType.Error:
					return cell.ErrorCellValue.ToString();

				case CellType.Unknown:

				default:
					return cell?.ToString() ?? "";
			}
		}

		public string RellenaDeCerosEnIzquierda(string strValor, int intCantidadDeCeros)
		{
			return new string('0', intCantidadDeCeros - strValor.Length) + strValor;
		}

	}

}
