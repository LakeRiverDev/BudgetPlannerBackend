using BP.Application.Interfaces;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BP.Application.Services
{
    public class DocumentService : IDocumentService
    {
        public string GeneratePdf(OpenApiDocument swagger)
        {
            var html = new StringBuilder();

            html.AppendLine($@"
<!DOCTYPE html>
<html>
<head>
    <title>{EscapeHtml(swagger.Info.Title)} - API Documentation</title>
    <meta charset='utf-8'>
    <style>
        /* Reset всех отступов */
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.5;
            margin: 0;
            padding: 5mm;
            color: #333;
            background: white;
        }}
        
        .container {{
            max-width: 100%;
        }}
        
        .header {{
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 15px 20px;
            border-radius: 8px;
            margin-bottom: 20px;
        }}
        
        /* Стиль для группы методов */
        .group-container.first-group {{
            page-break-before: auto; /* Первая группа без разрыва страницы */
            margin-bottom: 30px;
        }}
        
        .group-container {{
            page-break-before: always; /* Остальные группы с разрывом страницы */
            margin-bottom: 30px;
        }}
        
        .group-header {{
            background: #2c3e50;
            color: white;
            padding: 12px 15px;
            border-radius: 6px;
            margin-bottom: 15px;
            font-size: 18px;
            font-weight: bold;
            page-break-after: avoid;
        }}
        
        .endpoint {{
            background: #f8f9fa;
            border: 1px solid #e9ecef;
            border-radius: 6px;
            padding: 15px;
            margin-bottom: 15px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.1);
            page-break-inside: avoid;
        }}
        
        .method {{
            font-weight: bold;
            color: white;
            padding: 4px 8px;
            border-radius: 3px;
            display: inline-block;
            margin-right: 8px;
            font-size: 12px;
        }}
        
        .get {{ background-color: #61affe; }}
        .post {{ background-color: #49cc90; }}
        .put {{ background-color: #fca130; }}
        .delete {{ background-color: #f93e3e; }}
        .patch {{ background-color: #50e3c2; }}
        
        .path {{
            font-family: 'Courier New', monospace;
            font-size: 14px;
            color: #555;
        }}
        
        .summary {{
            font-size: 16px;
            font-weight: 600;
            margin: 8px 0;
            color: #2c3e50;
        }}
        
        .description {{
            color: #666;
            margin: 8px 0;
            font-size: 14px;
        }}
        
        .parameters {{
            margin: 12px 0;
        }}
        
        .parameter {{
            background: white;
            padding: 6px 10px;
            margin: 4px 0;
            border-radius: 3px;
            border-left: 3px solid #667eea;
            font-size: 13px;
        }}
        
        .parameter-name {{
            font-weight: 600;
            color: #2c3e50;
        }}
        
        .parameter-type {{
            color: #667eea;
            font-size: 11px;
        }}
        
        .responses {{
            margin: 12px 0;
        }}
        
        .response {{
            background: white;
            padding: 6px 10px;
            margin: 4px 0;
            border-radius: 3px;
            border-left: 3px solid #28a745;
            font-size: 13px;
        }}
        
        .response-code {{
            font-weight: 600;
            color: #28a745;
            font-size: 13px;
        }}
        
        .section-title {{
            font-size: 14px;
            font-weight: 600;
            color: #495057;
            margin: 12px 0 8px 0;
            border-bottom: 1px solid #667eea;
            padding-bottom: 3px;
        }}
        
        .footer {{
            text-align: center;
            margin-top: 30px;
            padding: 15px;
            color: #6c757d;
            font-size: 11px;
            border-top: 1px solid #dee2e6;
        }}
        
        /* Улучшения для печати */
        @media print {{
            body {{
                padding: 0 !important;
            }}
            .header {{
                margin: 0 0 15px 0 !important;
                border-radius: 0;
                page-break-after: avoid;
            }}
            .group-container.first-group {{
                page-break-before: auto !important;
            }}
            .group-container {{
                page-break-before: always !important;
            }}
            .group-header {{
                page-break-after: avoid;
            }}
            .endpoint {{
                box-shadow: none;
                border: 1px solid #ddd;
                page-break-inside: avoid;
            }}
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1 style='margin: 0 0 8px 0; font-size: 20px;'>{EscapeHtml(swagger.Info.Title)}</h1>
            <p style='margin: 0 0 5px 0;'>{EscapeHtml(swagger.Info.Description ?? "API Documentation")}</p>
            <p style='margin: 0 0 3px 0;'><strong>Version:</strong> {EscapeHtml(swagger.Info.Version)}</p>
            <p style='margin: 0;'><strong>Generated:</strong> {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>
        </div>");

            // Группируем endpoints по тегам
            var groupedEndpoints = GroupEndpointsByTags(swagger);
            bool isFirstGroup = true;

            foreach (var group in groupedEndpoints)
            {
                string groupClass = isFirstGroup ? "group-container first-group" : "group-container";

                html.AppendLine($@"
        <div class='{groupClass}'>
            <div class='group-header'>
                {EscapeHtml(group.GroupName)}
            </div>");

                foreach (var endpoint in group.Endpoints)
                {
                    html.AppendLine($@"
            <div class='endpoint'>
                <div style='margin-bottom: 8px;'>
                    <span class='method {endpoint.Method.ToString().ToLower()}'>{endpoint.Method.ToString().ToUpper()}</span>
                    <span class='path'>{EscapeHtml(endpoint.Path)}</span>
                </div>
                
                <div class='summary'>{EscapeHtml(endpoint.Summary ?? "No summary")}</div>
                
                <div class='description'>{EscapeHtml(endpoint.Description ?? "No description")}</div>");

                    // Parameters
                    if (endpoint.Parameters.Any())
                    {
                        html.AppendLine("<div class='section-title'>Parameters</div>");
                        html.AppendLine("<div class='parameters'>");

                        foreach (var parameter in endpoint.Parameters)
                        {
                            html.AppendLine($@"
                    <div class='parameter'>
                        <span class='parameter-name'>{EscapeHtml(parameter.Name)}</span>
                        <span class='parameter-type'>({EscapeHtml(parameter.In)}, {EscapeHtml(parameter.Type)})</span>
                        <div>{EscapeHtml(parameter.Description)}</div>
                        {(parameter.Required ? "<div style='font-size: 12px;'><strong>Required:</strong> Yes</div>" : "")}
                    </div>");
                        }
                        html.AppendLine("</div>");
                    }

                    // Responses
                    if (endpoint.Responses.Any())
                    {
                        html.AppendLine("<div class='section-title'>Responses</div>");
                        html.AppendLine("<div class='responses'>");

                        foreach (var response in endpoint.Responses)
                        {
                            html.AppendLine($@"
                    <div class='response'>
                        <span class='response-code'>{EscapeHtml(response.Code)}</span>
                        <div>{EscapeHtml(response.Description)}</div>");

                            if (response.ContentTypes.Any())
                            {
                                html.AppendLine("<div style='margin-top: 3px;'><strong>Content Type:</strong> ");
                                foreach (var contentType in response.ContentTypes)
                                {
                                    html.AppendLine($"{EscapeHtml(contentType)}");
                                }
                                html.AppendLine("</div>");
                            }
                            html.AppendLine("</div>");
                        }
                        html.AppendLine("</div>");
                    }

                    html.AppendLine("</div>");
                }

                html.AppendLine("</div>"); // закрываем group-container
                isFirstGroup = false;
            }

            html.AppendLine($@"
        <div class='footer'>
            Generated by {EscapeHtml(swagger.Info.Title)} API Documentation System<br>
            {DateTime.Now.Year}
        </div>
    </div>
</body>
</html>");

            return html.ToString();
        }

        // Остальные методы и классы остаются без изменений
        private List<EndpointGroup> GroupEndpointsByTags(OpenApiDocument swagger)
        {
            var groups = new Dictionary<string, EndpointGroup>();

            foreach (var path in swagger.Paths)
            {
                foreach (var operation in path.Value.Operations)
                {
                    var tags = operation.Value.Tags;
                    var groupName = tags?.FirstOrDefault()?.Name ?? "General";

                    if (!groups.ContainsKey(groupName))
                    {
                        groups[groupName] = new EndpointGroup { GroupName = groupName };
                    }

                    var endpoint = new EndpointInfo
                    {
                        Path = path.Key,
                        Method = (Core.Operations.OperationType)operation.Key,
                        Summary = operation.Value.Summary,
                        Description = operation.Value.Description,
                        Parameters = operation.Value.Parameters?.Select(p => new ParameterInfo
                        {
                            Name = p.Name,
                            In = p.In.ToString(),
                            Type = p.Schema?.Type ?? "unknown",
                            Description = p.Description ?? "No description",
                            Required = p.Required
                        }).ToList() ?? new List<ParameterInfo>(),
                        Responses = operation.Value.Responses?.Select(r => new ResponseInfo
                        {
                            Code = r.Key,
                            Description = r.Value.Description ?? "No description",
                            ContentTypes = r.Value.Content?.Keys.ToList() ?? new List<string>()
                        }).ToList() ?? new List<ResponseInfo>()
                    };

                    groups[groupName].Endpoints.Add(endpoint);
                }
            }

            return groups.Values.OrderBy(g => g.GroupName).ToList();
        }

        private class EndpointGroup
        {
            public string GroupName { get; set; }
            public List<EndpointInfo> Endpoints { get; set; } = new List<EndpointInfo>();
        }

        private class EndpointInfo
        {
            public string Path { get; set; }
            public Core.Operations.OperationType Method { get; set; }
            public string Summary { get; set; }
            public string Description { get; set; }
            public List<ParameterInfo> Parameters { get; set; }
            public List<ResponseInfo> Responses { get; set; }
        }

        private class ParameterInfo
        {
            public string Name { get; set; }
            public string In { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public bool Required { get; set; }
        }

        private class ResponseInfo
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public List<string> ContentTypes { get; set; }
        }

        private string EscapeHtml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return System.Net.WebUtility.HtmlEncode(input);
        }
    }
}
