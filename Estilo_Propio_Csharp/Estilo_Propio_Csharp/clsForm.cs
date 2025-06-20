using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estilo_Propio_Csharp
{
    public partial class ClsForm
    {
        //private string mConnect;
        //private string mConnectVB6;
        //private string mConnectOracle;

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int PutFocus(int hwnd);

        public string Cod_Empresa
        {
            get { return VariablesGenerales.pCodEmpresa; }
            set { VariablesGenerales.pCodEmpresa = value; }
        }
        public string UserName
        {
            get { return VariablesGenerales.pUsuario; }
            set { VariablesGenerales.pUsuario = value; }
        }

        public string Cod_Perfil
        {
            get { return VariablesGenerales.pCodPerfil; }
            set { VariablesGenerales.pCodPerfil = value; }
        }

        public string Rutas
        {
            get { return VariablesGenerales.pRuta; }
            set { VariablesGenerales.pRuta = value; }
        }
        public string Cod_opcion { get; set; }

        public string ConnectEmpresa
        {
            get { return VariablesGenerales.pConnect; }
            set { VariablesGenerales.pConnect = value; }
        }
        public string ConnectSeguridad
        {
            get { return VariablesGenerales.pConnectSeguridad; }
            set { VariablesGenerales.pConnectSeguridad = value; }
        }
        public string ConnectVB60
        {
            get { return VariablesGenerales.pConnectVB6; }
            set { VariablesGenerales.pConnectVB6 = value; }
        }
        public string ConnectSeguridadVB60 { get; set; }
        public string ConnectOracle { get; set; }
        public string Language { get; set; }
        public string AnoPeriodoContableVigente { get; set; }

        public object GetForm(ref object sFormName)
        {

            sFormName = sFormName.ToString().ToUpper();

            object MyForm = new object();

            switch ((string)sFormName)
            {
                case "MENUPRINCIPAL":
                    MyForm = new MenuPrincipal();
                    break;

                case "FRMBANDEJASEGUIMIENTOCOTIZACIONPRECIOSTELA":
                    MyForm = new FrmBandejaBibliotecaGraficos();
                    break;

                case "FRMBANDEJASOLICITUDAPLICACIONESBDESTF":
                    MyForm = new FrmBandejaBibliotecaGraficos();
                    break;

                case "FRMBANDEJABIBLIOTECAGRAFICOS":
                    MyForm = new FrmBandejaBibliotecaGraficos();
                    break;                   

                case "FRMBANDEJABIBLIOTECAGRAFICOS_MAN":
                    MyForm = new FrmBandejaBibliotecaGraficos_MAN();
                    break;

                case "FRMBANDEJABIBLIOTECAFORMAMEDIR":
                    MyForm = new FrmBandejaBibliotecaFormaMedir();
                    break;

                case "FRMBANDEJABIBLIOTECAFORMAMEDIR_MAN":
                    MyForm = new FrmBandejaBibliotecaFormaMedir_Man();
                    break;

                case "FRMCARGAUPCDESDEEXCEL":
                    MyForm = new FrmCargaUPCdesdeExcel();
                    break;
            }
            return MyForm;
        }

        public int OpenForm(string strFormName, int lngParentHwnd)
        {
            object x = strFormName;

            this.GetForm(ref x);

            return 0;
        }

    }
}
