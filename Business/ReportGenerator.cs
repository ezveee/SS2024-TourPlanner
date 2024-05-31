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
using Microsoft.VisualBasic.Logging;

namespace Business;
public class ReportGenerator(IService<Tour> tourService, IService<TourLog> tourLogService)
{
	private enum Type
	{
		TOUR = 0,
		SUMMARY = 1
	}

	private Tour? _currTour;
	private IEnumerable<TourLog>? _currLogs;
	private TourStatsSummary _summarizedStats = new();

	public void GenerateTourReport(int id)
	{
		this._currTour = tourService.GetById(id);
		this._currLogs = tourLogService.GetLogsByTourId(id);
		PromptSaveDialog(ReportGenerator.Type.TOUR);
	}

	public void GenerateSummaryReport(int id)
	{
		this._currTour = tourService.GetById(id);
		this._currLogs = tourLogService.GetLogsByTourId(id);
		PromptSaveDialog(ReportGenerator.Type.SUMMARY);
	}

	public void CalculateAverage()
	{
		this._summarizedStats.Tourname = this._currTour.Name;
		float averageDistance = 0;
		double averageTime = 0;
		int averageRating = 0;

		foreach(var log in this._currLogs)
		{
			averageDistance += log.Distance;
			averageTime += log.Time;
			averageRating += log.Rating;
		}
		this._summarizedStats.AverageDistance = averageDistance / this._currLogs.Count();
		this._summarizedStats.AverageTime = averageTime / this._currLogs.Count();
		this._summarizedStats.AverageRating = averageRating / this._currLogs.Count();
	}

	private void PromptSaveDialog(ReportGenerator.Type type)
	{
		var t = new Thread((ThreadStart)(() =>
		{
			Stream stream;
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.CheckWriteAccess = true;
			saveFileDialog.FileName = $"{type} _report.pdf";
			saveFileDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 2;
			saveFileDialog.RestoreDirectory = true;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				if ((stream = saveFileDialog.OpenFile()) != null)
				{
					switch(type)
					{
						case Type.TOUR:
							PopulateTourPdf(new PdfWriter(stream));
							break;
						case Type.SUMMARY:
							CalculateAverage();
							PopulateSummaryPdf(new PdfWriter(stream));
							break;
						default:
							break;
					}
						
				}
			}
		}));
		t.SetApartmentState(ApartmentState.STA);
		t.Start();
		t.Join();
	}

	private void PopulateTourPdf(PdfWriter writer)
	{
		PdfDocument pdf = new PdfDocument(writer);
		Document document = new Document(pdf);

		Paragraph header = new Paragraph(this._currTour.Name)
			.SetTextAlignment(TextAlignment.CENTER)
			.SetFontSize(20);
		document.Add(header);

		document.Add(new Paragraph("\n"));

		document.Add(new Paragraph($"Description: {_currTour.Description}"));
		document.Add(new Paragraph($"From: {_currTour.From}"));
		document.Add(new Paragraph($"To: {_currTour.To}"));
		document.Add(new Paragraph($"Transport Type: {_currTour.TransportType}"));
		document.Add(new Paragraph($"Distance: {_currTour.Distance} km"));
		document.Add(new Paragraph($"Estimated Time: {_currTour.EstimatedTime} hours"));


		document.Add(new Paragraph("\n"));

		Table table = new Table(6).SetTextAlignment(TextAlignment.CENTER);

		table.AddHeaderCell("Date");
		table.AddHeaderCell("Difficulty");
		table.AddHeaderCell("Distance");
		table.AddHeaderCell("Time");
		table.AddHeaderCell("Rating");
		table.AddHeaderCell("Comment");

		foreach (var log in this._currLogs)
		{
			table.AddCell(log.Date.ToString());
			table.AddCell(log.Difficulty.ToString());
			table.AddCell(log.Distance.ToString());
			table.AddCell(log.Time.ToString());
			table.AddCell(log.Rating.ToString());
			table.AddCell(log.Comment);
		}

		document.Add(table);

		document.Close();
	}

	private void PopulateSummaryPdf(PdfWriter writer)
	{
		PdfDocument pdf = new PdfDocument(writer);
		Document document = new Document(pdf);

		Paragraph header = new Paragraph(this._summarizedStats.Tourname)
			.SetTextAlignment(TextAlignment.CENTER)
			.SetFontSize(20);
		document.Add(header);

		document.Add(new Paragraph("\n"));

		Table table = new Table(3).SetTextAlignment(TextAlignment.CENTER);
		table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

		table.AddHeaderCell("Average Distance");
		table.AddHeaderCell("Average Time");
		table.AddHeaderCell("Average Rating");

		table.AddCell(this._summarizedStats.AverageDistance.ToString());
		table.AddCell(this._summarizedStats.AverageTime.ToString());
		table.AddCell(this._summarizedStats.AverageRating.ToString());

		document.Add(table);

		document.Close();
	}
}
