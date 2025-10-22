using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Drawing.Imaging;
using Org.BouncyCastle.Asn1.Cms;



namespace PDF_TML
{
    class CustomPDF
    {
       public static float Left = 0F;
       public static float Botton = 0F;
       public static float Right = 0F;
       public static float Top = 0F;
       public static int varPage = 1;


        public static void HightLight_PDF(string IniFile, string TempFile, string strText, string Color)
        {
            try
            {

                PdfReader reader = new PdfReader(IniFile);
               



                ReadRectangle(IniFile, strText.Trim());
                //Document doc = new Document();
                //FileStream fs = new FileStream(TempFile, FileMode.Create);

                //doc.Open();
                //PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                

                //using (FileStream fs = new FileStream(TempFile, FileMode.Create, FileAccess.Write, FileShare.None))
                //{


                using (FileStream fs = new FileStream(TempFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (PdfStamper stamper = new PdfStamper(reader, fs))
                    {
                       
                        
                       

                        //v1
                        //iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(Left, Botton, Right, Top);
                        ////Create an array of quad points based on that rectangle. NOTE: The order below doesn't appear to match the actual spec but is what Acrobat produces
                        //float[] quad = { rect.Left, rect.Bottom, rect.Right, rect.Bottom, rect.Left, rect.Top, rect.Right, rect.Top };
                        //PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                        //highlight.Color = GetColor(Color);
                        //stamper.AddAnnotation(highlight, varPage);
                        //stamper.Close();
                        //v1



                        //v2
                        //Rectangle r = reader.GetPageSize(varPage);
                        //PdfContentByte cb = stamper.GetOverContent(varPage);
                        //string strChunk = strText;
                        //Chunk textAsChunk = new Chunk(strChunk);
                        //textAsChunk.SetBackground(GetColor(Color));
                        //BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                        //Font times = new Font(bfTimes, 10);
                        //textAsChunk.Font = times;

                        //ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, new Phrase(textAsChunk), r.Height - Botton, Left + 2.2F, 0);


                        //stamper.Close();
                        //reader.Close();
                        //v2



                        //25.02.2021
                        //#region x
                        //string var = CustomPDF.GetText(IniFile, "", strText);

                        string text = PdfTextExtractor.GetTextFromPage(reader, 1, new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy());

                        if (text.IndexOf(strText) < 0)
                        {
                            stamper.Close();
                            fs.Close();
                            reader.Close();

                            fs.Dispose();

                            return;

                        }
                        //#endregion

                        //v3
                        Rectangle r = reader.GetPageSize(varPage);
                        PdfContentByte cb = stamper.GetOverContent(varPage);
                        string strChunk = strText;
                        Chunk textAsChunk = new Chunk(strChunk);
                        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                        Font times = new Font(bfTimes, 10);
                        textAsChunk.Font = times;
                        PdfPTable table = new PdfPTable(1);

                        table.TotalWidth = Botton - Top + 5F;
                        PdfPCell myCell = new PdfPCell(new Phrase(textAsChunk));
                        myCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        myCell.VerticalAlignment = Element.ALIGN_CENTER;
                        myCell.BackgroundColor = GetColor(Color);
                        myCell.Border = PdfPCell.NO_BORDER;


                        myCell.PaddingTop = 0;
                        myCell.PaddingBottom = 0;

                        myCell.FixedHeight = 14f;
                        table.AddCell(myCell);

                        table.WriteSelectedRows(0, -1, r.Height - Botton - 4F, Left + 15F, cb);



                        stamper.Close();
                        reader.Close();


                        //v3

                    }


                }
           
               

      



  

                //v1
                //if (File.Exists(TempFile))
                //{
                //    File.Delete(IniFile);
                //    File.Move(TempFile, IniFile);
                //}


                //v2
                    if (File.Exists(TempFile))
                    {
                        File.Copy(TempFile, IniFile, true);
                        File.Delete(TempFile);
                    }



            }
            catch(Exception ex)
            {
                MessageBox.Show("HightLight_PDF." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        public static void HightLight_PDF(string IniFile, string TempFile, string[]TML, string[] Color)
        {
            string strText = string.Empty;

            try
            {


                DateTime Date_ini = DateTime.Now;

                using (FileStream fs = new FileStream(TempFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {

                    

                    PdfReader reader = new PdfReader(IniFile);
                    using (PdfStamper stamper = new PdfStamper(reader, fs))
                    {

                        for (int idx = 0; idx < TML.Length; idx++)
                        {


                            strText = TML[idx];
                            ReadRectangle(IniFile, strText.Trim());


                            if(CustomPDF.Left != 0 || CustomPDF.Right != 0 || CustomPDF.Top != 0 || CustomPDF.Botton != 0)
                            {

                                #region (a)
                                //string text = PdfTextExtractor.GetTextFromPage(reader, 1, new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy());
                                //if (text.IndexOf(strText) < 0)
                                //{
                                //    stamper.Close();
                                //    fs.Close();
                                //    reader.Close();

                                //    fs.Dispose();

                                //    return;

                                //}
                                #endregion






                                //v3
                                Rectangle r = reader.GetPageSize(varPage);
                                PdfContentByte cb = stamper.GetOverContent(varPage);
                                string strChunk = strText;
                                Chunk textAsChunk = new Chunk(strChunk);
                                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                                Font times = new Font(bfTimes, 10);
                                textAsChunk.Font = times;
                                PdfPTable table = new PdfPTable(1);

                                table.TotalWidth = Botton - Top + 5F;

                                if (table.TotalWidth > 0)
                                {
                                    PdfPCell myCell = new PdfPCell(new Phrase(textAsChunk));
                                    myCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                    myCell.VerticalAlignment = Element.ALIGN_CENTER;
                                    myCell.BackgroundColor = GetColor(Color[idx]);
                                    myCell.Border = PdfPCell.NO_BORDER;


                                    myCell.PaddingTop = 0;
                                    myCell.PaddingBottom = 0;

                                    myCell.FixedHeight = 14f;
                                    table.AddCell(myCell);


                                    float xpos = r.Height - Botton - 4F;
                                    float ypos = Left + 15F;

                                    //table.WriteSelectedRows(0, -1, r.Height - Botton - 4F, Left + 15F, cb);
                                    table.WriteSelectedRows(0, -1, xpos, ypos, cb);
                                }

                            }

                        }

                        //stamper.Close();
                       

                    }

                    reader.Close();
                }


                DateTime  Date_fin = DateTime.Now;
                TimeSpan var = (Date_fin - Date_ini);
                

                if (File.Exists(TempFile))
                {
                    File.Copy(TempFile, IniFile, true);
                    File.Delete(TempFile);
                }


                //MessageBox.Show("Gata!Duration=" + var.ToString() );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Highlight_PDF." + strText + ","+ ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public static BaseColor GetColor(string strColor)
        {

            BaseColor var_Col = BaseColor.WHITE;

          switch(strColor)
          {
            case  "lightTurquoise":
                  var_Col = BaseColor.CYAN;
            break;
              case "red":
            var_Col = BaseColor.RED;
            break;
              case "yellow":
            var_Col = BaseColor.YELLOW;
            break;
              case "Out of service":
            var_Col = BaseColor.LIGHT_GRAY;
            break;
          }

          return var_Col;

        }


        public static void ReadRectangle(string PathFile, string Text)
        {
            

            Left = 0;
            Botton = 0;
            Right = 0;
            Top = 0;

            var t = new MyLocationTextExtractionStrategy(Text);

            varPage = 1;

            //Parse page 1 of the document above
            using (var r = new PdfReader(PathFile))
            {

                for (int k = 1; k <= r.NumberOfPages; k++)
                {
                    var ex = PdfTextExtractor.GetTextFromPage(r, k, t);

                    string[] ex1 = ex.Split('\n');

                   

                    //if (Text == "100")
                    //{
                        if (ex1.Any(Text.Equals) == false)
                        {
                            int step = 0;
                            for (int i = 0; i < ex1.Count(); i++)
                            {
                            
                                string[] ex2 = ex1[i].Split(' ');

                                if (ex2.Any(Text.Equals) == true)
                                    step = step + 1;

                                if (step == 1) break;
                                
                            }

                            if (step == 0) return;
                        }
                    //}



                    if (ex != null)
                    {
                        varPage = k;
                        break;
                    }

                }
            }

            //Loop through each chunk found
            foreach (var p in t.myPoints)
            {


                Left = p.Rect.Left;
                Botton = p.Rect.Bottom;
                Right = p.Rect.Right;
                Top = p.Rect.Top;
            }


        }

        public static void ReadRectangle(PdfReader reader, string Text)
        {

            Left = 0;
            Botton = 0;
            Right = 0;
            Top = 0;

            var t = new MyLocationTextExtractionStrategy(Text);

            varPage = 1;

            //Parse page 1 of the document above

                for (int k = 1; k <= reader.NumberOfPages; k++)
                {
                    var ex = PdfTextExtractor.GetTextFromPage(reader, k, t);
                    if (ex != null)
                    {
                        varPage = k;
                        break;
                    }

                }
            

            //Loop through each chunk found
            foreach (var p in t.myPoints)
            {


                Left = p.Rect.Left;
                Botton = p.Rect.Bottom;
                Right = p.Rect.Right;
                Top = p.Rect.Top;
            }


        }


        public static void RemovePDF_annotation(string IniFile, string TempFile)
        {
            try
            {
                PdfReader reader = new PdfReader(IniFile);
                FileStream fs = new FileStream(TempFile, FileMode.Create, FileAccess.Write, FileShare.None);
                PdfStamper stamper = new PdfStamper(reader, fs);


                for (int k = 1; k <= reader.NumberOfPages; k++)
                {

                    PdfDictionary page = reader.GetPageN(k);

                    PdfArray annots = page.GetAsArray(PdfName.ANNOTS);


                    if (annots == null)
                    {
                        return;
                    }

                    int i = annots.ArrayList.Count;

                    while (i >= 1)
                    {

                        annots.Remove(i - 1);
                        i = i - 1;
                    }
                }


                stamper.Close();
                reader.Close();
                fs.Dispose();



                if (File.Exists(TempFile))
                {
                    File.Delete(IniFile);
                    File.Move(TempFile, IniFile);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("RemovePDF_annotation." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string GetText(string newFileNameWithImageAndText, string extractedTextFileName, string TextFind)
        {
            string var_output = string.Empty;
            using (Stream newpdfStream = new FileStream(newFileNameWithImageAndText, FileMode.Open, FileAccess.ReadWrite))
            {
                PdfReader pdfReader = new PdfReader(newpdfStream);
                string text = PdfTextExtractor.GetTextFromPage(pdfReader, 1, new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy());
                //text = text.Replace("\n", "");


                if (text.IndexOf(TextFind) >= 0)
                {
                    var_output = TextFind;
                }

                pdfReader.Close();
                newpdfStream.Close();
            }

            return var_output;
        }

    }
}
