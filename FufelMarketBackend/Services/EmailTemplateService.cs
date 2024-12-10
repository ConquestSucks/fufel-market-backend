namespace FufelMarketBackend.Services;

public class EmailTemplateService(string templatesPath)
{
    public async Task<string> GetEmailBodyAsync(string templateName, Dictionary<string, string> placeholders)
    {
        var templatePath = Path.Combine(templatesPath, templateName);

        if (!File.Exists(templatePath))
            throw new FileNotFoundException("Template not found", templatePath);

        var templateContent = await File.ReadAllTextAsync(templatePath);

        foreach (var placeholder in placeholders)
        {
            templateContent = templateContent.Replace($"{{{placeholder.Key}}}", placeholder.Value);
        }

        return templateContent;
    }
}