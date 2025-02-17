﻿using API.Models.Domain.Concrete;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace API.Services.Interfaces.Control
{
    public interface IPdfService
    {
        byte[] GenerateOrderPdf(Order order);
    }
}
