using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estilo_Propio_Csharp
{
    public class VariablesGenerales
    {
        //public static string pConnect = "Server=172.16.87.12; Database=SIGE_TSC; Uid=sa; Pwd=D8t8$46nt63$";
        //public static string pConnectSeguridad = "Server=172.16.87.12; Database=SEGURIDAD; Uid=sa; Pwd=D8t8$46nt63$";
        //public static string pConnectVB6 = ("Provider=SQLOLEDB.1; Password=D8t8$46nt63$; Persist Security Info=True; User ID=sa; Initial Catalog=SIGE_TSC; Data Source=172.16.87.12");
        public static string pConnect = "Server=192.168.30.22; Database=SIGE_STN_QA_5; Integrated Security=SSPI";
        public static string pConnectSeguridad = "Server=192.168.30.22; Database=SEGURIDAD; Integrated Security=SSPI";
        public static string pConnectVB6 = ("Provider=SQLOLEDB.1; Persist Security Info=True; Initial Catalog=SIGE_STN_QA_5; Data Source=192.168.30.22;Integrated Security=SSPI");
        public static string pCodEmpresa = "01";
        public static string pUsuario = "vluna";
        public static string pRuta = @"C:\_SIGEDebug";
        public static string pCodPerfil = "0001";
        
    }
}
