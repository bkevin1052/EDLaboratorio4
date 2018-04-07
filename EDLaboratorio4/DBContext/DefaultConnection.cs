using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDLaboratorio4.Models;

namespace EDLaboratorio4.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();

        public static Dictionary<string, Pais> Album = new Dictionary<string, Pais>();

        public static Dictionary<string, int> EstadoCalcomanias = new Dictionary<string, int>();

        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}