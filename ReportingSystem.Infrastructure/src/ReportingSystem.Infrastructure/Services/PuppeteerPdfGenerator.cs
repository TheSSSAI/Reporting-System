using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using ReportingSystem.Application.Common.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ReportingSystem.Infrastructure.Services
{
    public class PuppeteerPdfGenerator : IPdfGenerator
    {
        private readonly ILogger<PuppeteerPdfGenerator> _logger;

        public PuppeteerPdfGenerator(ILogger<PuppeteerPdfGenerator> logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> GenerateFromHtmlAsync(string htmlContent)
        {
            IBrowser? browser = null;
            try
            {
                // Download the Chromium browser if it's not already present.
                // This is a one-time operation per machine.
                using var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();

                var launchOptions = new LaunchOptions
                {
                    Headless = true,
                    Args = new[] { "--no-sandbox" } // Often required in containerized/Linux environments
                };

                _logger.LogInformation("Launching headless browser for PDF generation.");
                browser = await Puppeteer.LaunchAsync(launchOptions);

                await using var page = await browser.NewPageAsync();

                // It's crucial to wait until the network is idle to ensure all resources (images, css) are loaded.
                await page.SetContentAsync(htmlContent, new NavigationOptions
                {
                    WaitUntil = new[] { WaitUntilNavigation.Networkidle0 }
                });

                var pdfOptions = new PdfOptions
                {
                    Format = PaperFormat.A4,
                    PrintBackground = true, // Ensure background colors and images are printed
                    MarginOptions = new MarginOptions
                    {
                        Top = "20px",
                        Bottom = "20px",
                        Left = "20px",
                        Right = "20px"
                    }
                };
                
                _logger.LogInformation("Generating PDF data from HTML content.");
                var pdfData = await page.PdfDataAsync(pdfOptions);
                _logger.LogInformation("PDF generation successful. PDF size: {PdfSize} bytes.", pdfData.Length);

                return pdfData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during PDF generation with Puppeteer Sharp.");
                // Wrap the specific exception in a domain-friendly exception
                throw new ApplicationException("Failed to generate PDF from HTML content.", ex);
            }
            finally
            {
                if (browser != null)
                {
                    _logger.LogInformation("Closing headless browser instance.");
                    await browser.CloseAsync();
                }
            }
        }
    }
}