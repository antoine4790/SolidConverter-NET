using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using SolidFramework.Converters.Plumbing;

namespace SolidConverter
{
    public partial class ProgressDialog : Form
    {
        private List<string> files;
        private int converterType = 0;
        private SolidFramework.Converters.Plumbing.ReconstructionMode reconstructionMode;
        private string password = string.Empty;

        private bool stopProcess = false;
        private Thread workerThread = null;
        private string currentStage = string.Empty;
        private string currentFile = string.Empty;
        private string ocrLang;

        public ProgressDialog(ref List<string> fileNames, int convertType, SolidFramework.Converters.Plumbing.ReconstructionMode recMode, string language)
        {
            InitializeComponent();

            files = fileNames;
            converterType = convertType;
            reconstructionMode = recMode;
            ocrLang = language;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            
            // Start converting in a thread here.
            this.workerThread = new Thread(new ThreadStart(ConvertOperation));
            this.workerThread.Start();
        }

        private void GetPassword(string fileName)
        {
            SolidFramework.Forms.PasswordForm passDialog = new SolidFramework.Forms.PasswordForm(fileName);
            DialogResult result = passDialog.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.password = passDialog.Password;
                return;
            }

            this.password = string.Empty;
        }

        private void ConvertOperation()
        {
            try
            {
                switch (this.converterType)
                {
                    case 0:
                        DoWordConversion(ref this.files, true);
                        break;
                    case 1:
                        DoWordConversion(ref this.files, false);
                        break;
                    case 2:
                        DoTextConversion(ref this.files);
                        break;
                    case 3:
                        DoHtmlConversion(ref this.files);
                        break;
                    case 4:
                        DoImageConversion(ref this.files);
                        break;
                    case 5:
                        DoExcelConversion(ref this.files);
                        break;
                    case 6:
                        DoCsvConversion(ref this.files);
                        break;
                    case 7:
                        DoPowerPointConversion(ref this.files);
                        break;
                    case 8:
                        DoPdfAConversion(ref this.files);
                        break;
					case 9:
						DoTaggedPDFConversion(ref this.files);
						break;
				}
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("License"))
                {
                    Invoke(new Action(() => this.ShowExceptionMessage("Please enter valid unlock information in About", true)));
                }
                else
                {
                    Invoke(new Action(() => this.ShowExceptionMessage(ex.Message, false)));
                }
            }

