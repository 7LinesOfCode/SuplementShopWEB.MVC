using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using SuplementShopWEB.MVC.Application.Interfaces;
using SuplementShopWEB.MVC.Application.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementShopWEB.MVC.Application.PDFGenerator
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        public void CreatingPDF(OrderForListVm order)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);

                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    page.Header()
                        .PaddingHorizontal(3, QuestPDF.Infrastructure.Unit.Centimetre)
                        .Text("Order Confimation")
                        .SemiBold().FontSize(30).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Text($"Order id:  {order.Id}\n" +
                              $"Item id:  {order.ItemId} \n" +
                              $"Item Name:  {order.ItemName} x {order.CountOfItems}\n" +
                              $"Price:  {order.Price}$ \n" +
                              $"Full Name of Client:  {order.CustomerFullName}\n" +
                              $"Date of resive order:  {DateTime.UtcNow} \n")
                        .FontSize(12).FontColor(Colors.Black);

                    page.Footer()
                        .Text("Order document has been generate automaticly. " +
                        "Copyright © 2023 SuplementShop Inc. All rights reserved.")
                        .FontColor(Colors.Grey.Darken1);

                });

            })
            .GeneratePdf($"Order{order.Id}.pdf");
        }
    }
}
