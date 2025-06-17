using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Estilo_Propio_Csharp
{
    public class ModGeneral
    {
		public void FondoDegradeDiagonalEnPanel(Panel objPanel, PaintEventArgs e, Color colEmpresa)
		{
			try
			{
				System.Drawing.Drawing2D.LinearGradientBrush Brocha;
				Graphics Superficie;
				Rectangle Rectangulo;
				Pen Lapiz;

				try
				{
					Superficie = e.Graphics;
					Lapiz = new Pen(Color.Azure, 1);
					Rectangulo = new Rectangle(0, 0, objPanel.Width, objPanel.Height);
					Brocha = new System.Drawing.Drawing2D.LinearGradientBrush(Rectangulo, Color.White, colEmpresa, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);


					Superficie.FillRectangle(Brocha, Rectangulo);
					Superficie.DrawRectangle(Lapiz, Rectangulo);
					Lapiz.Dispose();
					Superficie.Dispose();
				}
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
				catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
				{
				}
			}
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
			catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
			{
			}
		}

		public static string Mid(string strValue, int intBegin, int intLen)
		{
			string strMid = strValue.Substring(intBegin - 1, intLen);
			return strMid; //Output: 'to 
		}

		public string CompletaCodigo(string CodOrigen, int longcodfinal, int PosfinalCod)
		{
			// CodOrigen     = Es el codigo que sera pasado por parametro
			// LongCodFinal  = Es el tamaño del Codigo a devolver
			// PosFinalCod   = Es la posicion de la 1era parte del codigo

			string CompletaCodigos;
			int Contador;
			CompletaCodigos = CodOrigen.Substring(0, PosfinalCod).ToUpper(); // Strings.Mid(CodOrigen, 1, PosfinalCod).ToUpper();
			for (Contador = 1; Contador <= longcodfinal - CodOrigen.Length; Contador++)
				CompletaCodigos = CompletaCodigos + "0";
			if (CodOrigen.Length == 2)
				return CompletaCodigos;
			else
				return CompletaCodigos + CodOrigen.Substring(2, (CodOrigen.Length-PosfinalCod));
		}

		public static Double Val(string value)
		{
			String result = String.Empty;
			foreach (char c in value)
			{
				if (Char.IsNumber(c) || (c.Equals('.') && result.Count(x => x.Equals('.')) == 0))
					result += c;
				else if (!c.Equals(' '))
					return String.IsNullOrEmpty(result) ? 0 : Convert.ToDouble(result);
			}
			return String.IsNullOrEmpty(result) ? 0 : Convert.ToDouble(result);
		}
	}
}
