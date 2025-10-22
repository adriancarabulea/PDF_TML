using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
//using System.Threading;

namespace PDF_TML
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>



    

        public static int a = 0;
            [STAThread]
         static void Main()
        {
  


            try
            {
                string IniPath = Application.StartupPath + @"\" + Properties.Settings.Default.Ini_TML;
                string Colors = Ini.ReadValue("PDF_PARAMETERS", "Colors", IniPath);
                string Files = Ini.ReadValue("PDF_PARAMETERS", "Files", IniPath);
                string Pdf_Viewer = Ini.ReadValue("PDF_PARAMETERS", "PDF_Viewer", IniPath);
                string HasColorDrawings = Ini.ReadValue("PDF_PARAMETERS", "HasColorDrawings", IniPath);


                if (HasColorDrawings == "1")
                {
                //string Temp_path = "C:\\SIGMA\\documents";
                string Temp_path = Ini.ReadValue("PDF_PARAMETERS", "Temporary path", IniPath);
                //string Path_PDF_Viewer = Ini.ReadValue("PDF_PARAMETERS", "Sigma_PDF_Viewer path", IniPath);


    
                    string[] TMLColors_Arr = Colors.Split('#');
                    string[] TML_Arr = null;
                    string[] Colors_Arr = null;
                    string strTML = string.Empty;
                    string strColor = string.Empty;

                    string[] File_Arr = Files.Split('#');
                    string File = string.Empty;




                    for (int k1 = 0; k1 < TMLColors_Arr.Length; k1++)
                    {


                        if (k1 % 2 == 0)
                        {
                            if (strTML == string.Empty)
                            {
                                strTML = TMLColors_Arr[k1].ToString();
                            }
                            else
                            {
                                strTML = strTML + "#" + TMLColors_Arr[k1].ToString();
                            }
                        }

                        if (k1 % 2 == 1)
                        {
                            if (strColor == string.Empty)
                            {
                                strColor = TMLColors_Arr[k1].ToString();
                            }
                            else
                            {
                                strColor = strColor + "#" + TMLColors_Arr[k1].ToString();
                            }
                        }


                    }





                    TML_Arr = strTML.Split('#');
                    Colors_Arr = strColor.Split('#');

          

                    
//#region v1 Scan_File_1
                    //Scan_File_1(Temp_path, File_Arr, TML_Arr, Colors_Arr);

                    //for (int k2 = 0; k2 < File_Arr.Length; k2++)
                    //{

                    //    string Temp_file = Temp_path + @"\" + Path.GetFileNameWithoutExtension(File_Arr[k2].ToString()) + "_temp" + Path.GetExtension(File_Arr[k2].ToString());



                    //    //CustomPDF.RemovePDF_annotation(File_Arr[k2].ToString(), Temp_file);


                    //    for (int k3 = 0; k3 < TML_Arr.Length; k3++)
                    //    {
                    //        //MessageBox.Show("TML=" + TML_Arr[k2].ToString() + "|Color=" + Colors_Arr[k2].ToString());
                    //        strTML = TML_Arr[k3].ToString();
                    //        strColor = Colors_Arr[k3].ToString();
                    //        string var = CustomPDF.GetText(File_Arr[k2].ToString(), "", strTML);
                    //        if (var == strTML)
                    //        {
                    //            CustomPDF.HightLight_PDF(File_Arr[k2].ToString(), Temp_file, strTML, strColor);
                    //        }
                    //    }
                    //}
//#endregion    


               #region v1 Scan_File_2
                    Scan_File_2(Temp_path, File_Arr, TML_Arr, Colors_Arr);
               #endregion




                    TMLColors_Arr = null;
                    TML_Arr = null;
                    Colors_Arr = null;
                }
 
                //MessageBox.Show("Ready marked TML's!", "PDF_TML.Mark TML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Pdf_Viewer == "1")
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + @"\Sigma_PDF_Viewer\Sigma1_pdfViewer.exe");
                }
                

            } 
            catch(Exception ex)
            {
                if (ex.InnerException == null)
                {
                    MessageBox.Show("Main." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }

        }


        public static void Scan_File_1(string Temp_path, string[] File_Arr, string[] TML_Arr, string[] Colors_Arr)
        {
            for (int k2 = 0; k2 < File_Arr.Length; k2++)
            {

                
                string Temp_file = Temp_path + @"\" + Path.GetFileNameWithoutExtension(File_Arr[k2].ToString()) + "_temp" + Path.GetExtension(File_Arr[k2].ToString());




                //CustomPDF.RemovePDF_annotation(File_Arr[k2].ToString(), Temp_file);


                for (int k3 = 0; k3 < TML_Arr.Length; k3++)
                {
                    //MessageBox.Show("TML=" + TML_Arr[k2].ToString() + "|Color=" + Colors_Arr[k2].ToString());
                    string strTML = TML_Arr[k3].ToString();
                    string strColor = Colors_Arr[k3].ToString();
                    string var = CustomPDF.GetText(File_Arr[k2].ToString(), "", strTML);
                    if (var == strTML)
                    {
                        CustomPDF.HightLight_PDF(File_Arr[k2].ToString(), Temp_file, strTML, strColor);
                    }
                }
            }
                    

        }



        public static void Scan_File_2(string Temp_path, string[] File_Arr, string[] TML_Arr, string[] Colors_Arr)
        {
            for (int k2 = 0; k2 < File_Arr.Length; k2++)
            {



                string iniFile = File_Arr[k2].ToString();
                string Temp_file = Temp_path + @"\" + Path.GetFileNameWithoutExtension(File_Arr[k2].ToString()) + "_temp" + Path.GetExtension(File_Arr[k2].ToString());

                 CustomPDF.HightLight_PDF(iniFile,Temp_file,TML_Arr,Colors_Arr);
  
            }


        }

  
    }
}
