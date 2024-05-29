using Business.Interfaces;
using Business.Models;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Windows.Forms;

namespace Business;
public class ReportGenerator(IService<Tour> tourService, IService<TourLog> tourLogService)
{
	public void GenerateTourReport(int id)
	{
		Tour? currTour = new(); //move to business, only pass id
		IEnumerable<TourLog>? currLogs;
		currTour = tourService.GetById(id);
		currLogs = tourLogService.GetLogsByTourId(id);

	}

	public void GenerateSummaryReport(int id)
	{

	}

	//public TourStatsSummary CalculateAverage(int id)
	//{
	//
	//}

	public void PromptSaveDialog()
	{
		Stream stream;
		SaveFileDialog saveFileDialog = new SaveFileDialog();

		saveFileDialog.CheckWriteAccess = true;
		saveFileDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;

		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			if ((stream = saveFileDialog.OpenFile()) != null)
			{
				PdfWriter writer = new PdfWriter(stream);
				PdfDocument pdf = new PdfDocument(writer);
				Document document = new Document(pdf);
				Paragraph header = new Paragraph("HEADER")
				   .SetTextAlignment(TextAlignment.CENTER)
				   .SetFontSize(20);

				document.Add(header);
				document.Close();
			}
		}
	}
}
