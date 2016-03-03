using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP4560_asgn5 {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            
            Matrix a = new Matrix(); //identity
            Matrix b = Matrix.translationMatrix(1, 2, 3);
            Matrix c = a * b;
            if (c == b)
                Console.WriteLine("matrix multiplication succeeded.");
            else
                Console.WriteLine("matrix multiplication failed.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Transformer());
        }
    }
}
