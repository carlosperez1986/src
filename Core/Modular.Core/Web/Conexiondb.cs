using System;
namespace Modular.Core.Web
{
    public static class Conexiondb
    {
        public static string ConexiondbString()
        {

            //string cnn = "Data Source=82.223.108.84,10606;Initial Catalog=shopcartdb;User ID=sa;Password=Bp9Ea51VGI;Packet Size=4096;MultipleActiveResultSets=True;Application Name=EntityFramework";
            string cnn = "Data Source=localhost,1433;Initial Catalog=shopcartdb;User ID=sa;Password=Carloselias23.;Packet Size=4096;MultipleActiveResultSets=True;Application Name=EntityFramework";

            return cnn;

        }
    }
}
