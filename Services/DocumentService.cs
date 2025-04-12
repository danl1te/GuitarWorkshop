using System.IO;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using GuitarWorkshop.Data.Entities;

namespace GuitarWorkshop.Services
{
    public class DocumentService
    {
        public void GenerateAcceptanceAct(Order order)
        {
            var doc = new FlowDocument();

            // Заполнение документа данными из order
            // ... ваш код генерации документа ...

            SaveAsXps(doc, $"Акт приема {order.Id}.xps");
        }

        private void SaveAsXps(FlowDocument document, string fileName)
        {
            using (var package = new XpsDocument(fileName, FileAccess.ReadWrite))
            {
                var writer = XpsDocument.CreateXpsDocumentWriter(package);
                writer.Write(((IDocumentPaginatorSource)document).DocumentPaginator);
            }
        }
    }
}