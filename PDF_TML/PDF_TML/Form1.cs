using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace PDF_TML
{
    public partial class Form1 : Form
    {
        
        //private float Left =0F;
        //private float Botton = 0F;
        //private float Right = 0F;
        //private float Top = 0F;
        //private int varPage = 1;
        //private int a = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "D:\\sigma project\\__Sigma_PDF_DWG";
            string file = "test.pDF";
            //string file_1 = "test_text_1.pDF";
            //v1
            //InsertTextToPdf(path + @"\" + file, path + @"\" + file_1);
            //v2
            ExtractTextFromPdf(path + @"\" + file, "");

            //v3
            //ExtractTextFromRegionOfPdf(path + @"\" + file);

            //v4
            //ChangeTextColorOfPdf(path + @"\" + file);


            //System.Diagnostics.Process.Start(path + @"\" + file);

            //return;
            
            
            //Document doc = new Document();
            //PdfWriter.GetInstance(doc, new FileStream(path + @"\" + file, FileMode.Open));

            //doc.Open();

            //Chunk chunk = new Chunk("Setting the Font", FontFactory.GetFont("dax-black"));

            //chunk.SetUnderline(0.5f, -1.5f);
          
            //doc.Close();

        }

        private static void InsertTextToPdf(string sourceFileName, string newFileName)
        {
            using (Stream pdfStream = new FileStream(sourceFileName, FileMode.Open))
            using (Stream newpdfStream = new FileStream(newFileName, FileMode.Create, FileAccess.ReadWrite))
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, newpdfStream);
                PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                pdfContentByte.SetColorFill(BaseColor.BLUE);
                pdfContentByte.SetFontAndSize(baseFont, 8);
                pdfContentByte.BeginText();
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Kevin Cheng - A Hong Kong actor", 400, 600, 0);
                pdfContentByte.EndText();
                pdfStamper.Close();
            }
        }

        private  void ExtractTextFromPdf(string newFileNameWithImageAndText, string extractedTextFileName)
        {
            using (Stream newpdfStream = new FileStream(newFileNameWithImageAndText, FileMode.Open, FileAccess.ReadWrite))
            {
                PdfReader pdfReader = new PdfReader(newpdfStream);
                string text = PdfTextExtractor.GetTextFromPage(pdfReader, 1, new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy());
                //File.WriteAllText(extractedTextFileName, text);
                textBox1.Text = text;
            }
        }


        private  void ExtractTextFromRegionOfPdf(string sourceFileName)
        {
            using (Stream pdfStream = new FileStream(sourceFileName, FileMode.Open))
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                //System.util.RectangleJ rect = new System.util.RectangleJ(50, 650, 250, 140);
                System.util.RectangleJ rect = new System.util.RectangleJ(0,0,150, 150);
                RenderFilter[] renderFilter = new RenderFilter[1];
                renderFilter[0] = new RegionTextRenderFilter(rect);
                ITextExtractionStrategy textExtractionStrategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), renderFilter);
                //Console.WriteLine(PdfTextExtractor.GetTextFromPage(pdfReader, 1, textExtractionStrategy));
                textBox1.Text = PdfTextExtractor.GetTextFromPage(pdfReader, 1, textExtractionStrategy);
            }
        }

        private  void ChangeTextColorOfPdf(string sourceFileName)
        {
            using (Stream pdfStream = new FileStream(sourceFileName, FileMode.Open))
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                using (PdfStamper stamper = new PdfStamper(pdfReader, pdfStream))
                {
                    iTextSharp.text.Rectangle rect = new Rectangle(130, 635, 230, 650);
                    float[] quadPoints = { rect.Left, rect.Bottom, rect.Right, rect.Bottom, rect.Left, rect.Top, rect.Right, rect.Top };
                    PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quadPoints);
                    highlight.Color = BaseColor.GREEN;
                    stamper.AddAnnotation(highlight, 1);
                }
                //Console.WriteLine("Text was highlighted");
            }
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //paragraph
        //    string path = "D:\\sigma project\\__Sigma_PDF_DWG";
        //    string file = "test.pDF";
        //    string workingFile = path + @"\" + file;
           
        //    //PdfReader reader = new PdfReader(workingFile);
        //    //TextAsParagraphsExtractionStrategy S = new TextAsParagraphsExtractionStrategy();
        //    //iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, S);
        //    //for (int i = 0; i < S.strings.Count; i++)
        //    //{
        //    //    Console.WriteLine("Line {0,-5}: {1}", S.baselines[i], S.strings[i]);
        //    //}

        //    //From source
        //    var t = new MyLocationTextExtractionStrategy(textBox2.Text.Trim());




        //    //Parse page 1 of the document above
        //    using (var r = new PdfReader(workingFile))
        //    {
        //        var ex = PdfTextExtractor.GetTextFromPage(r, varPage, t);
        //    }

        //    //Loop through each chunk found
        //    foreach (var p in t.myPoints)
        //    {
        //        //Console.WriteLine(string.Format("Found text {0} at {1}x{2}", p.Text, p.Rect.Left, p.Rect.Bottom));
        //        MessageBox.Show("Found text |" + p.Text + "| at " + "Left=" + p.Rect.Left + ";Botton=" + p.Rect.Bottom +";Right=" + p.Rect.Right + ";Top=" + p.Rect.Top);

        //    }


  
        //}


        //private void ReadRectangle(string PathFile ,string Text)
        //{
        //    Left =0;
        //    Botton =0;
        //    Right =0;
        //    Top =0;

        //    var t = new MyLocationTextExtractionStrategy(Text);

        //    varPage = 1;

        //    //Parse page 1 of the document above
        //    using (var r = new PdfReader(PathFile))
        //    {

        //        for (int k = 1; k <= r.NumberOfPages; k++)
        //        {
        //            var ex = PdfTextExtractor.GetTextFromPage(r, k, t);
        //            if (ex != null)
        //            {
        //                varPage = k;
        //                break;
        //            }

        //        }
        //    }

        //    //Loop through each chunk found
        //    foreach (var p in t.myPoints)
        //    {
   

        //        Left = p.Rect.Left;
        //        Botton = p.Rect.Bottom;
        //        Right = p.Rect.Right;
        //        Top = p.Rect.Top;
        //    }


        //}

        private void button3_Click(object sender, EventArgs e)
        {

            string path = "D:\\sigma project\\__Sigma_PDF_DWG";
            string file = "test.pDF";
            string workingFile = path + @"\" + file;
            System.IO.FileStream fs = new FileStream(workingFile, FileMode.Create);
            // Create an instance of the document class which represents the PDF document itself.
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // Create an instance to the PDF file by creating an instance of the PDF
            // Writer class using the document and the filestrem in the constructor.

            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            // Open the document to enable you to write to the document

            document.Open();

            // Add a simple and wellknown phrase to the document in a flow layout manner

            document.Add(new Paragraph("Hello World!"));
            document.Add(new Paragraph("Acesta e un test!"));
            // Close the document

            document.Close();
            // Close the writer instance

            writer.Close();
            // Always close open filehandles explicity
            fs.Close();
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
            //try
            //{
            //    //Create a new file from our test file with highlighting
            //    string path = "D:\\sigma project\\__Sigma_PDF_DWG";
            //    string file = "test.pDF";
            //    string file_out = "test_tmp.pDF";

            //    string workingFile = path + @"\" + file;
            //    string tempFile = path + @"\" + file_out;



            //    //string highLightFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Highlighted.pdf");

            //    string highLightFile = workingFile;

            //    //Bind a reader and stamper to our test PDF
            //    string outputFile = string.Empty;

            //    RemovePDFAnnotation(workingFile, tempFile);

                
            //    PdfReader reader = new PdfReader(highLightFile);

             


            //    ReadRectangle(workingFile, textBox2.Text.Trim());

            //    //RemovehighlightPDFAnnotation(workingFile, tempFile, varPage);

            //    using (FileStream fs = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None))
            //    {
            //        using (PdfStamper stamper = new PdfStamper(reader, fs))
            //        {
            //            //Create a rectangle for the highlight. NOTE: Technically this isn't used but it helps with the quadpoint calculation
            //            //iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(60.6755f, 749.172f, 94.0195f, 735.3f);


            //            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(Left, Botton, Right, Top);




            //            //Create an array of quad points based on that rectangle. NOTE: The order below doesn't appear to match the actual spec but is what Acrobat produces
            //            float[] quad = { rect.Left, rect.Bottom, rect.Right, rect.Bottom, rect.Left, rect.Top, rect.Right, rect.Top };










                    
            //            //Create our hightlight
            //            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                 
                        
                       
       

  
                   
            //            //v1 Set the color
            //            highlight.Color = BaseColor.YELLOW;
            //            //highlight.Title= "doi";
            //            stamper.AddAnnotation(highlight, varPage);
                       
      
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        
                     
                        
            //            stamper.Close();
            //        }


            //        fs.Dispose();
            //    }

            //    reader.Close();

            //    if (File.Exists(tempFile))
            //    {
            //        File.Delete(workingFile);
            //        File.Move(tempFile, workingFile);
            //    }



            //    System.Diagnostics.Process.Start(workingFile);
            //    //this.Close();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Subroutine." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
           

        //}

        //private void RemovehighlightPDFAnnotation(string outputFile, string highLightFile, int pageno)
        //{
        //    PdfReader reader = new PdfReader(outputFile);
        //    using (FileStream fs = new FileStream(highLightFile, FileMode.Create, FileAccess.Write, FileShare.None))
        //    {
        //        using (PdfStamper stamper = new PdfStamper(reader, fs))
        //        {
        //            PdfDictionary pageDict = reader.GetPageN(pageno);
        //            PdfArray annots = pageDict.GetAsArray(PdfName.ANNOTS);
        //            if (annots != null)
        //            {
        //                for (int i = 0; i < annots.Size; ++i)
        //                {
        //                    PdfDictionary annotationDic = (PdfDictionary)PdfReader.GetPdfObject(annots[i]);
        //                    PdfName subType = (PdfName)annotationDic.Get(PdfName.SUBTYPE);
        //                    if (subType.Equals(PdfName.HIGHLIGHT))
        //                    {
        //                        PdfString str = annots.GetAsString(i);
        //                        annots.Remove(i);
        //                    }
        //                }


        //            }
        //        }

        //        fs.Dispose();

        //    }

        //    reader.Close();
           
        //}

        private void RemovePDFAnnotation(string OldFile,string NewFile)
        {
            PdfReader reader = new PdfReader(OldFile);
            FileStream fs = new FileStream(NewFile, FileMode.Create, FileAccess.Write, FileShare.None);
            PdfStamper stamper = new PdfStamper(reader, fs);

            //using (FileStream fs = new FileStream(NewFile, FileMode.Create, FileAccess.Write, FileShare.None))
            //{
           
            for(int k = 1; k<=reader.NumberOfPages; k++ )
                {
            
                PdfDictionary page = reader.GetPageN(k);

                PdfArray annots = page.GetAsArray(PdfName.ANNOTS);


                if(annots == null)
                {
                    return;
                }

                int i = annots.ArrayList.Count;

                while(i >=1)
                {

                    annots.Remove(i-1);
                    i = i - 1;
                }
            }


            stamper.Close();
            reader.Close();
            fs.Dispose();
        


            if (File.Exists(NewFile))
            {
                File.Delete(OldFile);
                File.Move(NewFile, OldFile);
            }

            MessageBox.Show("Gata!");


            //annots.Remove(3);
            //annots.Remove(2);
            //annots.Remove(1);
            //annots.Remove(0);
            //for (int i = 0; i < annots.ArrayList.Count; i++)
            //{
            //    annots.Remove(i);
            //}
                //if (annots!=null)
                //{
                //    foreach (PdfObject annot in annots.ArrayList)
                //    //for (int i = 0; i < annots.Length; i++)
                //    {
                //        PdfDictionary annotation = (PdfDictionary)PdfReader.GetPdfObject(annot);
                //        PdfString contents = annotation.GetAsString(PdfName.CONTENTS);


                //        //PdfString Type = annot.GetType();
                //        // now use the String value of contents
                //        //PdfName str = annots.GetAsName(i);
                //    }
                       
                //annots.Remove(0);         
            

               
                //}


           
    
            //reader.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string path = "D:\\sigma project\\__Sigma_PDF_DWG";
            string file = "test.pDF";
            string file_out = "test_tmp.pDF";

            string workingFile = path + @"\" + file;
            string tempFile = path + @"\" + file_out;

            RemovePDFAnnotation(workingFile,tempFile);


        }

        private void button6_Click(object sender, EventArgs e)
        {
            string IniFile = "D:\\sigma project\\__Sigma_PDF_DWG\\test.pdf";
            string TempFile ="C:\\SIGMA\\documents\\test_temp.pdf";

            CustomPDF.RemovePDF_annotation(IniFile, TempFile);
            CustomPDF.HightLight_PDF(IniFile,TempFile,txtTML.Text,txtColor.Text);


            System.Diagnostics.Process.Start(IniFile);


        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            //a = Convert.ToInt32(textBox1.Text);
            //timer1.Enabled = true;
            
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (a == 1)
            //{
             
            //    listBox1.Items.Add("salut");
            //}
            //else
            //{
            //    timer1.Dispose();
            //}

        }

         //public void manipulatePdf(String src, String dest) throws IOException, DocumentException 
        //    {
        //    PdfReader reader = new PdfReader(src);
        //    PdfDictionary page = reader.getPageN(1);
        //    PdfArray annots = page.getAsArray(PdfName.ANNOTS);
        //    PdfDictionary sticky = annots.getAsDict(0);
        //    PdfArray stickyRect = sticky.getAsArray(PdfName.RECT);
        //    PdfRectangle stickyRectangle = new PdfRectangle(
        //    stickyRect.getAsNumber(0).floatValue() - 120, stickyRect.getAsNumber(1).floatValue() -70,
        //    stickyRect.getAsNumber(2).floatValue(), stickyRect.getAsNumber(3).floatValue() - 30
        //    );
        //sticky.put(PdfName.RECT, stickyRectangle);
        //PdfDictionary popup = annots.getAsDict(1);
        //PdfArray popupRect = popup.getAsArray(PdfName.RECT);
        //PdfRectangle popupRectangle = new PdfRectangle(
        //    popupRect.getAsNumber(0).floatValue() - 250, popupRect.getAsNumber(1).floatValue(),
        //    popupRect.getAsNumber(2).floatValue(), popupRect.getAsNumber(3).floatValue() - 250
        //);
        //popup.put(PdfName.RECT, popupRectangle);
        //PdfStamper stamper = new PdfStamper(reader, new FileOutputStream(dest));
        //stamper.close();
    //}



    }
}
