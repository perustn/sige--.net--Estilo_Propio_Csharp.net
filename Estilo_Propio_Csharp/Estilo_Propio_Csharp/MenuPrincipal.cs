using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estilo_Propio_Csharp
{
    public partial class MenuPrincipal : ProyectoBase.frmBase
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            FrmBandejaControlFTPublicaciones FRM = new FrmBandejaControlFTPublicaciones();
            FRM.ShowDialog();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            FrmBandejaBibliotecaFormaMedir FRM = new FrmBandejaBibliotecaFormaMedir();
            FRM.ShowDialog();
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            FrmEstiloVersionMadreDependencia FRM = new FrmEstiloVersionMadreDependencia();
            FRM.ShowDialog();
        }
    }
}
