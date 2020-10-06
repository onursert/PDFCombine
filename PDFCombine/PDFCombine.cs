using System;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace PDFCombine
{
    public partial class PDFCombine : Form
    {
        public PDFCombine()
        {
            InitializeComponent();
        }

        private void btn_add_pdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    listBox1.Items.Add(file);
                }
            }
        }

        private void btn_combine_Click(object sender, EventArgs e)
        {
            PdfDocument pdfOutput = new PdfDocument(new PdfWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Output.pdf"));

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                PdfDocument pdfToCombine = new PdfDocument(new PdfReader(listBox1.Items[i].ToString()));
                PdfMerger merger = new PdfMerger(pdfOutput);
                merger.Merge(pdfToCombine, 1, pdfToCombine.GetNumberOfPages());
                pdfToCombine.Close();

                progressBar1.Value = (i + 1) * 100 / listBox1.Items.Count;
            }

            pdfOutput.Close();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            progressBar1.Value = 0;
        }
    }
}