            Invoke(new Action(() => this.ConversionDone()));
        }

        private void ConversionDone()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        void converter_Progress(object sender, SolidFramework.ProgressEventArgs e)
        {
            if (this.InvokeRequired)
            {
                if (this.currentStage != e.StatusDescription)
                {
                    this.currentStage = e.StatusDescription;
                    if (currentStage == "PDFPreScanInfoText")
                    {
                        this.Invoke(new Action(() => this.labelMode.Text = "Scanning PDF"));
                    }
                    else if (currentStage == "PDFLoadingInfoText")
                    {
                        this.Invoke(new Action(() => this.labelMode.Text = "Loading PDF"));
                    }
                    else if (currentStage == "PDFConvertingInfoText")
                    {
                        this.Invoke(new Action(() => this.labelMode.Text = "Converting PDF"));
                    }
                    else if (currentStage == "WritingFileMessage")
                    {
                        this.Invoke(new Action(() => this.labelMode.Text = "Writing file"));
                    }
                }

                this.Invoke(new Action(() => this.progressBar1.Value = e.Progress));

                if (this.stopProcess)
                {
                    ((SolidFramework.Converters.Converter)sender).Cancel();
                    this.Invoke(new Action(() => this.labelMode.Text = "Cancelling"));
                }
            }
        }

        private void SetCurrentFile(string filename)
        {
            this.labelFile.Text = filename;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.stopProcess = true; 
            this.labelMode.Text = "Cancelling";
            
            // Kinda important. The default is OK, and taht causes the dialog to close
            // on any button click. TO stop this, set None.
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void ShowConversionError(SolidFramework.Converters.Plumbing.ConversionStatus status, string fileName)
        {
            string errMessage = string.Format("{0} failed: {1}", Path.GetFileName(fileName), status);
            SolidFramework.Forms.SolidMessageBox messageDialog = new SolidFramework.Forms.SolidMessageBox(this);
            messageDialog.Content = errMessage;
            messageDialog.Text = "Conversion Error";
            messageDialog.MessageIcon = MessageBoxIcon.Error;
            messageDialog.Buttons = MessageBoxButtons.OK;
            messageDialog.ShowIcon = true;
            messageDialog.Execute();
        }

        private void ShowExceptionMessage(string message, bool isWarning)
        {
            SolidFramework.Forms.SolidMessageBox messageDialog = new SolidFramework.Forms.SolidMessageBox(this);
            messageDialog.Content = message;
            if (isWarning)
            {
                messageDialog.Text = "Warning";
                messageDialog.MessageIcon = MessageBoxIcon.Warning;
            }
            else
            {
                messageDialog.Text = "Error";
                messageDialog.MessageIcon = MessageBoxIcon.Error;
            }
            messageDialog.Buttons = MessageBoxButtons.OK;
            messageDialog.ShowIcon = true;
            messageDialog.Execute();
        }

        private void DoWordConversion(ref List<string> files, bool doRtf)
        {
            foreach (string file in this.files)
            {
                if (this.stopProcess)
                {
                    return;
                }

                using (SolidFramework.Converters.PdfToWordConverter converter = new SolidFramework.Converters.PdfToWordConverter())
                {
                    converter.Progress += converter_Progress;
                    converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;

                    if (doRtf)
                    {
                        converter.OutputType = WordDocumentType.Rtf;
                    }
                    else
                    {
                        converter.OutputType = WordDocumentType.DocX;
                    }

                    converter.AddSourceFile(file);
                        
                    Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

                    if (OCRInit.IrisInstalled() != false)
                    {
                        if (ocrLang == "ko" |
                            ocrLang == "ja" |
                            ocrLang == "zh" |
                            ocrLang == "zt")
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.IRIS;
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                        else
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.SolidOCR;
                            if (ocrLang == "Automatic")
                            {
                                ocrLang = "au";
                            }
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                    }

                    // Converted file goes into the directory as source file.
                    string outputFolder = Path.GetDirectoryName(file);
                    converter.OutputDirectory = outputFolder;

                RedoConversion:
                    converter.Password = this.password;

                    converter.Convert();

                    IConversionResult result = converter.Results[0];

                    if (result.Status != ConversionStatus.Success)
                    {
                        if (result.Status != ConversionStatus.Canceled)
                        {
                            if (result.Status == ConversionStatus.FileHasCopyProtection)
                            {
                                Invoke(new Action(() => this.GetPassword(Path.GetFileName(file))));
                                if (!string.IsNullOrEmpty(this.password))
                                {
                                    goto RedoConversion;
                                }
                            }
                            else
                            {
                                Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
                            }
                        }
                    }
                }
            }
        }

		private void DoTextConversion(ref List<string> files)
        {
            foreach (string file in this.files)
            {
                if (this.stopProcess)
                {
                    return;
                }

                using (SolidFramework.Converters.PdfToTextConverter converter = new SolidFramework.Converters.PdfToTextConverter())
                {
                    converter.Progress += converter_Progress;
                    converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;

                    converter.AddSourceFile(file);
                    Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

                    if (OCRInit.IrisInstalled() != false)
                    {
                        if (ocrLang == "ko" |
                            ocrLang == "ja" |
                            ocrLang == "zh" |
                            ocrLang == "zt")
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.IRIS;
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                        else
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.SolidOCR;
                            if (ocrLang == "Automatic")
                            {
                                ocrLang = "au";
                            }
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                    }
                    
                    // Converted file goes into the directory as source file.
                    string outputFolder = Path.GetDirectoryName(file);
                    converter.OutputDirectory = outputFolder;

                RedoConversion:
                    converter.Password = this.password;
                    
                    converter.Convert();

                    IConversionResult result = converter.Results[0];

                    if (result.Status != ConversionStatus.Success)
                    {
                        if (result.Status != ConversionStatus.Canceled)
                        {
                            if (result.Status == ConversionStatus.FileHasCopyProtection)
                            {
                                Invoke(new Action(() => this.GetPassword(Path.GetFileName(file))));
                                if (!string.IsNullOrEmpty(this.password))
                                {
                                    goto RedoConversion;
                                }
                            }
                            else
                            {
                                Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
                            }
                        }
                    }
                }
            }
        }

        private void DoHtmlConversion(ref List<string> files)
        {
            foreach (string file in this.files)
            {
                if (this.stopProcess)
                {
                    return;
                }

                using (SolidFramework.Converters.PdfToHtmlConverter converter = new SolidFramework.Converters.PdfToHtmlConverter())
                {
                    converter.Progress += converter_Progress;
                    converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;
                    converter.ImageType = ImageDocumentType.Png;

                    converter.AddSourceFile(file);
                    Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

                    if (OCRInit.IrisInstalled() != false)
                    {
                        if (ocrLang == "ko" |
                            ocrLang == "ja" |
                            ocrLang == "zh" |
                            ocrLang == "zt")
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.IRIS;
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                        else
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.SolidOCR;
                            if (ocrLang == "Automatic")
                            {
                                ocrLang = "au";
                            }
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                    }

                RedoConversion:
                    converter.Password = this.password;

                    converter.Convert();

                    IConversionResult result = converter.Results[0];

                    if (result.Status != ConversionStatus.Success)
                    {
                        if (result.Status != ConversionStatus.Canceled)
                        {
                            if (result.Status == ConversionStatus.FileHasCopyProtection)
                            {
                                Invoke(new Action(() => this.GetPassword(Path.GetFileName(file))));
                                if (!string.IsNullOrEmpty(this.password))
                                {
                                    goto RedoConversion;
                                }
                            }
                            else
                            {
                                Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
                            }
                        }
                    }
                    else
                    {
                        string outExtension = Path.GetExtension(result.Paths[0]);
                        string outputFolder = Path.GetDirectoryName(file);
                        string convFile = Path.ChangeExtension(file, outExtension);
                        SolidFramework.Plumbing.Utilities.FileCopy(result.Paths[0], convFile, true);

                        // See if we have images, and if so, copy over and rebase.
                        String imageFolder = Path.GetFileNameWithoutExtension(converter.Results[0].Paths[0]);
                        String imageSourceFolder = Path.GetDirectoryName(converter.Results[0].Paths[0]);
                        imageSourceFolder = Path.Combine(imageSourceFolder, imageFolder);
                        String imageOutputFolder = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(convFile));
                        if (Directory.Exists(imageSourceFolder))
                        {
                            foreach (string image in Directory.GetFiles(imageSourceFolder, "*.*", SearchOption.TopDirectoryOnly))
                            {
                                String imageOutput = Path.Combine(imageOutputFolder, Path.GetFileName(image));
                                if (image.ToUpperInvariant() != imageOutput.ToUpperInvariant())
                                {
                                    SolidFramework.Plumbing.Utilities.FileCopy(image, imageOutput, true);
                                }
                            }

                            // Rebase the images in the html to new folder.
                            SolidFramework.Converters.PdfToHtmlConverter.RebaseHtml(convFile, Path.GetFileNameWithoutExtension(result.Paths[0]), Path.GetFileNameWithoutExtension(convFile));
                        }
                    }
                }
            }
        }

        private void DoImageConversion(ref List<string> files)
        {
            foreach (string file in this.files)
            {
                if (this.stopProcess)
                {
                    return;
                }

                using (SolidFramework.Converters.PdfToImageConverter converter = new SolidFramework.Converters.PdfToImageConverter())
                {
                    converter.Progress += converter_Progress;
                    converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;
                    converter.OutputType = ImageDocumentType.Default;
                    converter.ConversionType = ImageConversionType.ExtractPages;
                    converter.AddSourceFile(file);
                    Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

                    // Images go into folder with filename + ".images" in same directory as source file.
                    string outputFolder = Path.GetDirectoryName(file);
                    outputFolder = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + ".images");
                    converter.OutputDirectory = outputFolder;

                RedoConversion:
                    converter.Password = this.password;

                    converter.Convert();

                    IConversionResult result = converter.Results[0];

                    if (result.Status != ConversionStatus.Success)
                    {
                        if (result.Status != ConversionStatus.Canceled)
                        {
                            if (result.Status == ConversionStatus.FileHasCopyProtection)
                            {
                                Invoke(new Action(() => this.GetPassword(Path.GetFileName(file))));
                                if (!string.IsNullOrEmpty(this.password))
                                {
                                    goto RedoConversion;
                                }
                            }
                            else
                            {
                                Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
                            }
                        }
                    }
                }
            }
        }

        private void DoExcelConversion(ref List<string> files)
        {
            foreach (string file in this.files)
            {
                if (this.stopProcess)
                {
                    return;
                }

                using (SolidFramework.Converters.PdfToExcelConverter converter = new SolidFramework.Converters.PdfToExcelConverter())
                {
                    converter.Progress += converter_Progress;
                    converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;
                    converter.OutputType = ExcelDocumentType.XlsX;

                    converter.AddSourceFile(file);
                    Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

                    if (OCRInit.IrisInstalled() != false)
                    {
                        if (ocrLang == "ko" |
                            ocrLang == "ja" |
                            ocrLang == "zh" |
                            ocrLang == "zt")
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.IRIS;
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                        else
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.SolidOCR;
                            if (ocrLang == "Automatic")
                            {
                                ocrLang = "au";
                            }
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                    }

                    // Converted file goes into the directory as source file.
                    string outputFolder = Path.GetDirectoryName(file);
                    converter.OutputDirectory = outputFolder;

                RedoConversion:
                    converter.Password = this.password;

                    converter.Convert();

                    IConversionResult result = converter.Results[0];

                    if (result.Status != ConversionStatus.Success)
                    {
                        if (result.Status != ConversionStatus.Canceled)
                        {
                            if (result.Status == ConversionStatus.FileHasCopyProtection)
                            {
                                Invoke(new Action(() => this.GetPassword(Path.GetFileName(file))));
                                if (!string.IsNullOrEmpty(this.password))
                                {
                                    goto RedoConversion;
                                }
                            }
                            else
                            {
                                Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
                            }
                        }
                    }
                }
            }
        }

        private void DoCsvConversion(ref List<string> files)
        {
            foreach (string file in this.files)
            {
                if (this.stopProcess)
                {
                    return;
                }

                using (SolidFramework.Converters.PdfToDataConverter converter = new SolidFramework.Converters.PdfToDataConverter())
                {
                    converter.Progress += converter_Progress;
                    converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;
                    converter.OutputType = DataDocumentType.Csv;

                    converter.AddSourceFile(file);
                    Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

                    if (OCRInit.IrisInstalled() != false)
                    {
                        if (ocrLang == "ko" |
                            ocrLang == "ja" |
                            ocrLang == "zh" |
                            ocrLang == "zt")
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.IRIS;
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                        else
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.SolidOCR;
                            if (ocrLang == "Automatic")
                            {
                                ocrLang = "au";
                            }
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                    }

                    // Datafiles go into folder with filename + ".csv" in same directory as source file.
                    string outputFolder = Path.GetDirectoryName(file);
                    converter.OutputDirectory = outputFolder;

                RedoConversion:
                    converter.Password = this.password;

                    converter.Convert();

                    IConversionResult result = converter.Results[0];

                    if (result.Status != ConversionStatus.Success)
                    {
                        if (result.Status != ConversionStatus.Canceled)
                        {
                            if (result.Status == ConversionStatus.FileHasCopyProtection)
                            {
                                Invoke(new Action(() => this.GetPassword(Path.GetFileName(file))));
                                if (!string.IsNullOrEmpty(this.password))
                                {
                                    goto RedoConversion;
                                }
                            }
                            else
                            {
                                Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
                            }
                        }
                    }
                }
            }
        }

        private void DoPowerPointConversion(ref List<string> files)
        {
            foreach (string file in this.files)
            {
                if (this.stopProcess)
                {
                    return;
                }

                using (SolidFramework.Converters.PdfToPowerPointConverter converter = new SolidFramework.Converters.PdfToPowerPointConverter())
                {
                    converter.Progress += converter_Progress;
                    converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;

                    converter.AddSourceFile(file);
                    Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

                    if (OCRInit.IrisInstalled() != false)
                    {
                        if (ocrLang == "ko" |
                            ocrLang == "ja" |
                            ocrLang == "zh" |
                            ocrLang == "zt")
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.IRIS;
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                        else
                        {
                            converter.TextRecoveryEngine = TextRecoveryEngine.SolidOCR;
                            if (ocrLang == "Automatic")
                            {
                                ocrLang = "au";
                            }
                            converter.TextRecoveryLanguage = ocrLang;
                        }
                    }

                    // Converted file goes into the directory as source file.                   
                    string outputFolder = Path.GetDirectoryName(file);
                    converter.OutputDirectory = outputFolder;

                RedoConversion:
                    converter.Password = this.password;

                    converter.Convert();

                    IConversionResult result = converter.Results[0];

                    if (result.Status != ConversionStatus.Success)
                    {
                        if (result.Status != ConversionStatus.Canceled)
                        {
                            if (result.Status == ConversionStatus.FileHasCopyProtection)
                            {
                                Invoke(new Action(() => this.GetPassword(Path.GetFileName(file))));
                                if (!string.IsNullOrEmpty(this.password))
                                {
                                    goto RedoConversion;
                                }
                            }
                            else
                            {
                                Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
                            }
                        }
                    }
                }
            }
        }

        private void DoPdfAConversion(ref List<string> files)
        {
            foreach (string file in this.files)
            {
                if (this.stopProcess)
                {
                    return;
                }

                using (SolidFramework.Converters.PdfToPdfAConverter converter = new SolidFramework.Converters.PdfToPdfAConverter())
                {
                    converter.Progress += converter_Progress;
                    converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;
                    converter.ValidationMode = SolidFramework.Plumbing.ValidationMode.PdfA2A;
           
                    converter.AddSourceFile(file);
                    Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

                    converter.Convert();
                  
                    PdfAConversionResult result = (PdfAConversionResult)converter.Results[0];

                    if (result.PdfAStatus != PdfAConversionStatus.ErrorsFixed && result.PdfAStatus != PdfAConversionStatus.Compliant)
                    {
                        if (result.Status != ConversionStatus.Canceled)
                        {
                            Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
                        }
                    }
                    else
                    {
                        // Save the PDFA file to the same folder and append -2b to the filename.
                        string outFile = Path.GetFileNameWithoutExtension(file);
                        outFile = outFile + "-2A.pdf";
                        string outDir = Path.GetDirectoryName(file);
                        string convFile = Path.Combine(outDir, outFile);
                        SolidFramework.Plumbing.Utilities.FileCopy(result.Paths[0], convFile, true);
                    }
                }
            }
        }

		private void DoTaggedPDFConversion(ref List<string> files)
		{
			foreach (string file in this.files)
			{
				if (this.stopProcess)
				{
					return;
				}

				using (SolidFramework.Converters.PdfToPdfConverter converter = new SolidFramework.Converters.PdfToPdfConverter())
				{
					converter.Progress += converter_Progress;
					converter.OverwriteMode = SolidFramework.Plumbing.OverwriteMode.ForceOverwrite;

					converter.AddSourceFile(file);

					Invoke(new Action(() => this.SetCurrentFile(Path.GetFileName(file))));

					if (OCRInit.IrisInstalled() != false)
					{
						if (ocrLang == "ko" |
							ocrLang == "ja" |
							ocrLang == "zh" |
							ocrLang == "zt")
						{
							converter.OcrEngine = TextRecoveryEngine.IRIS;
							converter.OcrLanguage = ocrLang;
						}
						else
						{
							converter.OcrEngine = TextRecoveryEngine.SolidOCR;
							if (ocrLang == "Automatic")
							{
								ocrLang = "au";
							}
							converter.OcrLanguage = ocrLang;
						}
					}

					// Creating a tagged pdf.
					converter.CreateTags = true;

				RedoConversion:
					converter.Password = this.password;

					converter.Convert();

					IConversionResult result = converter.Results[0];

					if (result.Status != ConversionStatus.Success)
					{
						if (result.Status != ConversionStatus.Canceled)
						{
							if (result.Status == ConversionStatus.FileHasCopyProtection)
							{
								Invoke(new Action(() => this.GetPassword(Path.GetFileName(file))));
								if (!string.IsNullOrEmpty(this.password))
								{
									goto RedoConversion;
								}
							}
							else
							{
								Invoke(new Action(() => this.ShowConversionError(result.Status, result.Source)));
							}
						}
					}
					else
					{
						// Save the PDF file to the same folder and append -tagged to the filename.
						string outFile = Path.GetFileNameWithoutExtension(file);
						outFile = outFile + "-tagged.pdf";
						string outDir = Path.GetDirectoryName(file);
						string convFile = Path.Combine(outDir, outFile);
						SolidFramework.Plumbing.Utilities.FileCopy(result.Paths[0], convFile, true);
					}
				}
			}
		}
    }
}
